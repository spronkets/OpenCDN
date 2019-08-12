using System;
using System.Collections.Generic;
using CommandLine;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using OpenCdn.Data.Migrations;

namespace OpenCdn.Data.Migrator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("OpenCDN Migration Tool");

            Parser.Default.ParseArguments<ProgramOptions>(args)
                .WithParsed(StartProgram)
                .WithNotParsed(HandleOptionErrors);
        }

        private static void StartProgram(ProgramOptions options)
        {
            var serviceProvider = ConfigureServices(options);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                var canMigrateUp = runner.HasMigrationsToApplyUp(options.Version);
                var canMigrateDown = options.Version.HasValue && runner.HasMigrationsToApplyDown(options.Version.Value);
                var canRollback = runner.HasMigrationsToApplyRollback();

                var migrateUp = false;
                var migrateDown = false;
                var rollback = false;

                if (canMigrateUp && !canMigrateDown && !canRollback)
                {
                    Console.WriteLine("\nYou can only migrate up. Continue? (y/n)");
                    migrateUp = Console.ReadKey().Key == ConsoleKey.Y;
                }
                else if (canMigrateDown && !canMigrateUp && !canRollback)
                {
                    Console.WriteLine("\nYou can only migrate down. Continue? (y/n)");
                    migrateDown = Console.ReadKey().Key == ConsoleKey.Y;
                }
                else if (canRollback && !canMigrateUp && !canMigrateDown)
                {
                    Console.WriteLine("\nYou can only rollback. Continue? (y/n)");
                    rollback = Console.ReadKey().Key == ConsoleKey.Y;
                }
                else
                {
                    var cancel = false;
                    do
                    {
                        Console.WriteLine("Multiple actions available. Please select what you want to do:");

                        if (canMigrateUp)
                        {
                            Console.WriteLine("1) Migrate Up");
                        }

                        if (canMigrateDown)
                        {
                            Console.WriteLine("2) Migrate Down");
                        }

                        if (canRollback)
                        {
                            Console.WriteLine("3) Rollback");
                        }
                    } while (!migrateUp && !migrateDown && !rollback && !cancel);
                }

                //switch (options.Direction)
                //{
                //    case MigrationDirection.Up:
                //        if (options.Version.HasValue)
                //        {
                //            runner.MigrateUp(options.Version.Value);
                //        }
                //        else
                //        {
                //            runner.MigrateUp();
                //        }
                //        break;
                //    case MigrationDirection.Down:
                //        if (options.Version.HasValue)
                //        {
                //            runner.MigrateDown(options.Version.Value);
                //        }
                //        else
                //        {
                //            throw new ArgumentNullException(nameof(ProgramOptions.Version), $"Required when {nameof(options.Direction)} is set to {nameof(MigrationDirection.Down)}.");
                //        }
                //        break;
                //    default:
                //        throw new ArgumentOutOfRangeException(nameof(ProgramOptions.Version), "Invalid migration direction.");
                //}
            }

            if (options.PromptToClose)
            {
                Console.Write("\nPress any key to exit...");
            }
        }

        private static IServiceProvider ConfigureServices(ProgramOptions options)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(mrb => mrb
                    .AddSQLite()
                    .WithGlobalConnectionString(options.ConnectionString)
                    .ScanIn(typeof(MigrationVersions).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void HandleOptionErrors(IEnumerable<Error> optionErrors)
        {
            // TODO: Handle Option Errors
        }
    }
}

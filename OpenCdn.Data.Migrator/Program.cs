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

                var canMigrateUp = false;
                var canMigrateDown = false;
                var canRollback = false;

                if (options.Version.HasValue)
                {
                    var version = options.Version.Value;
                    canMigrateUp = runner.HasMigrationsToApplyUp(version);
                    canMigrateDown = runner.HasMigrationsToApplyDown(version);
                    canRollback = runner.HasMigrationsToApplyRollback();
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

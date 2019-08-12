using CommandLine;
using FluentMigrator;

namespace OpenCdn.Data.Migrator
{
    internal class ProgramOptions
    {
        [Option('c', "connectionString", Required = true, HelpText = "Set the full database connection string.")]
        public string ConnectionString { get; set; }

        [Option('v', "version", Required = false, Default = null, HelpText = "Set the migration version for the migration direction.")]
        public long? Version { get; set; }

        [Option('p', "promptToClose", Required = false, Default = false, HelpText = "Set console to wait for user input to close.")]
        public bool PromptToClose { get; set; }

        [Option('d', "debug", Required = false, Default = false, HelpText = "Set to output debug/verbose logs.")]
        public bool Verbose { get; set; }
    }
}

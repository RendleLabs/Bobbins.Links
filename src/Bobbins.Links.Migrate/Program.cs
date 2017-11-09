using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using RendleLabs.EntityFrameworkCore.MigrateHelper;

namespace Bobbins.Links.Migrate
{
    [UsedImplicitly]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            Console.WriteLine("Trying migration...");
            await new MigrationHelper(loggerFactory).TryMigrate(args);
            Console.WriteLine("Done.");
        }
    }
}

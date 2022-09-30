using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DbUp;
using DbUp.Helpers;

namespace TicketManagement.Database.CD;

public static class Program
{
    public static int Main(string[] args)
    {
        var retryCount = 0;
        var environmentVariableConnectionString = "Data Source=sql-server;Initial Catalog=TicketManagement.Database;Integrated Security=True";
        var connectionString =
            environmentVariableConnectionString; //// ?? args.FirstOrDefault(x => x.StartsWith("--ConnectionString", StringComparison.OrdinalIgnoreCase));

#pragma warning disable S2583 // Conditionally executed code should be reachable
        if (string.IsNullOrEmpty(environmentVariableConnectionString))
        {
            connectionString = connectionString.Substring(connectionString.IndexOf("=") + 1).Replace(@"""", string.Empty);
        }
#pragma warning restore S2583 // Conditionally executed code should be reachable

        // retry three times
        while (true)
        {
            try
            {
                EnsureDatabase.For.SqlDatabase(connectionString);
                break;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                // check type
                if (retryCount < 3)
                {
                    // Display
                    Console.WriteLine("Connection error occured, waiting 10 seconds then trying again.");
                    System.Threading.Thread.Sleep(10000);
                    retryCount += 1;
                }
                else
                {
                    // rethrow
                    throw;
                }
            }
        }

        var upgrader =
            DeployChanges.To
               .SqlDatabase(connectionString)
               .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
               .LogToConsole()
               .Build();

        if (args.Any(a => a.StartsWith("--PreviewReportPath", StringComparison.InvariantCultureIgnoreCase)))
        {
            // Generate a preview file so Octopus Deploy can generate an artifact for approvals
            var report = args.FirstOrDefault(x => x.StartsWith("--PreviewReportPath", StringComparison.OrdinalIgnoreCase));
            report = report.Substring(report.IndexOf("=") + 1).Replace(@"""", string.Empty);

            var fullReportPath = Path.Combine(report, "UpgradeReport.html");

            Console.WriteLine($"Generating the report at {fullReportPath}");

            upgrader.GenerateUpgradeHtmlReport(fullReportPath);
        }
        else
        {
            var result = upgrader.PerformUpgrade();

            // Display the result
            if (result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.WriteLine("Failed!");
            }
        }

        return 0;
    }
}

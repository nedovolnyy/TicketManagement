using System;
using System.Diagnostics;

namespace TicketManagement.IntegrationTests
{
    public class SetupLocalDB
    {
        protected SetupLocalDB()
        {
        }

        /// <summary>
        /// Create a new LocalDbInstance.
        /// </summary>
        public static void SetupLocalDb()
        {
            var processInfo =
            new ProcessStartInfo("cmd.exe", "/c " + "sqllocaldb.exe create localtestdb -s")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };

            var process = Process.Start(processInfo);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            var exitCode = process.ExitCode;

            Console.WriteLine("output>>" + (string.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (string.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode.ToString());
            process.Close();
        }
    }
}

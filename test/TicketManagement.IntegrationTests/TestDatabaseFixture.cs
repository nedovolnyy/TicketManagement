using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.DataAccess.EF;
using TicketManagement.IntegrationTests;

namespace TicketManagement.DataAccess.IntegrationTests
{
    [SetUpFixture]
    public class TestDatabaseFixture
    {
        private static readonly string _testConnectionString = ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString;
        private static readonly string _backupConnectionString = ConfigurationManager.ConnectionStrings["BackupConnection"].ConnectionString;
        public static DatabaseContext DatabaseContext { get; set; } = new DatabaseContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        [OneTimeSetUp]
        public void Setup()
        {
            InitiallizeDatabase();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DropDatabase();
            DatabaseContext.Dispose();
        }

        public void ChangeConfigFile()
        {
            var config = ConfigurationManager.OpenExeConfiguration("");
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString != _testConnectionString ? _testConnectionString : _backupConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            DatabaseContext = new DatabaseContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public void InitiallizeDatabase()
        {
            DropDatabase();

            var target = new DacpacService();
            target.ProcessDacPac(_testConnectionString,
                                 "TestTicketManagement.Database",
                                 "TestTicketManagement.Database.dacpac");
            if (ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString == _testConnectionString)
            {
                ChangeConfigFile();
            }
        }

        public void DropDatabase()
        {
            if (ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString == _backupConnectionString)
            {
                ChangeConfigFile();
            }

            DatabaseContext.Database.OpenConnection();
            var cmd = DatabaseContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = @"IF EXISTS(SELECT * FROM sys.databases WHERE name = 'TestTicketManagement.Database')
                                BEGIN
                                    ALTER DATABASE [TestTicketManagement.Database] SET OFFLINE WITH ROLLBACK IMMEDIATE;
                                    ALTER DATABASE [TestTicketManagement.Database] SET ONLINE;
                                    DROP DATABASE [TestTicketManagement.Database]
                                END;";
            cmd.ExecuteNonQuery();
        }
    }
}

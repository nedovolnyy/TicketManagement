using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Dac;

namespace TicketManagement.IntegrationTests
{
    public class DacpacService
    {
        public void ProcessDacPac(string connectionString,
                                    string databaseName,
                                    string dacpacName)
        {
            var dacOptions = new DacDeployOptions();
            dacOptions.BlockOnPossibleDataLoss = false;

            var dacServiceInstance = new DacServices(connectionString);

            using (DacPackage dacpac = DacPackage.Load(dacpacName))
            {
                dacServiceInstance.Deploy(dacpac, databaseName,
                                        upgradeExisting: true,
                                        options: dacOptions);
            }
        }
    }
}

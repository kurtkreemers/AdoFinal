using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemeenschap
{
    public class VideoDbManager
    {
        private static ConnectionStringSettings conVideoSetting = ConfigurationManager.ConnectionStrings["Video"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conVideoSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conTuin = factory.CreateConnection();
            conTuin.ConnectionString = conVideoSetting.ConnectionString;
            return conTuin;
        }
    }
}

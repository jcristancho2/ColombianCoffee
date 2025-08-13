using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySqlConnector;

namespace ColombianCoffee.Src.Shared.Helpers
{
    public class MysqlVersionResolver
    {
        public static Version DetectVersion(string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var raw = connection.ServerVersion;
            var clean = raw.Split('-')[0];
            return Version.Parse(clean);
        }
    }
}
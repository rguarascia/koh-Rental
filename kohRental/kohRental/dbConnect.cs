using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace kohRental
{
    class dbConnect
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "den1.mysql4.gear.host";
            int port = 3306;
            string database = "kohrental";
            string username = "kohrental";
            string password = "HamiltonKia!@";

            return dbConnectUtil.GetDBConnection(host, port, database, username, password);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System_do_zarządzania_magazynem.Configuration;
using Microsoft.Data.SqlClient;
using System.IO;

namespace System_do_zarządzania_magazynem.Data
{
    public class Database
    {
        private string connectionString;

        public Database()
        {
            
            string json = File.ReadAllText("config.json");
            Config config = JsonSerializer.Deserialize<Config>(json);

            connectionString = config.ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }  
}

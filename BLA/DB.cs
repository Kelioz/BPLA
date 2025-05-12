using System;
using System.Data.SqlClient;
using System.IO;

namespace BLA
{
    internal class DB
    {
        public static readonly string ConnectionFilePath = "connection.txt";
        private static string _serverName = "DESKTOP-Q09V9UI"; // default value

        static DB()
        {
            // Read server name from file if it exists
            if (File.Exists(ConnectionFilePath))
            {
                _serverName = File.ReadAllText(ConnectionFilePath).Trim();
            }
        }

        SqlConnection connection = new SqlConnection($@"Data Source={_serverName}; Initial Catalog=Meteor; Integrated Security=True");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public SqlConnection GetConnection() { return connection; }

        public static void UpdateServerName(string newServerName)
        {
            _serverName = newServerName;
            File.WriteAllText(ConnectionFilePath, newServerName);
        }
    }
}
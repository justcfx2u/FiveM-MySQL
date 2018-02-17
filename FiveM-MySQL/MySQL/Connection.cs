using MySql.Data.MySqlClient;
using System;

namespace GHMatti.MySQL
{
    public class Connection : IDisposable
    {
        public readonly MySqlConnection connection;

        public Connection(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}

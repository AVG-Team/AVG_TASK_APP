using AVG_TASK_APP.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;

namespace AVG_TASK_APP.Repositories
{
    public class RepositoryBase
    {
        public string ConnectionString
        {
            get
            {
                string appRootDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(appRootDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (connectionString == null)
                {
                    return "";
                }

                return connectionString;
            }
        }

        public RepositoryBase()
        {
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public bool IsServerConnected()
        {
            using (var l_oConnection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
        }
    }
}
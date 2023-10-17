using AVG_TASK_APP.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace AVG_TASK_APP.Repositories
{
    public class RepositoryBase
    {
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (connectionString == null)
                {
                    connectionString = "";
                }

                _connectionString = connectionString;
                return connectionString;
            }
        }

        public RepositoryBase()
        {
            _connectionString = "Server=103.200.23.139;Database=ntddevte_avg_task;Uid=ntddevte_admin;Pwd=dung@16082003;";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}
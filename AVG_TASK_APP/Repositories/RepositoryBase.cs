using System.Data.Common;
using System.Data.SqlClient;

namespace AVG_TASK_APP.Repositories
{
    public class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=103.200.23.139;Database=ntddevte_avg_task;Uid=ntddevte_huy;Pwd=AVGTASK2023;";
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
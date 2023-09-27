using AVG_TASK_APP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }


        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())

            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where username= @username and [password] = @password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }

            return validUser;
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByEmail(string email)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())

            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where email= @email";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Email = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            PhoneNumber = reader[4].ToString(),
                            Level = reader[5].ToString(),
                        };
                    }
                }
            }

            return user;
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void check()
        {
            if (IsServerConnected())
            {
                MessageBox.Show("Connect!!");
            }
            else
            {
                MessageBox.Show("Error!!");
            }
        }
    }
}

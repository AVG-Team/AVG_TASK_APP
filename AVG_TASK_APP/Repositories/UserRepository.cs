using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            var connection = GetConnection();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connection, serverVersion);

            var dbContext = new AppDbContext(optionsBuilder.Options);

            dbContext.Users.Add(userModel);
            dbContext.SaveChanges();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser = false;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())

            {
                //connection.Open();
                //command.Connection = connection;
                //command.CommandText = "select * from [User] where username= @username and [password] = @password";
                //command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                //command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                //validUser = command.ExecuteScalar() == null ? false : true;
            }

            return validUser;
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByEmail(string email)
        {
            if (email == null)
                return null;
            UserModel user = null;

            var connection = GetConnection();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connection, serverVersion);

            var dbContext = new AppDbContext(optionsBuilder.Options);

            user = dbContext.Users.FirstOrDefault(x => x.Email == email);
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

        public string HashPassword(SecureString securePassword, byte[] salt)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
            string password = Marshal.PtrToStringUni(unmanagedString);

            using (var sha256 = new SHA256Managed())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
                var hash = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hash);
            }
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public bool verifyAccount(string username, SecureString password)
        {
            var connection = GetConnection();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connection, serverVersion);

            var dbContext = new AppDbContext(optionsBuilder.Options);

            
            byte[] salt = dbContext.Users.FirstOrDefault(x => x.Email == username).Salt;

            if(!HashPassword(password, salt).Equals(dbContext.Users.FirstOrDefault(x => x.Email == username).Password))
            {
                return false;
            }
            return true;
        }
    }
}

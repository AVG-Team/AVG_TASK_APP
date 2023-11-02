using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private AppDbContext dbContext
        {
            get
            {
                var connection = GetConnection();
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySql(connection, serverVersion);

                return new AppDbContext(optionsBuilder.Options);
            }
        }

        public void Add(UserModel userModel)
        {
            AppDbContext dbContextTmp = dbContext;

            dbContextTmp.Users.Add(userModel);
            dbContextTmp.SaveChanges();
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
            return dbContext.Users.ToList();
        }

        public UserModel GetByEmail(string email)
        {
            if (email == null)
                return null;

            return dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public UserModel GetById(int id)
        {
            if (id == null)
                return null;

            return dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            AppDbContext dbContextTmp = dbContext;

            UserModel userModel = GetById(id);
            dbContextTmp.Users.Remove(userModel);
            dbContextTmp.SaveChanges();
        }

        public void Update(UserModel userModel)
        {
            AppDbContext dbContextTmp = dbContext;

            dbContextTmp.Users.Update(userModel);
            dbContextTmp.SaveChanges();
        }

        public void check()
        {
            MessageBoxView msb = new MessageBoxView();
            if (IsServerConnected())
            {
                msb.Show("Connect!!");
            }
            else
            {
                msb.Show("Error!!!", 1);
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
            if (dbContext.Users.FirstOrDefault(x => x.Email == username) == null)
            {
                return false;
            }

            byte[] salt = dbContext.Users.FirstOrDefault(x => x.Email == username).Salt;

            if (!HashPassword(password, salt).Equals(dbContext.Users.FirstOrDefault(x => x.Email == username).Password))
            {
                return false;
            }
            return true;
        }

        public bool verifyAccount(string username, String password)
        {
            UserModel user = dbContext.Users.FirstOrDefault(x => x.Email == username);
            if (user == null)
            {
                return false;
            }

            byte[] salt = user.Salt;

            if (!password.Equals(user.Password))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<UserModel> GetByContainEmail(string email)
        {
            List<UserModel> users = null;

            users = dbContext.Users.Where(s => s.Email.Contains(email)).ToList();

            return users;
        }
    }
}

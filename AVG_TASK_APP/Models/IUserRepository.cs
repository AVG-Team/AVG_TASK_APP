using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Update(UserModel userModel);
        void Remove(int id);
        string HashPassword(SecureString securePassword, byte[] salt);
        byte[] GenerateSalt();
        UserModel GetById(int id);
        UserModel GetByEmail(string email);
        IEnumerable<UserModel> GetAll();
        bool verifyAccount(string username , SecureString password);

    }
}

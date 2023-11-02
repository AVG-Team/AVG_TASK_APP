using AVG_TASK_APP.Models;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories.Interface
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
        IEnumerable<UserModel> GetByContainEmail(string email);
        IEnumerable<UserModel> GetAll();
        bool VerifyAccount(string username, SecureString password);
        bool VerifyAccount(string username, string password);

    }
}

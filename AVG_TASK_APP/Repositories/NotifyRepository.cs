using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AVG_TASK_APP.Models;

namespace AVG_TASK_APP.DataAccess
{
    public class NotifyRepository : RepositoryBase, INotifyRepository
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

        public void Add(Notify notify)
        {
            AppDbContext dbContextTmp = dbContext;
            dbContextTmp.Notifies.Add(notify);
            dbContextTmp.SaveChanges();
        }

        public IEnumerable<Notify> GetAll()
        {
            return dbContext.Notifies.OrderByDescending(s => s.Pin.ToString() + s.Created_At).ToList();
        }
        public Notify GetById(int id)
        {
            return dbContext.Notifies.FirstOrDefault(s => s.Id == id);
        }

        public UserModel GetUserCreated(int id)
        {
            int idUser = dbContext.Notifies.FirstOrDefault(s => s.Id == id).Id_User;
            return dbContext.Users.FirstOrDefault(s => s.Id == idUser);
        }

        public void Remove(Notify notify)
        {
            throw new NotImplementedException();
        }

        public void Update(Notify notify)
        {
            throw new NotImplementedException();
        }
    }
}

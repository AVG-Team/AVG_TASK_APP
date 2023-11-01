using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories
{
    internal class TaskRepository : RepositoryBase, ITaskRepository
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

        public void Add(Models.Task task)
        {
            AppDbContext dbContextTemp = dbContext;
            dbContextTemp.Tasks.Add(task);
            dbContextTemp.SaveChanges();
        }

        public IEnumerable<Models.Task> GetAll()
        {
            return dbContext.Tasks.ToList();
        }

        public IEnumerable<Models.Task> GetAllForCard(int idCard)
        {
            return dbContext.Tasks.Where(s => s.Id_Card == idCard && s.Deleted_At == null).ToList();
        }

        public Models.Task GetById(int idTask)
        {
            return dbContext.Tasks.Where(s => s.Id == idTask).FirstOrDefault();
        }

        public void Remove(Models.Task task)
        {
            AppDbContext dbContextTmp = dbContext;
            task.Deleted_At = DateTime.Now;
            dbContextTmp.Tasks.Update(task);
            dbContextTmp.SaveChanges();
        }

        public void Update(Models.Task task)
        {
            AppDbContext dbContextTemp = dbContext;
            dbContextTemp.Tasks.Update(task);
            dbContextTemp.SaveChanges();
        }
    }
}

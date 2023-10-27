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
            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();
        }

        public IEnumerable<Models.Task> GetAll()
        {
            return dbContext.Tasks.ToList();
        }

        public IEnumerable<Models.Task> GetAllForCard(int idCard)
        {
            return dbContext.Tasks.Where(s => s.Id_Card == idCard).ToList();
        }

        public Models.Task GetById(int idTask)
        {
            return dbContext.Tasks.Where(s => s.Id == idTask).FirstOrDefault();
        }

        public void Remove(Models.Task task)
        {
            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();
        }

        public void Update(Models.Task task)
        {
            dbContext.Tasks.Update(task);
            dbContext.SaveChanges();
        }
    }
}

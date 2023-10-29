using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories
{
    public class TableRepository : RepositoryBase, ITableRepository
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
        public void Add(Table table)
        {
            dbContext.Tables.Add(table);
            dbContext.SaveChanges();
        }

        public IEnumerable<Table> GetAll(string sort = "desc")
        {
            if (sort.Equals("desc"))
                return dbContext.Tables.OrderByDescending(s => s.Created_At).ToList();
            else
                return dbContext.Tables.OrderBy(s => s.Created_At).ToList();
        }


        public IEnumerable<Table> GetAllForWorkspace(int idWorkspace, string sort = "desc")
        {
            if (sort.Equals("desc"))
                return dbContext.Tables.Where(s => s.Id_Workspace == idWorkspace).OrderByDescending(s => s.Created_At).ToList();
            else
                return dbContext.Tables.Where(s => s.Id_Workspace == idWorkspace).OrderBy(s => s.Created_At).ToList();
        }

        public Table GetById(int idTable)
        {
            return dbContext.Tables.Where(s => s.Id == idTable).FirstOrDefault();
        }

        public void Remove(Table table)
        {
            dbContext.Tables.Remove(table);
            dbContext.SaveChanges();
        }

        public void Update(Table table)
        {
            dbContext.Tables.Update(table);
            dbContext.SaveChanges();
        }
    }
}

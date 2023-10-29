using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
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
        public IEnumerable<Table> GetAllForUser(string sort = "desc")
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);
            var tables = dbContext.UserTables
                                .Where(s => s.Id_User == id)
                                .Select(s => s.Table);

            if (sort.Equals("desc"))
            {
                return tables.Where(s => s.Deleted_At == null).OrderByDescending(s => s.Created_At).ToList();
            }

            return tables.Where(s => s.Deleted_At == null).OrderBy(s => s.Created_At).ToList();
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

        public Workspace GetWorkspace(int idTable)
        {
            int idWorkspace = dbContext.Tables.FirstOrDefault(s => s.Id == idTable).Id_Workspace;
            return dbContext.Workspaces.FirstOrDefault(s => s.Id == idWorkspace);
        }

        public int GetRole(int idTable)
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);

            return dbContext.UserTables.FirstOrDefault(s => s.Id_Table == idTable && s.Id_User == id).Role;
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


using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.Repositories
{
    public class WorkspaceReposity : RepositoryBase, IWorkspaceReposity
    {
        public AppDbContext DbContext()
        {
            var connection = GetConnection();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connection, serverVersion);

            return new AppDbContext(optionsBuilder.Options);
        }
        public void Add(Workspace workspace)
        {
            var dbContext = DbContext();
            IUserRepository userRepository = new UserRepository();

            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            int id = int.Parse(identity.FindFirst("Id").ToString());

            dbContext.Workspaces.Add(workspace);
            UserWorkspace userWorkspace = new UserWorkspace()
            {
                Id_User = id,
                Id_Workspace = workspace.Id,
                Role = 1,
            };

            dbContext.UserWorkspaces.Add(userWorkspace);
            dbContext.SaveChanges();
        }

        public void Update(Workspace workspace)
        {
            var dbContext = DbContext();
            dbContext.Workspaces.Update(workspace);
            dbContext.SaveChanges();
        }

        public void Remove(Workspace workspace)
        {
            var dbContext = DbContext();
            dbContext.Workspaces.Remove(workspace);
            dbContext.SaveChanges();
        }

        public Workspace GetByNameForUser(string name, UserModel user)
        {
            var dbContext = DbContext();
            var workspace = dbContext.Workspaces
                .FirstOrDefault(s => s.Name == name && s.UserWorkspaces.Any(t => t.User == user));
            return workspace;
        }

        public Workspace GetByCode(string code)
        {
            var dbContext = DbContext();
            var workspace = dbContext.Workspaces
                .FirstOrDefault(s => s.Code == code);
            return workspace;
        }

        public IEnumerable<Workspace> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workspace> GetAllForUser()
        {
            throw new NotImplementedException();
        }
    }
}

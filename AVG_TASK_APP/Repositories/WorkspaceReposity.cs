using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

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

        public bool AddUserToWorkspace(Workspace workspace, UserModel user)
        {
            try
            {
                var dbContext = DbContext();
                UserWorkspace userWorkspace = new UserWorkspace()
                {
                    Id_User = user.Id,
                    Id_Workspace = workspace.Id,
                    Role = 0,
                };

                dbContext.UserWorkspaces.Add(userWorkspace);
                dbContext.SaveChanges();

                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }

        public Workspace GetById(int id)
        {
            var dbContext = DbContext();
            var workspace = dbContext.Workspaces
                .FirstOrDefault(s => s.Id == id);
            return workspace;
        }
    }
}

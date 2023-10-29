using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace AVG_TASK_APP.Repositories
{
    public class WorkspaceRepository : RepositoryBase, IWorkspaceRepository
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
        public void Add(Workspace workspace)
        {
            IUserRepository userRepository = new UserRepository();

            AppDbContext dbContextTmp = dbContext;
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);

            dbContextTmp.Workspaces.Add(workspace);
            dbContextTmp.SaveChanges();
            UserWorkspace userWorkspace = new UserWorkspace()
            {
                Id_User = id,
                Id_Workspace = workspace.Id,
                Role = 1,
            };

            dbContextTmp.UserWorkspaces.Add(userWorkspace);
            dbContextTmp.SaveChanges();
        }

        public void Update(Workspace workspace)
        {
            AppDbContext dbContextTmp = dbContext;
            dbContextTmp.Workspaces.Update(workspace);
            dbContextTmp.SaveChanges();
        }

        public void Remove(Workspace workspace)
        {
            AppDbContext dbContextTmp = dbContext;
            workspace.Deleted_At = DateTime.Now;
            dbContextTmp.Workspaces.Update(workspace);
            dbContextTmp.SaveChanges();
        }

        public Workspace GetByNameForUser(string name, UserModel user)
        {
            var workspace = dbContext.Workspaces
                .FirstOrDefault(s => s.Name == name && s.UserWorkspaces.Any(t => t.User == user));
            return workspace;
        }

        public Workspace GetByCode(string code)
        {
            var workspace = dbContext.Workspaces
                .FirstOrDefault(s => s.Code == code);
            return workspace;
        }

        public IEnumerable<Workspace> GetAll(string sort = "desc")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workspace> GetAllForUser(string sort = "desc")
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            int id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);
            var workspaces = dbContext.UserWorkspaces
                                .Where(s => s.Id_User == id)
                                .Select(s => s.Workspace);

            if (sort.Equals("desc"))
            {
                return workspaces.Where(s => s.Deleted_At == null).OrderByDescending(s => s.Created_At).ToList();
            }

            return workspaces.Where(s => s.Deleted_At == null).OrderBy(s => s.Created_At).ToList();
        }

        public bool AddUserToWorkspace(Workspace workspace, UserModel user)
        {
            try
            {

                if (dbContext.UserWorkspaces.FirstOrDefault(x => x.Id_Workspace == workspace.Id && x.Id_User == user.Id) != null)
                    return false;
                UserWorkspace userWorkspace = new UserWorkspace()
                {
                    Id_User = user.Id,
                    Id_Workspace = workspace.Id,
                    Role = 0,
                };

                dbContext.UserWorkspaces.Add(userWorkspace);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Workspace GetById(int id)
        {
            var workspace = dbContext.Workspaces
                .FirstOrDefault(s => s.Id == id);
            return workspace;
        }

        public IEnumerable<UserModel> GetUsersForWorkspace(int idWorkspace)
        {
            return dbContext.UserWorkspaces
                  .Where(s => s.Id_Workspace == idWorkspace)
                  .Select(x => x.User).ToList();
        }
    }
}

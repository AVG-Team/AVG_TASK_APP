using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    public class BoardViewModel : ViewModelBase
    {
        private IWorkspaceRepository workspaceReposity = new WorkspaceRepository();
        private ITableRepository tableRepository = new TableRepository();

        public BoardViewModel() { }

        public int getIdWorkspaceRecently()
        {
            var workspaces = workspaceReposity.GetAllForUser();
            if (workspaces == null)
                return -1;
            return workspaces.FirstOrDefault().Id;
        }

        public List<Table> GetTables(int idWorkspace)
        {
            List<Workspace> workspaces = (List<Workspace>)workspaceRepository.GetAllForUser();
            if(workspaces.Count == 0)
                return null;
            else if(workspaces.Count > 2) 
            {
                return workspaces.Take(2).ToList();
            }
            return workspaces.ToList();
        }

        public List<Table> GetTablesRecently()
        {
            List<Table> tables = (List<Table>)tableRepository.GetAllForUser();
            if (tables.Count == 0)
            {
               
            } 
            else if (tables.Count > 4)
            {
                return tables.Take(4).ToList();
            }
            return tables.ToList();
        }
    }
}

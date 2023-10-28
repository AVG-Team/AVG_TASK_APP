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
        private IWorkspaceReposity workspaceReposity = new WorkspaceReposity();
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
            return (List<Table>)tableRepository.GetAllForWorkspace(idWorkspace);
        }
    }
}

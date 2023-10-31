using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AVG_TASK_APP.ViewModels
{
    public class BoardViewModel : ViewModelBase
    {
        private IWorkspaceRepository workspaceReposity = new WorkspaceRepository();
        private ITableRepository tableRepository = new TableRepository();


        public BoardViewModel()
        {
        }

        public List<Workspace> GetWorkspacesRecently()
        {
            return (List<Workspace>)workspaceReposity.GetAllForUser().Take(2).ToList();
        }

        public List<Table> GetTablesRecently()
        {
            return (List<Table>)tableRepository.GetAllForUser().Take(4).ToList();
        }
    }
}

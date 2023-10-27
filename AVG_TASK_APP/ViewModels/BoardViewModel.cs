using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
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

        public BoardViewModel() { }

        public int getIdWorkspaceRecently()
        {
            var workspaces = workspaceReposity.GetAllForUser();
            if (workspaces == null)
                return -1;
            return workspaces.FirstOrDefault().Id;
        }
    }
}

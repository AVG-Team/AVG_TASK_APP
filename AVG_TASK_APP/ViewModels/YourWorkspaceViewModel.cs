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
    public class YourWorkspaceViewModel : ViewModelBase
    {
        private IWorkspaceRepository workspaceReposity;
        private string _idWorkspace;

        public string IdWorkspace
        {
            get => _idWorkspace;
            set { _idWorkspace = value; OnPropertyChanged(nameof(IdWorkspace)); }
        }

        public YourWorkspaceViewModel()
        {
            workspaceReposity = new WorkspaceRepository();
        }

        public string getName()
        {
            return workspaceReposity.GetById(int.Parse(IdWorkspace)).Name;
        }

        public int countMember()
        {
            return workspaceReposity.GetUsersForWorkspace(int.Parse(IdWorkspace)).Count();
        }
    }
}

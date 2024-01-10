using AVG_TASK_APP.Usercontrols;
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
    public class ItemWorkspace_ViewModel : ViewModelBase
    {
        private IWorkspaceRepository workspaceReposity;

        private string _itemIdWorkspace;

        public string ItemIdWorkspace
        {
            get { return _itemIdWorkspace; }
            set
            {
                _itemIdWorkspace = value;
                OnPropertyChanged(nameof(ItemIdWorkspace));
            }
        }

        public ItemWorkspace_ViewModel()
        {
            workspaceReposity = new WorkspaceRepository();
        }

        public string getName()
        {
            int idWorkspace = int.Parse(ItemIdWorkspace);
            Workspace workspace = workspaceReposity.GetById(idWorkspace);
            return workspace.Name;
        }
    }
}

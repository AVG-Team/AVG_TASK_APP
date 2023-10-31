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
        private IWorkspaceRepository workspaceRepository;
        private ITableRepository tableRepository;

        private string _idWorkspace;

        public string IdWorkspace
        {
            get => _idWorkspace;
            set { _idWorkspace = value; OnPropertyChanged(nameof(IdWorkspace)); }
        }

        public YourWorkspaceViewModel()
        {
            workspaceRepository = new WorkspaceRepository();
            tableRepository = new TableRepository();
        }

        public string getName()
        {
            return workspaceRepository.GetById(int.Parse(IdWorkspace)).Name;
        }

        public int countMember()
        {
            return workspaceRepository.GetUsersForWorkspace(int.Parse(IdWorkspace)).Count();
        }

        public List<Table> GetTables()
        {
            return (List<Table>)tableRepository.GetAllForWorkspace(int.Parse(IdWorkspace));
        }
    }
}

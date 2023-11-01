using AVG_TASK_APP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    internal class ManageTaskLayoutViewModel : ViewModelBase
    {
        private string _nameWorkspace;

        public string NameWorkspace
        {
            get
            {
                return _nameWorkspace;
            }
            set
            {
                _nameWorkspace = value;
                OnPropertyChanged(nameof(NameWorkspace));
            }
        }

        private TableRepository tableRepository;

        public ManageTaskLayoutViewModel()
        {
            tableRepository = new TableRepository();
        }

        public void getNameWorkspace(int idTable)
        {
            if (idTable != -1)
            {
                NameWorkspace = tableRepository.GetWorkspace(idTable).Name;
            }
        }
    }
}

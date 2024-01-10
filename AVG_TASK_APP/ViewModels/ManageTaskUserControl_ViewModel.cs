using AVG_TASK_APP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    internal class ManageTaskUserControl_ViewModel : ViewModelBase
    {
        private TableRepository tableRepository;
        private CardRepository cardRepository;
        private string _nameTable;

        public string NameTable
        {
            get => _nameTable;
            set
            {
                _nameTable = value;
                OnPropertyChanged(nameof(NameTable));
            }
        }


        public ManageTaskUserControl_ViewModel()
        {
            tableRepository = new TableRepository();
        }

        public void getNameTable(int idTable)
        {
            NameTable = tableRepository.GetById(idTable).Name;
        }
    }
}

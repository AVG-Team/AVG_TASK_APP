using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AVG_TASK_APP.ViewModels
{
    internal class RadioButtonBoard_ViewModel : ViewModelBase
    {
        private ITableRepository tableRepository;

        private string _idTable;
        private string _nameTable;

        public string IdTable
        {
            get { return _idTable; }
            set
            {
                _idTable = value;
                OnPropertyChanged(nameof(IdTable));
            }
        }

        public string NameTable
        {
            get { return _nameTable; }
            set
            {
                _nameTable = value;
                OnPropertyChanged(nameof(NameTable));
            }
        }


        public RadioButtonBoard_ViewModel()
        {
            tableRepository = new TableRepository();

        }



        public void getName()
        {
            int idTable = int.Parse(IdTable);
            Table table = tableRepository.GetById(idTable);

            NameTable = table.Name;
        }
    }
}

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
    internal class RadioButtonBoardViewModel : ViewModelBase
    {
        private ITableRepository tableRepository;

        private int _idTable;

        public int IdTable
        {
            get { return _idTable; }
            set
            {
                _idTable = value;
                OnPropertyChanged(nameof(IdTable));
            }
        }

        public RadioButtonBoardViewModel()
        {
            tableRepository = new TableRepository();
        }

        public string getName()
        {
            Table table = tableRepository.GetById(IdTable);
            return table.Name;
        }
    }
}

using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection;
using System.Reflection.Metadata;

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

        #region searchInit
        private string _txtSearch;
        private bool _isOpenSearch;

        public ObservableCollection<ItemMenuSearch> SelectedItems { get; set; } = new ObservableCollection<ItemMenuSearch>();

        private ObservableCollection<ItemMenuSearch> _menuSearchBoard;

        public ObservableCollection<ItemMenuSearch> MenuSearchBoard
        {
            get
            {
                if (_menuSearchBoard == null)
                {
                    _menuSearchBoard = new ObservableCollection<ItemMenuSearch>();
                }
                return _menuSearchBoard;
            }
            set
            {
                _menuSearchBoard = value;
                OnPropertyChanged(nameof(MenuSearchBoard));
            }
        }

        public bool IsOpenSearch
        {
            get { return _isOpenSearch; }
            set
            {
                _isOpenSearch = value;
                OnPropertyChanged(nameof(IsOpenSearch));
            }
        }

        public string ValueSearch
        {
            get { return _txtSearch; }
            set
            {
                _txtSearch = value;
                OnPropertyChanged(nameof(ValueSearch));
                UpdateSearch();
            }
        }

        private void UpdateSearch()
        {
            ObservableCollection<ItemMenuSearch> menuTmp = new ObservableCollection<ItemMenuSearch>();

            IsOpenSearch = true;
            if (MenuSearchBoard != null)
                MenuSearchBoard.Clear();

            string search = ValueSearch.Trim();

            List<Table> tables = (search.Length == 0) ? (List<Table>)tableRepository.GetAllForUser() : (List<Table>)tableRepository.GetByContainName(search);

            if (tables == null)
                return;

            foreach (Table table in tables)
            {
                int role = tableRepository.GetRole(table.Id);
                ItemMenuSearch itemMenuSearch = new ItemMenuSearch(table.Id.ToString(), table.Name, role);
                itemMenuSearch.PreviewMouseDown += ItemMenuSearch_PreviewMouseDown;
                menuTmp.Add(itemMenuSearch);
            }

            MenuSearchBoard = menuTmp;
        }

        private void ItemMenuSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemMenuSearch item = sender as ItemMenuSearch;
            int idTable = int.Parse(item.txtValue.Text);
            ManageTaskLayout manage = new ManageTaskLayout(idTable);
            manage.Show();


            foreach (Window window in Application.Current.Windows)
            {
                if (window is ManageTaskLayout && window != manage)
                {
                    window.Close();
                    return;
                }
            }
        }
        #endregion

        private TableRepository tableRepository;

        public ManageTaskLayoutViewModel()
        {
            tableRepository = new TableRepository();
        }

        public List<Table> getStarList()
        {
            return tableRepository.GetAllForUser().Where(s => s.Pin == true).ToList();
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

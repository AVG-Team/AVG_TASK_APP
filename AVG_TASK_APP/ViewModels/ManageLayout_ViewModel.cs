using AVG_TASK_APP.Usercontrols;
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
using AVG_TASK_APP.Views.Layouts;

namespace AVG_TASK_APP.ViewModels
{
    internal class ManageLayout_ViewModel : ViewModelBase
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

        public ObservableCollection<ItemMenuSearch_UserControl> SelectedItems { get; set; } = new ObservableCollection<ItemMenuSearch_UserControl>();

        private ObservableCollection<ItemMenuSearch_UserControl> _menuSearchBoard;

        public ObservableCollection<ItemMenuSearch_UserControl> MenuSearchBoard
        {
            get
            {
                if (_menuSearchBoard == null)
                {
                    _menuSearchBoard = new ObservableCollection<ItemMenuSearch_UserControl>();
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
            ObservableCollection<ItemMenuSearch_UserControl> menuTmp = new ObservableCollection<ItemMenuSearch_UserControl>();

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
                ItemMenuSearch_UserControl itemMenuSearch = new ItemMenuSearch_UserControl(table.Id.ToString(), table.Name, role);
                itemMenuSearch.PreviewMouseDown += ItemMenuSearch_PreviewMouseDown;
                menuTmp.Add(itemMenuSearch);
            }

            MenuSearchBoard = menuTmp;
        }

        private void ItemMenuSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemMenuSearch_UserControl item = sender as ItemMenuSearch_UserControl;
            int idTable = int.Parse(item.txtValue.Text);
            Manage_Layout manage = new Manage_Layout(idTable);
            manage.Show();


            foreach (Window window in Application.Current.Windows)
            {
                if (window is Manage_Layout && window != manage)
                {
                    window.Close();
                    return;
                }
            }
        }
        #endregion

        private TableRepository tableRepository;

        public ManageLayout_ViewModel()
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

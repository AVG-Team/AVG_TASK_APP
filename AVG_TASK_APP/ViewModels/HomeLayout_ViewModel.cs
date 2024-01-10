using AVG_TASK_APP.Usercontrols;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views.Forms;
using AVG_TASK_APP.Views.Layouts;
using AVG_TASK_APP.Views.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Claims;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.ViewModels
{
    public class HomeLayout_ViewModel : ViewModelBase
    {
        private IWorkspaceRepository workspaceReposity;

        private UserAccount currentUserAccount;
        private IUserRepository userRepository;
        private ITableRepository tableRepository;

        private string _userAccountName;
        private string _userAccountImage;

        #region searchInit
        private string _txtSearch;
        private bool _isOpenSearch;

        public ObservableCollection<ItemMenuSearch_UserControl> SelectedItems { get; set; } = new ObservableCollection<ItemMenuSearch_UserControl>();

        private ObservableCollection<ItemMenuSearch_UserControl> _menuSearchBoard;

        #endregion

        private ObservableCollection<ItemWorkspace_UserControl> _workspaces;

        public ObservableCollection<ItemWorkspace_UserControl> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<ItemWorkspace_UserControl>();
                }
                return _workspaces;

            }
            set
            {
                _workspaces = value;
                OnPropertyChanged(nameof(Workspaces));
            }
        }
        #region Search Init
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

        #endregion

        public ICommand HomeLayoutLoaded { get; }
        public ICommand CreateWorkSpaceCommand { get; }
        public ICommand ShowInformationUserCommand { get; }

        public HomeLayout_ViewModel()
        {
            userRepository = new UserRepository();
            tableRepository = new TableRepository();
            workspaceReposity = new WorkspaceRepository();
            currentUserAccount = new UserAccount();
            HomeLayoutLoaded = new ViewModelCommand(ExcuteLoadedCommand);
            CreateWorkSpaceCommand = new ViewModelCommand(ExcuteCreateWorkspaceCommand);
            ShowInformationUserCommand = new ViewModelCommand(ExcuteCreateWorkspaceCommand);

            LoadCurrentUserData();
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
            int idTable = int.Parse(item.Value);
            Manage_Layout manage = new Manage_Layout(idTable);
            manage.Show();


            foreach (Window window in Application.Current.Windows)
            {
                if (window is Home_Layout)
                {
                    window.Close();
                }
            }
        }

        #region Create Workspace

        private void ExcuteCreateWorkspaceCommand(object obj)
        {
            CreateWorkspace_View createWorkspaceView = new CreateWorkspace_View();

            createWorkspaceView.ShowDialog();
            if (createWorkspaceView.Visibility == Visibility.Visible)
            {
                loadItemWorkspace();
            }
        }

        private void ExcuteLoadedCommand(object obj)
        {
            loadItemWorkspace();
        }

        private void loadItemWorkspace()
        {
            Workspaces = new ObservableCollection<ItemWorkspace_UserControl>();

            List<Workspace> workspaces = (List<Workspace>)workspaceReposity.GetAllForUser();
            if (!workspaces.Any())
            {
                return;
            }
            foreach (Workspace workspace in workspaces)
            {
                ItemWorkspace_UserControl itemWorkspace = new ItemWorkspace_UserControl(workspace.Id);
                Workspaces.Add((ItemWorkspace_UserControl)itemWorkspace.userControl);
            }
        }

        public string UserAccountName
        {
            get { return _userAccountName; }
            set
            {
                _userAccountName = value;
                OnPropertyChanged(nameof(UserAccountName));
            }
        }

        public string UserAccountImage
        {
            get => _userAccountImage;
            set
            {
                _userAccountImage = value;
                OnPropertyChanged(nameof(UserAccountImage));
            }
        }

        private void LoadCurrentUserData()
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity != null)
            {
                currentUserAccount.Id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);
                UserModel userTmp = userRepository.GetById(currentUserAccount.Id);
                currentUserAccount.Name = userTmp.Name;
                currentUserAccount.Email = userTmp.Email;
                currentUserAccount.Level = userTmp.Level;

                UserAccountName = currentUserAccount.Name;

                if (currentUserAccount.Level == 0)
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
                }
                else
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
                }

                string email = userTmp.Email;
            }
            else
            {
                Login_View loginView = new Login_View();
                loginView.Show();


                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Home_Layout)
                    {
                        window.Close();
                        return;
                    }
                }
                return;
            }
        }
        #endregion
    }
}

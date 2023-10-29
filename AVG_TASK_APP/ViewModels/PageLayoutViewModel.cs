using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
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
    public class PageLayoutViewModel : ViewModelBase
    {
        private IWorkspaceRepository workspaceReposity;

        private UserAccount currentUserAccount;
        private IUserRepository userRepository;

        private string _userAccountName;
        private string _userAccountImage;

        private ObservableCollection<itemWorkspace> _workspaces;

        public ObservableCollection<itemWorkspace> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<itemWorkspace>();
                }
                return _workspaces;

            }
            set
            {
                _workspaces = value;
                OnPropertyChanged(nameof(Workspaces));
            }
        }

        public ICommand PageLayoutLoaded { get; }
        public ICommand CreateWorkSpaceCommand { get; }
        public ICommand ShowInformationUserCommand { get; }

        public PageLayoutViewModel()
        {
            userRepository = new UserRepository();
            workspaceReposity = new WorkspaceRepository();
            currentUserAccount = new UserAccount();
            PageLayoutLoaded = new ViewModelCommand(ExcuteLoadedCommand);
            CreateWorkSpaceCommand = new ViewModelCommand(ExcuteCreateWorkspaceCommand);
            ShowInformationUserCommand = new ViewModelCommand(ExcuteCreateWorkspaceCommand);
            LoadCurrentUserData();
        }

        private void ExcuteCreateWorkspaceCommand(object obj)
        {
            CreateWorkspaceView createWorkspaceView = new CreateWorkspaceView();

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
            Workspaces = new ObservableCollection<itemWorkspace>();

            List<Workspace> workspaces = (List<Workspace>)workspaceReposity.GetAllForUser();
            if (!workspaces.Any())
            {
                return;
            }
            foreach (Workspace workspace in workspaces)
            {
                itemWorkspace itemWorkspace = new itemWorkspace(workspace.Id);
                Workspaces.Add(itemWorkspace.userControl);
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
                else if (currentUserAccount.Level == 1)
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
                }

                string email = userTmp.Email;
            }
            else
            {
                LoginView loginView = new LoginView();
                loginView.Show();


                foreach (Window window in Application.Current.Windows)
                {
                    if (window is PageLayout)
                    {
                        window.Close();
                        return;
                    }
                }
                return;
            }
        }
    }
}

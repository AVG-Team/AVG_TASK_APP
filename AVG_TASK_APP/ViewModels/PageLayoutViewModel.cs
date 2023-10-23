using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using System;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Claims;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace AVG_TASK_APP.ViewModels
{
    public class PageLayoutViewModel : ViewModelBase
    {

        private UserAccount currentUserAccount;
        private IUserRepository userRepository;
        private string _userAccountName;
        private string _userAccountImage;

        public PageLayoutViewModel()
        {
            userRepository = new UserRepository();
            currentUserAccount = new UserAccount();
            LoadCurrentUserData();
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
                currentUserAccount.Name = identity.Name;
                currentUserAccount.Email = identity.Claims.FirstOrDefault(s => s.Type == "Email").Value;
                currentUserAccount.Level = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Level").Value);

                UserAccountName = currentUserAccount.Name;
                if (currentUserAccount.Level == 0)
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
                }
                else if (currentUserAccount.Level == 1)
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
                }

                string email = Thread.CurrentPrincipal.Identity.Name;
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

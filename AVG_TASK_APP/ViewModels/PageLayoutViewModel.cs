using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            var user = userRepository.GetByEmail(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                currentUserAccount.Id = user.Id;
                currentUserAccount.Name = user.Name;
                currentUserAccount.Email = user.Email;
                currentUserAccount.Level = user.Level;

                UserAccountName = currentUserAccount.Name;
                if (currentUserAccount.Level == 0)
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
                }
                else if (currentUserAccount.Level == 1)
                {
                    UserAccountImage = "/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
                }
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

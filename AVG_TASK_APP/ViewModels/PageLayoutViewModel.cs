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

namespace AVG_TASK_APP.ViewModels
{
    class PageLayoutViewModel: ViewModelBase
    {
        private UserAccount _currentUserAccount;
        private IUserRepository userRepository;
        
        public UserAccount CurrentUserAccount {
            get 
            {
                return _currentUserAccount; 
            }

            set { 
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public PageLayoutViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccount();
            LoadCurrentUserData();
        }
        
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByEmail(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Id = user.Id;
                CurrentUserAccount.Name = user.Name;
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.Level = user.Level;
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

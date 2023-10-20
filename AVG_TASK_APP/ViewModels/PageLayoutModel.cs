using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
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
    class PageLayoutModel: ViewModelBase
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
        public PageLayoutModel()
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

                    CurrentUserAccount.Username = user.Name;
                    CurrentUserAccount.DisplayName = $"Welcome {user.Id} {user.Name} ;)";
                    CurrentUserAccount.ProfiePiture = null;
                  
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            }
        }
    }
}

using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        public event EventHandler? CanExecuteChanged;

        //properties
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // --> Command 
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructor 

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExcuteLoginCommand, CanExcuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExcuteRecoverPassCommand("", ""));
        }
        private void ExcuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExcuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExcuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.verifyAccount(Username, Password);
            if (isValidUser)
            {
                MessageBoxView msb = new MessageBoxView();
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);

                byte[] salt = userRepository.GetByEmail(Username).Salt;
                string passwordTmp = userRepository.HashPassword(Password, salt);

                var assembly = Assembly.GetExecutingAssembly();
                var registryKey = Registry.CurrentUser.CreateSubKey("Software\\" + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title + "\\Login");
                registryKey.SetValue("Username", Username);
                registryKey.SetValue("Password", passwordTmp);

                PageLayout pageLayout = new PageLayout();
                pageLayout.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is LoginView)
                    {
                        window.Close();
                        IsViewVisible = false;
                        return;
                    }
                }
            }
            else
            {
                ErrorMessage = "*Invalid username or password";
            }
        }

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }

    }
}

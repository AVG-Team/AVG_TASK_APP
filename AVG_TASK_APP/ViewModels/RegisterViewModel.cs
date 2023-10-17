using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Mail;
using System.Windows;
using System.Runtime.InteropServices;

namespace AVG_TASK_APP.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private SecureString _repeatPassword;
        private string _phone;
        private string _fullName;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private bool _isCheckedPrivacy = false;

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

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
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

        public SecureString RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
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

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
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

        public bool IsCheckedPrivacy
        {
            get { return _isCheckedPrivacy; }
            set
            {
                _isCheckedPrivacy = value;
                OnPropertyChanged(nameof(IsCheckedPrivacy));
            }
        }

        // --> Command 
        public ICommand RegisterCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructor 

        public RegisterViewModel()
        {
            userRepository = new UserRepository();
            RegisterCommand = new ViewModelCommand(ExcuteRegisterCommand, CanExcuteRegisterCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExcuteRecoverPassCommand("", ""));
        }
        private void ExcuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool CanExcuteRegisterCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || !IsValid(Username) ||
                string.IsNullOrWhiteSpace(FullName) || FullName.Length < 3 ||
                string.IsNullOrWhiteSpace(Phone) || Phone.Length < 9 ||
                Password == null || Password.Length < 3 ||
                RepeatPassword == null || RepeatPassword.Length < 3 || !IsCheckedPrivacy)
            {
                validData = false;
            }
            else
                validData = true;
            return validData;
        }

        private void ExcuteRegisterCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "*Invalid";
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

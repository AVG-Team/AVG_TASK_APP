﻿using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views.Layouts;
using AVG_TASK_APP.Views.Notifies;
using AVG_TASK_APP.Views.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.ViewModels
{
    public class Login_ViewModel : ViewModelBase
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

        public Login_ViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExcuteLoginCommand, CanExcuteLoginCommand);
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

        private bool CanExcuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || !IsValid(Username) ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExcuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.VerifyAccount(Username, Password);
            if (isValidUser)
            {
                MessageBox_View msb = new MessageBox_View();

                UserModel user = userRepository.GetByEmail(Username);
                var currentPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

                if (currentPrincipal is { })
                {
                    // Cập nhật claims trong principal
                    var identity = currentPrincipal.Identities.First(); // Lấy identity đầu tiên
                                                                        // Xóa claims hiện có và thêm claims mới
                    identity.Claims.ToList().ForEach(c => identity.RemoveClaim(c));
                    identity.AddClaim(new Claim("Id", user.Id.ToString()));
                    identity.AddClaim(new Claim("Level", user.Level.ToString()));
                }
                else
                {
                    // Tạo principal mới nếu chưa tồn tại
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Level", user.Level.ToString()),
                    }, "sectionads");

                    var principal = new ClaimsPrincipal(identity);
                    Thread.CurrentPrincipal = principal;
                    AppDomain.CurrentDomain.SetThreadPrincipal(principal);
                }

                var salt = userRepository.GetByEmail(Username).Salt;
                string passwordTmp = userRepository.HashPassword(Password, salt);

                var assembly = Assembly.GetExecutingAssembly();
                var registryKey = Registry.CurrentUser.CreateSubKey("Software\\" + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title + "\\Login");
                registryKey.SetValue("Username", Username);
                registryKey.SetValue("Password", passwordTmp);

                Home_Layout Home_Layout = new();
                Home_Layout.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Login_View)
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

        public bool CanExecute(object? parameter) => throw new NotImplementedException();

        public void Execute(object? parameter) => throw new NotImplementedException();




    }
}

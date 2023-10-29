using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
    public class UserInformationViewModel : ViewModelBase
    {
        private IUserRepository userRepository = new UserRepository();

        private UserModel _userCurrent = new UserModel();

        private string _userAvatar;
        private string _name;
        private SecureString _password;
        private SecureString _repeatPassword;
        private string _email;
        private string _phone;

        public string UserAvatar
        {
            get
            {
                return _userAvatar;
            }
            set
            {
                _userAvatar = value;
                OnPropertyChanged(nameof(UserAvatar));
            }
        }

        public UserModel UserCurrent
        {
            get
            {
                return _userCurrent;
            }
            set
            {
                _userCurrent = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
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

        public ICommand SaveCommand { get; }

        public UserInformationViewModel()
        {
            userRepository = new UserRepository();

            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (identity != null)
            {
                UserCurrent.Id = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);

                UserCurrent = userRepository.GetById(_userCurrent.Id);

                if (UserCurrent.Level == 0)
                {
                    UserAvatar = "/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
                }
                else if (UserCurrent.Level == 1)
                {
                    UserAvatar = "/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
                }
            }

            loadCurrentUserData();
            SaveCommand = new ViewModelCommand(ExcuteSaveCommand, CanExcuteCommand);
        }

        public bool IsValidEmail(string emailaddress)
        {
            if (emailaddress == null) return false;

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

        private bool CanExcuteCommand(object obj)
        {
            bool validData = false;
            if (string.IsNullOrWhiteSpace(Email) || Email.Length < 3 || !IsValidEmail(Email) ||
            string.IsNullOrWhiteSpace(Name) || Name.Length < 3 ||
                string.IsNullOrWhiteSpace(Phone) || Phone.Length < 9)
            {
                validData = false;
            }
            else
                validData = true;
            return validData;
        }

        private bool CheckRepeatPassword(SecureString password, SecureString repeatPassword)
        {
            if (password == null || repeatPassword == null) return false;
            IntPtr unmanagedString = IntPtr.Zero;
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(password);
            string passwordTmp = Marshal.PtrToStringUni(unmanagedString);

            IntPtr unmanagedStringRepeat = IntPtr.Zero;
            unmanagedStringRepeat = Marshal.SecureStringToGlobalAllocUnicode(repeatPassword);
            string repeatPasswordTmp = Marshal.PtrToStringUni(unmanagedStringRepeat);
            return passwordTmp == repeatPasswordTmp ? true : false;
        }

        private void ExcuteSaveCommand(object obj)
        {
            MessageBoxView msb = new MessageBoxView();
            bool isChange = false;

            if (Name != UserCurrent.Name)
            {
                UserCurrent.Name = Name;
                isChange = true;
            }

            if (Phone != UserCurrent.PhoneNumber)
            {
                UserCurrent.PhoneNumber = Phone;
                isChange = true;
            }
            if (CheckRepeatPassword(Password, RepeatPassword) && !userRepository.verifyAccount(Email, Password))
            {
                isChange = true;
                byte[] salt = userRepository.GenerateSalt();

                string hashedPassword = userRepository.HashPassword(Password, salt);

                UserCurrent.Salt = salt;
                UserCurrent.Password = hashedPassword;
            }

            if (isChange)
            {
                userRepository.Update(UserCurrent);
                msb.Show("Save Information Successfully");

                return;
            }
            msb.Show("No information has been changed");
        }

        private void loadCurrentUserData()
        {
            Name = UserCurrent.Name;
            Email = UserCurrent.Email;
            Phone = UserCurrent.PhoneNumber;
        }
    }
}

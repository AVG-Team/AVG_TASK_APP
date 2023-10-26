using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using C1.WPF.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AVG_TASK_APP.ViewModels
{
    public class AddMemberToWorkSpaceViewModel : ViewModelBase
    {
        //Fields
        private string _emailUsers;
        private string _idWorkspace;
        private string _errorMessage;
        private bool _isOpen;
        private string _valueEmail;
        public ObservableCollection<ItemMenuSearch> SelectedItems { get; set; } = new ObservableCollection<ItemMenuSearch>();

        private ObservableCollection<ItemMenuSearch> _menuSearch;

        private IUserRepository userRepository;
        private IWorkspaceReposity workspaceReposity;

        public List<UserModel> Users { get; set; }

        public event EventHandler? CanExecuteChanged;

        //properties
        public string EmailUsers
        {
            get { return _emailUsers; }
            set
            {
                _emailUsers = value;
                OnPropertyChanged(nameof(EmailUsers));
                UpdateSearch();
            }
        }

        public string IdWorkspace
        {
            get { return _idWorkspace; }
            set
            {
                _idWorkspace = value;
                OnPropertyChanged(nameof(IdWorkspace));
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

        public ObservableCollection<ItemMenuSearch> MenuSearch
        {
            get
            {
                if (_menuSearch == null)
                {
                    _menuSearch = new ObservableCollection<ItemMenuSearch>();
                }
                return _menuSearch;
            }
            set
            {
                _menuSearch = value;
                OnPropertyChanged(nameof(MenuSearch));
            }
        }

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        public string ValueEmail
        {
            get 
            {
                return _valueEmail;
            }
            set
            {
                _valueEmail = value;
                OnPropertyChanged(nameof(ValueEmail));
            }
        }

        // --> Command 
        public ICommand InvitationCommand { get; }

        // Constructor 

        public AddMemberToWorkSpaceViewModel()
        {
            userRepository = new UserRepository();
            workspaceReposity = new WorkspaceReposity();
            InvitationCommand = new ViewModelCommand(ExcuteInvitationCommand, CanExcuteInvitationCommand);
        }

        private bool IsSelected(string email)
        {
            if (ValueEmail == null)
                return false;
            string[] tmp = ValueEmail.Split(";");
            foreach (string e in tmp)
            {
                if (e == email)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateSearch()
        {
            IsOpen = true;
            if (MenuSearch != null)
                MenuSearch.Clear();

            string search = EmailUsers.Trim();
            if (search.Contains(";"))
            {
                string[] emailParts = search.Split(';');
                search = emailParts[emailParts.Length - 1].Trim();
            }

            List<UserModel> users = (search.Length == 0) ? (List<UserModel>)userRepository.GetAll() : (List<UserModel>)userRepository.GetByContainEmail(search);

            if (users == null)
                return;

            if (users == null)
                return;

            foreach (UserModel user in users)
            {
                if (!IsSelected(user.Email))
                {
                    ItemMenuSearch itemMenuSearch = new ItemMenuSearch(user.Email, user.Name, user.Level);
                    itemMenuSearch.PreviewMouseDown += ItemMenuSearch_PreviewMouseDown;
                    MenuSearch.Add(itemMenuSearch);
                }
            }
        }

        private void ItemMenuSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemMenuSearch item = (ItemMenuSearch)sender;
            string emailTmp = item.Email;
            ValueEmail += item.Email + ";";
            EmailUsers = ValueEmail;
            IsOpen = false;
        }

        public bool IsValidEmail(string emailaddress)
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

        private bool CanExcuteInvitationCommand(object obj)
        {
            bool validData;
            bool validDataEmail = true;
            if (EmailUsers == null)
                return false;
            if (EmailUsers.Contains(";"))
            {
                string[] tmp = EmailUsers.Split(';');
                string[] newArray = tmp.Take(tmp.Length - 1).ToArray();
                foreach (string s in newArray)
                {
                    if (!IsValidEmail(s))
                    {
                        validDataEmail = false;
                    }
                }

            }
            int tmp12312 = int.Parse(IdWorkspace);
            if (string.IsNullOrWhiteSpace(EmailUsers) || EmailUsers.Length < 3 ||! validDataEmail || int.Parse(IdWorkspace) == -1)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void processAdd(string email , int idWorkspace)
        {
            UserModel user = userRepository.GetByEmail(email);
            if (user == null)
            {
                return;
            }

            Workspace wsp = workspaceReposity.GetById(idWorkspace);
            workspaceReposity.AddUserToWorkspace(wsp, user);
        }

        private void ExcuteInvitationCommand(object obj)
        {
            bool isSuccess = false;
            if (EmailUsers.Contains(";"))
            {
                string[] tmp = EmailUsers.Split(';');
                string[] newArray = tmp.Take(tmp.Length - 1).ToArray();
                foreach (string email in newArray)
                {
                    try
                    {
                        processAdd(email, int.Parse(IdWorkspace));
                        isSuccess = true;
                    } catch (Exception ex)
                    {
                        isSuccess = false;
                        ErrorMessage += " Email " + email + " Error Unknown, Please Again";
                    }
                }
            }
            if(isSuccess)
            {
                MessageBoxView msb = new MessageBoxView();
                msb.Show("Add Successfully");

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is CreateWorkspaceView)
                    {
                        window.Close();
                        return;
                    }
                }
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

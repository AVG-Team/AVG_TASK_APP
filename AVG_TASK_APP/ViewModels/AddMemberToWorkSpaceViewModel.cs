using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP.ViewModels
{
    public class AddMemberToWorkSpaceViewModel : ViewModelBase
    {
        //Fields
        private string _emailUsers;
        private string _idWorkspace;
        private string _errorMessage;

        private IUserRepository userRepository;
        private IWorkspaceReposity workspaceReposity;

        public event EventHandler? CanExecuteChanged;

        //properties
        public string EmailUsers
        {
            get { return _emailUsers; }
            set
            {
                _emailUsers = value;
                OnPropertyChanged(nameof(EmailUsers));
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

        // --> Command 
        public ICommand InvitationCommand { get; }

        // Constructor 

        public AddMemberToWorkSpaceViewModel()
        {
            userRepository = new UserRepository();
            workspaceReposity = new WorkspaceReposity();
            InvitationCommand = new ViewModelCommand(ExcuteInvitationCommand, CanExcuteInvitationCommand);
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
                        MessageBoxView msb = new MessageBoxView();
                        msb.Show("Add Successfully");
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

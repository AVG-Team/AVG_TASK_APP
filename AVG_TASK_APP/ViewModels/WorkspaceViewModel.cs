using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class WorkspaceViewModel : ViewModelBase
    {
        //Fields
        private string _workspaceName;
        private DateTime _dateStart;
        private bool _isCheckedPublic = true;
        private bool _isCheckedNotPublic = false;
        private string _code;
        private bool _isViewVisible = true;

        private IWorkspaceReposity workspaceReposity;

        public string WorkspaceName
        {
            get => _workspaceName;
            set
            {
                _workspaceName = value;
                OnPropertyChanged(nameof(WorkspaceName));
            }
        }

        public DateTime DateStart
        {
            get => _dateStart;
            set
            {
                _dateStart = value;
                OnPropertyChanged(nameof(DateStart));
            }
        }

        private void getDateStart()
        {
            OnPropertyChanged(nameof(WorkspaceName));
        }

        public bool IsCheckedPublic 
        {
            get => _isCheckedPublic;
            set
            {
                _isCheckedPublic = value;
                OnPropertyChanged(nameof(IsCheckedPublic));
            }
        }

        public bool IsCheckedNotPublic
        {
            get => _isCheckedNotPublic;
            set
            {
                _isCheckedNotPublic = value;
                OnPropertyChanged(nameof(IsCheckedNotPublic));
            }
        }

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }


        public event EventHandler? CanExecuteChanged;

        // --> Command 
        public ICommand ContinueCommand { get; }

        // Constructor 

        public WorkspaceViewModel()
        {
            workspaceReposity = new WorkspaceReposity();
            ContinueCommand = new ViewModelCommand(ExcuteCreateWorkspaceCommand, CanExcuteCreateWorkspaceCommand);
        }

        private bool checkCodeExist(string code)
        {
            return workspaceReposity.GetByCode(code) == null ? false : true;
        }

        private bool CanExcuteCreateWorkspaceCommand(object obj)
        {
            bool validData = false;
            if (string.IsNullOrWhiteSpace(WorkspaceName) || WorkspaceName.Length < 3
                || string.IsNullOrWhiteSpace(Code) || Code.Length < 3 || checkCodeExist(Code))
            {
                validData = false;
            }
            else
                validData = true;
            return validData;
        }

        private void ExcuteCreateWorkspaceCommand(object obj)
        {
            try
            {
                Workspace workspace = new Workspace()
                {
                    Name = WorkspaceName,
                    Date = DateStart,
                    Visible = IsCheckedPublic,
                    Code = Code,
                };

                workspaceReposity.Add(workspace);
                MessageBoxView msb = new MessageBoxView();
                msb.Show("Add Workspace Successfully");

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is CreateWorkspaceView)
                    {
                        AddMemberToWorkspace addMemberToWorkspace = new AddMemberToWorkspace(workspace.Id);
                        window.Content = addMemberToWorkspace;
                        window.Width = addMemberToWorkspace.Width;
                        window.Height = addMemberToWorkspace.Height;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Unknow, Please try again");
            }
        }
    }
}

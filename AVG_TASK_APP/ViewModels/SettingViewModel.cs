using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    class SettingViewModel : ViewModelBase
    {
        private bool _visible;
        private string _textVisible;
        private string _nameWorkspace;
        private string _textVisibleChange;

        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }

        public string NameWorkspace
        {
            get
            {
                return _nameWorkspace;
            }
            set
            {
                _nameWorkspace = value;
                OnPropertyChanged("NameWorkspace");
            }
        }

        public string TextVisibleChange
        {
            get
            {
                return _textVisibleChange;

            }
            set
            {
                _textVisibleChange = value;
                OnPropertyChanged("TextVisibleChange");
            }
        }

        public string TextVisible
        {
            get
            {
                return _textVisible;
            }
            set
            {
                _textVisible = value;
                OnPropertyChanged("TextVisible");
            }
        }

        public WorkspaceRepository workspaceRepository;

        public SettingViewModel()
        {
            workspaceRepository = new WorkspaceRepository();
        }

        public void getName(int idWorkspace)
        {
            string name = workspaceRepository.GetById(idWorkspace).Name;

            NameWorkspace = name.Length > 14 ? name.Substring(0, 10) + "..." : name;
        }
        public void GetTextVisible(bool visible)
        {
            if (visible)
            {
                TextVisible = "Public";
            }
            else
            {
                TextVisible = "Private";
            }
        }

        public void ChangeVisible(bool visible)
        {
            if (visible)
            {
                TextVisibleChange = "Private";
            }
            else
            {
                TextVisibleChange = "Public";
            }
        }
    }
}

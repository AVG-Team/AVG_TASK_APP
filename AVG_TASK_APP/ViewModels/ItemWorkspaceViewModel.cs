﻿using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    public class ItemWorkspaceViewModel : ViewModelBase
    {
        private IWorkspaceReposity workspaceReposity;

        private string _itemIdWorkspace;

        public string ItemIdWorkspace
        {
            get { return _itemIdWorkspace; }
            set
            {
                _itemIdWorkspace = value;
                OnPropertyChanged(nameof(ItemIdWorkspace));
            }
        }

        public ItemWorkspaceViewModel()
        {
            workspaceReposity = new WorkspaceReposity();
        }

        public string getName()
        {
            int idWorkspace = int.Parse(ItemIdWorkspace);
            Workspace workspace = workspaceReposity.GetById(idWorkspace);
            return workspace.Name;
        }
    }
}

﻿using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
using AVG_TASK_APP.Views.Forms;
using AVG_TASK_APP.Views.Notifies;
using FontAwesome.WPF;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for BoardUserControl.xaml
    /// </summary>
    public partial class BoardUserControl : System.Windows.Controls.UserControl
    {
        private ITableRepository tableRepository;
        private IWorkspaceRepository workspaceRepository;

        private List<Table> tables;

        public int idWorkspace { get; set; }

        public BoardUserControl(int idWorkspace)
        {
            InitializeComponent();

            tableRepository = new TableRepository();
            workspaceRepository = new WorkspaceRepository();

            this.idWorkspace = idWorkspace;

            Reload();
        }

        public void Reload()
        {
            loadDataWorkspace();
            loadBoard();
        }
        public void loadDataWorkspace()
        {
            Workspace workspace = workspaceRepository.GetById(idWorkspace);
            nameWorkspace.Text = workspace.Name;
            if (workspace.Visible == true)
            {
                visibilityWorkspace.Text = "Public";
                iconVisibilityWorkspace.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.Globe;
            }
            else
            {
                visibilityWorkspace.Text = "Private";
                iconVisibilityWorkspace.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.Lock;
            }
        }

        public void loadBoard()
        {
            areaBoard.Children.Clear();
            int i = 0;
            List<Table> tableList = (List<Table>)tableRepository.GetAllForWorkspace(idWorkspace);
            foreach (Table table in tableList)
            {

                BtnBoard_UserControl btnTable = new BtnBoard_UserControl(table.Id, table.Name);

                btnTable.Margin = new Thickness(5);

                if (i % 4 == 0)
                {
                    StackPanel rowPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(0, 10, 0, 0),
                    };

                    areaBoard.Children.Add(rowPanel);
                }

                if (areaBoard.Children.Count > 0 && areaBoard.Children[areaBoard.Children.Count - 1] is StackPanel rowStackPanel)
                {
                    rowStackPanel.Children.Add(btnTable);
                }

                i++;
            }
            BtnBoard_UserControl btnTableAdd = new BtnBoard_UserControl(-1, "Add New Table");

            btnTableAdd.Margin = new Thickness(5);

            if (i % 4 == 0)
            {
                StackPanel rowPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 0),
                };

                areaBoard.Children.Add(rowPanel);
            }

            if (areaBoard.Children.Count > 0 && areaBoard.Children[areaBoard.Children.Count - 1] is StackPanel rowStackPanelAdd)
            {
                rowStackPanelAdd.Children.Add(btnTableAdd);
            }
        }

        private void BtnTableAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CreateBoard_View createBoard = new CreateBoard_View(idWorkspace);
            createBoard.Show();
        }

        private void BtnTable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddNewButton()
        {
            BtnBoard_UserControl newBoard = new BtnBoard_UserControl(1, "123") { Margin = new Thickness(10, 0, 0, 0) };
            newBoard.content.Text = "New";
            areaBoard.Children.Add(newBoard);
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void btnInvite_Click(object sender, RoutedEventArgs e)
        {
            AddMemberToWorkspace addMemberToWorkspace = new AddMemberToWorkspace(idWorkspace);
            Window window = new CreateWorkspace_View();
            window.Content = addMemberToWorkspace;
            window.Width = addMemberToWorkspace.Width;
            window.Height = addMemberToWorkspace.Height;
            window.WindowStyle = WindowStyle.None;

            window.Show();

        }

        private void btnCancelDescription_Click(object sender, RoutedEventArgs e)
        {
            MessageBox_View msb = new MessageBox_View();
            msb.Show("Error Unknow , This feature is under development, please try again later. Sorry for the inconvenience", 2);
        }
    }
}

using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.ViewModels;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Table = AVG_TASK_APP.Models.Table;
namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for CreateBoard.xaml
    /// </summary>
    public partial class CreateBoard : Window
    {
        private IWorkspaceRepository workspaceRepository;
        private TableRepository tableRepository;
        public CreateBoard(int idWorkspace = -1)
        {
            InitializeComponent();

            workspaceRepository = new WorkspaceRepository();

            List<Workspace> workspaces = (List<Workspace>)workspaceRepository.GetAllForUser();

            tableRepository = new TableRepository();

            cbWorkspace.ItemsSource = workspaces;
            cbWorkspace.DisplayMemberPath = "Name";
            cbWorkspace.SelectedValuePath = "Id";
            cbWorkspace.SelectedValue = idWorkspace;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!checkInput())
            {
                MessageBoxView messageBoxView = new MessageBoxView();
                messageBoxView.Show("Please not to leave blank");
                return;
            }
            bool pin = false;
            if (radioVisibility.IsChecked == true)
            {
                pin = true;
            }
            else if (radioInvisibility.IsChecked == true)
            {
                pin = false;
            }


            Table table = new Table()
            {
                Id_Workspace = (int)cbWorkspace.SelectedValue,
                Name = txtTitle.Text,
                Pin = pin,
                Visible = true,
                Code = codeBoard.Text
            };
            tableRepository.Add(table);
            this.Close();

        }
        private string RandomCode()
        {
            Random rnd = new Random();
            return "AVG_Table_" + rnd.Next();
        }

        private bool checkInput()
        {
            bool validData = false;
            if (string.IsNullOrWhiteSpace("txtTitle") || txtTitle.Text.Length < 3 || cbWorkspace.SelectedIndex == -1 || (radioVisibility.IsChecked == false && radioInvisibility.IsChecked == false) || string.IsNullOrWhiteSpace("codeBoard") || codeBoard.Text.Length < 3)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
        }

        private void codeBoard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            codeBoard.Text = string.Empty;
        }

        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            codeBoard.Text = RandomCode();
        }
    }
}

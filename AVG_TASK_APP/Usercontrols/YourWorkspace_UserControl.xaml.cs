using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views.Layouts;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for YourWorkspaceUserControl.xaml
    /// </summary>
    public partial class YourWorkspace_UserControl : UserControl
    {
        private TableRepository tableRepository;
        public YourWorkspace_UserControl(int idWorkspace)
        {
            YourWorkspace_ViewModel viewModel = new YourWorkspace_ViewModel();
            DataContext = viewModel;

            tableRepository = new TableRepository();

            InitializeComponent();

            this.idWorkspace.Text = idWorkspace.ToString();
            nameWorkspace.Text = viewModel.getName();
            countMember.Text = "( " + viewModel.countMember() + " ) Members";

            List<Table> tables = viewModel.GetTables();
            foreach (Table table in tables)
            {
                BtnBoard_UserControl btnBoard = new BtnBoard_UserControl(table.Id, table.Name);

                workspaceStackPanel.Children.Add(btnBoard);
            }
        }

        private void buttonLinkBoard_Click(object sender, RoutedEventArgs e)
        {
            Home_Layout Home_Layout = (Home_Layout)Window.GetWindow(this);
            Home_Layout.areaUserControl.Children.Clear();
            BoardUserControl boardView = new BoardUserControl(int.Parse(idWorkspace.Text));
            Home_Layout.areaUserControl.Children.Add(boardView);
        }
    }
}

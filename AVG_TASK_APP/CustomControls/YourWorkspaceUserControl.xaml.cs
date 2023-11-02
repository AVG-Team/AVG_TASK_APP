using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views;
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

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for YourWorkspaceUserControl.xaml
    /// </summary>
    public partial class YourWorkspaceUserControl : UserControl
    {
        private TableRepository tableRepository;
        public YourWorkspaceUserControl(int idWorkspace)
        {
            YourWorkspaceViewModel viewModel = new YourWorkspaceViewModel();
            DataContext = viewModel;

            tableRepository = new TableRepository();

            InitializeComponent();

            this.idWorkspace.Text = idWorkspace.ToString();
            nameWorkspace.Text = viewModel.getName();
            countMember.Text = "( " + viewModel.countMember() + " ) Members";

            List<Table> tables = viewModel.GetTables();
            foreach (Table table in tables)
            {
                btnBoard btnBoard = new btnBoard(table.Id, table.Name);

                workspaceStackPanel.Children.Add(btnBoard);
            }
        }

        private void buttonLinkBoard_Click(object sender, RoutedEventArgs e)
        {
            PageLayout pageLayout = (PageLayout)Window.GetWindow(this);
            pageLayout.areaUserControl.Children.Clear();
            BoardUserControl boardView = new BoardUserControl(int.Parse(idWorkspace.Text));
            pageLayout.areaUserControl.Children.Add(boardView);
        }
    }
}

using AVG_TASK_APP.Models;
using AVG_TASK_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : System.Windows.Controls.UserControl
    {
        private BoardViewModel viewModel;
        public BoardView()
        {
            viewModel = new BoardViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void BoardView_Loaded(object sender, RoutedEventArgs e)
        {
            loadTablesRecently();
            loadWorkspacesRecently();
        }

        private void loadWorkspacesRecently()
        {
            List<Workspace> workspaceList = viewModel.GetWorkspacesRecently();
            if (workspaceList == null) return;
            foreach (Workspace workspace in workspaceList)
            {
                YourWorkspaceUserControl yourWorkspace = new YourWorkspaceUserControl(workspace.Id);
                workspaces.Children.Add(yourWorkspace);
            }
        }

        private void loadTablesRecently()
        {
            List<Table> tables = viewModel.GetTablesRecently();
            if (tables == null)
                return;
            foreach (Table table in tables)
            {
                btnBoard btnBoard = new btnBoard(table.Id, table.Name);
                tableListRecently.Children.Add(btnBoard);
            }
        }
    }
}

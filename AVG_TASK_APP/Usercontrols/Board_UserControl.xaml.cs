using AVG_TASK_APP.Models;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views;
using AVG_TASK_APP.Views.Notifies;
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

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class Board_UserControl : System.Windows.Controls.UserControl
    {
        private Board_ViewModel viewModel;
        public Board_UserControl()
        {
            viewModel = new Board_ViewModel();
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
                YourWorkspace_UserControl yourWorkspace = new YourWorkspace_UserControl(workspace.Id);
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
                BtnBoard_UserControl btnBoard = new BtnBoard_UserControl(table.Id, table.Name);
                tableListRecently.Children.Add(btnBoard);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox_View msb = new MessageBox_View();
            msb.Show("Error Unknow , This feature is under development, please try again later. Sorry for the inconvenience", 2);
        }
    }
}

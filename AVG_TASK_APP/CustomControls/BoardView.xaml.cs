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
            int idWorkspace = viewModel.getIdWorkspaceRecently();
            YourWorkspaceUserControl yourWorkspace = new YourWorkspaceUserControl(idWorkspace);
            workspaceInfo.Children.Add(yourWorkspace);

            List<Table> tables = viewModel.GetTables(idWorkspace);
            foreach (Table table in tables)
            {
                btnBoard btnBoard = new btnBoard(table.Id, table.Name);
                workspaceStackPanel.Children.Add(btnBoard);
            }
        }
    }
}  

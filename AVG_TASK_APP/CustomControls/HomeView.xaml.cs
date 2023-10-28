using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private ITableRepository tableRepository;

        public HomeView()
        {
            tableRepository = new TableRepository();
            InitializeComponent();

            List<Table> tables = (List<Models.Table>)tableRepository.GetAllForUser();

            foreach (Models.Table table in tables)
            {
                Workspace w = tableRepository.GetWorkspace(table.Id);
                var role = tableRepository.GetRole(table.Id);
                string name = "";

                if (w != null)
                {
                    name = w.Name;
                }

                btnBoardInHome btn = new btnBoardInHome(table.Id, table.Name, name, role);
                recently.Children.Add(btn);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            groupFeatured.Visibility = Visibility.Collapsed;
        }
    }
}

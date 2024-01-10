using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class Home_UserControl : UserControl
    {
        private ITableRepository tableRepository;

        public Home_UserControl()
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

                BtnBoard_Home_UserControl btn = new BtnBoard_Home_UserControl(table.Id, table.Name, name, role);
                recently.Children.Add(btn);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            groupFeatured.Visibility = Visibility.Collapsed;
        }
    }
}

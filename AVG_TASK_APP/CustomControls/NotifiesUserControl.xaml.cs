using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MySql.Data.MySqlClient;
namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for NotifiesUserControl.xaml
    /// </summary>
    public partial class NotifiesUserControl : UserControl
    {
        public  NotifiesUserControl()
        {
            InitializeComponent();
            string connectionString = "Server=103.200.23.139;Database=ntddevte_avg_task;Uid=ntddevte_hung;Pwd=AVGTASK2023;";
            string tableName = "Notifies";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"SELECT COUNT(*) FROM {tableName}", connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    for (int i = 0; i < count; i++)
                    {
                        NotifyItem notifyItem = new NotifyItem(i);
                        ListNotifies.Children.Add(notifyItem);
                    }
                }
            }

            

/*            NotifyItem notifyItem2 = new NotifyItem(1);
            NotifyItem notifyItem3 = new NotifyItem(2);
            ListNotifies.Children.Add(notifyItem1);
            ListNotifies.Children.Add(notifyItem2);
            ListNotifies.Children.Add(notifyItem3);*/
        }
        
    }
}

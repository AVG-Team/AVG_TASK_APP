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
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using AVG_TASK_APP.DataAccess;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for NotifiesUserControl.xaml
    /// </summary>
    public partial class NotifiesUserControl : UserControl
    {
        private NotifyItem notifyItem;
        private INotifyRepository notifyRepository;

        public  NotifiesUserControl()
        {
            InitializeComponent();
            // Assuming you have a NotifyRepository to access your data
            notifyRepository = new NotifyRepository();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }
        public void loadData()
        {
            List<Notify> notifyData = (List<Notify>)notifyRepository.GetAll();

            foreach (Notify notify in notifyData)
            {
                var notifyItem = new NotifyItem(notify.Id);
                ListNotifies.Children.Add(notifyItem);
            }
        }
        private void ButtonCreateNotify_Click(object sender, RoutedEventArgs e)
        {
            FormCreateNotify formCreateNotify = new FormCreateNotify(this);
            formCreateNotify.Show();
        }
    }

}

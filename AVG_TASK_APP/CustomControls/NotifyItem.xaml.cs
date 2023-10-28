using AVG_TASK_APP.Migration;
using AVG_TASK_APP.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for NotifyItem.xaml
    /// </summary>
    public partial class NotifyItem : UserControl
    {
        public AppDbContext context;
        public NotifyItem()
        {
            InitializeComponent();
            // Create DbContextOptions with your MySQL connection string and options
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql("Server=103.200.23.139;Database=ntddevte_avg_task;Uid=ntddevte_hung;Pwd=AVGTASK2023;ConvertZeroDateTime=True;", new MySqlServerVersion(new Version(8, 0, 23))) // Replace with your actual MySQL connection string
                .Options;

            // Initialize the context using the options
            context = new AppDbContext(options);

            // Create an instance of your NotifyViewModel
            var viewModel = new NotifyViewModel();

            // Populate the NotifyViewModelCollection with data
            viewModel.NotifyViewModelCollection = LoadData(); // Implement this method to load your data

            // Set the DataContext of your view to the viewModel
            DataContext = viewModel;
        }

        private ObservableCollection<NotifyViewModel> LoadData()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql("Server=103.200.23.139;Database=ntddevte_avg_task;Uid=ntddevte_hung;Pwd=AVGTASK2023;ConvertZeroDateTime=True;", new MySqlServerVersion(new Version(8, 0, 23))) // Replace with your actual MySQL connection string
            .Options;
            ObservableCollection<NotifyViewModel> data = new ObservableCollection<NotifyViewModel>();

            using (AppDbContext context = new AppDbContext(options)) // Replace with your actual DbContext
            {
                var notifies = context.Notifies.Include(n => n.User).ToList();

                // Create NotifyViewModel instances and add them to the collection
                foreach (var notify in notifies)
                {
                    var viewModel = new NotifyViewModel
                    {
                        UserName = notify.User.Name,
                        NotifyContent = notify.Content,
                        NotifyCreatedAt = notify.Created_At
                    };
                    data.Add(viewModel);
                }
            }

            return data;
        }
    }
}

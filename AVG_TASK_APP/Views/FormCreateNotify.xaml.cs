using AVG_TASK_APP.DataAccess;
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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.CustomControls;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for FormCreateNotify.xaml
    /// </summary>
    public partial class FormCreateNotify : Window
    {
        private NotifyRepository notifyRepository;
        private bool isCheck = true; // Initial state
        NotifiesUserControl notifiesUserControl;
        private int idCurrentUser;
        public FormCreateNotify(NotifiesUserControl notifiesUserControl)
        {
            notifyRepository = new NotifyRepository();
            InitializeComponent();

            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            idCurrentUser = int.Parse(identity.Claims.FirstOrDefault(s => s.Type == "Id").Value);

            this.notifiesUserControl = notifiesUserControl;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string text = new TextRange(ContentNotifyTextBlock.Document.ContentStart, ContentNotifyTextBlock.Document.ContentEnd).Text;
            
            if(text.Length <= 0 )
            { 
                MessageBoxView messageBoxView = new MessageBoxView();
                messageBoxView.Show("Please not to leave blank ");
                return;
            }
            int pin = 0;
            if (PinButton.IsChecked == true)
            {
                pin = 1;
            }

            Notify notify = new Notify()
            {
                Content = text,
                Id_User = idCurrentUser,
                Pin = pin,
            };

            notifyRepository.Add(notify);
            //thong bap

            this.Close();
        }

        private void ContentNotifyTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock placeholderContentNotify = ContentNotifyTextBlock.Template.FindName("PlaceholderContentNotify", ContentNotifyTextBlock) as TextBlock;
            placeholderContentNotify.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            IUserRepository userRepository = new UserRepository();
            int level = userRepository.GetById(idCurrentUser).Level;

            if (level < 2)
            {
                MessageBoxView msb = new MessageBoxView();
                msb.Show("Are you not allowed to access this !!!", 1);
                this.Close();
                return;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            notifiesUserControl.loadData();
        }
    }
}

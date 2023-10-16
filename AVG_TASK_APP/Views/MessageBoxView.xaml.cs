using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
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

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBoxView : Window
    {
        public MessageBoxView()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
        public void ProcessFault(string message, int code)
        {

            //notification
           if(code == 0)
           {
                txtTitle.Text = "Notification";
                txtMessage.Text = message;
                this.Show();
           }
           else if (code == 1) //error
           {
                txtTitle.Text = "Error";
                txtMessage.Text = message;
                btnOk.Visibility = Visibility.Collapsed;
                this.Show();
            }
            else //warning
            {
                txtTitle.Text = "Warning";
                txtMessage.Text = message;
                this.Show();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

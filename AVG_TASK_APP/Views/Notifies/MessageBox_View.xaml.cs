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

namespace AVG_TASK_APP.Views.Notifies
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox_View : Window
    {
        public bool dialogResult = false;

        public MessageBox_View()
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

        public void Show(string message, int code = 0)
        {
            btnCancel.Visibility = Visibility.Collapsed;
            //notification
            if (code == 0)
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

        public void DialogResultShow(string message, int code = 0)
        {
            //notification
            if (code == 0)
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
            dialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            dialogResult = true;
            this.Close();
        }
    }
}

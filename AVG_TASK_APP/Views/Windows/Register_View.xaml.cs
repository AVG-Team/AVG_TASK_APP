using System.Windows;
using System.Windows.Input;
using AVG_TASK_APP.Views.Windows;

namespace AVG_TASK_APP.Views.Windows
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class Register_View : Window
    {
        public Register_View()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Login_View loginView = new Login_View();
            loginView.Show();
            this.Close();
        }

        private void textHadAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login_View loginView = new Login_View();
            loginView.Show();
            this.Close();
        }

        private void clickHereLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login_View loginView = new Login_View();
            loginView.Show();
            this.Close();
        }
    }
}

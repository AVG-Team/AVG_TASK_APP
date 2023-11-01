using AVG_TASK_APP.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontAwesome.Sharp;
using System.Threading.Tasks;
using FontAwesome.WPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using Microsoft.Win32;
using System.Reflection;
using System.Security.Claims;
using System.Threading;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class PageLayout : Window
    {
        private int _count = 1;

        private PageLayoutViewModel viewModel;

        public PageLayout()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            BoardView boardView = new BoardView();
            areaUserControl.Children.Add(boardView);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!txtSearch.IsMouseOver)
            {
                txtSearch.Width = 150;
                btnMinimize.Focus();
            }
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = ""; 
            txtSearch.Width = 300;

        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
            }
            txtSearch.Width = 150;

            areaMenuSearch.IsOpen = false;
        }

        private void Avatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserInformationUi userInformationUi = new UserInformationUi();
            userInformationUi.ShowDialog();
            if (userInformationUi.Visibility == Visibility.Visible)
            {
                PageLayout pageLayout = new PageLayout();
                pageLayout.Show();
                this.Close();
            }
        }

        private void HomeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            HomeView homeView = new HomeView();
            areaUserControl.Children.Add(homeView);

        }

        private void BoardRadioButton_Click(object sender, RoutedEventArgs e)
        {
            BoardView boardView = new BoardView();
            areaUserControl.Children.Add(boardView);
        }

        private void btnUserMenu_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Get the button and check for nulls

            if (button == null || button.ContextMenu == null)
                return;
            // Set the placement target of the ContextMenu to the button
            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.FlowDirection = FlowDirection.LeftToRight;
            // Open the ContextMenu
            button.ContextMenu.IsOpen = true;
        }

        private void itemMenuLogout(object sender, RoutedEventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var registryKey = Registry.CurrentUser.OpenSubKey("Software\\" + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title + "\\Login", true);

            registryKey.SetValue("Username", String.Empty);
            registryKey.SetValue("Password", String.Empty);

            var currentPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentPrincipal != null)
            {
                foreach (var identity in currentPrincipal.Identities)
                {
                    identity.Claims.ToList().ForEach(c => identity.RemoveClaim(c));
                }
            }

            LoginView login = new LoginView();
            login.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (!(window is LoginView))
                {
                    window.Close();
                }
            }
        }

        private void txtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {

            if (txtSearch.Text.Length == 0)
            {
                areaMenuSearch.IsOpen = false;
                return;
            }

            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                areaMenuSearch.IsOpen = false;
            }
        }
    }
}

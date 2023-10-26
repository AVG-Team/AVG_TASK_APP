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

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class PageLayout : Window
    {
        private int _count = 1;

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
        
        private void btnCreateWorkspace_Click(object sender, RoutedEventArgs e)
        {
            CreateWorkspaceView createWorkspaceView = new CreateWorkspaceView();
            createWorkspaceView.ShowDialog();
            if (createWorkspaceView.Visibility == Visibility.Visible)
            {

                itemWorkspace itemWorkspace = new itemWorkspace();
                menuWorkspace.Children.Add(itemWorkspace.userControl);

            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = ""; // Xóa nội dung mặc định khi bấm vào
            txtSearch.Width = 300; // Kích thước mới khi TextBox nhận focus

        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Search..."; // Đặt lại nội dung mặc định nếu không có gì được nhập
            }
            txtSearch.Width = 150;  // Thu hẹp TextBox khi nó mất focus
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserInformationUi userInformationUi = new UserInformationUi();
            userInformationUi.ShowDialog();
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
    }
}

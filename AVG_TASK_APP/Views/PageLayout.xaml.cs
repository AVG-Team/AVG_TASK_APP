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
            itemWorkspace itemWorkspace = new itemWorkspace();
            menuWorkspace.Children.Add(itemWorkspace.userControl);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!txtUsername.IsMouseOver)
            {
                txtUsername.Width = 150;  // Thu hẹp TextBox
                btnMinimize.Focus();    // Loại bỏ focus từ TextBox
            }
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {

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

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnItemBoard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnItemMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnItemSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = ""; // Xóa nội dung mặc định khi bấm vào
            txtUsername.Width = 300; // Kích thước mới khi TextBox nhận focus

        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                txtUsername.Text = "Search..."; // Đặt lại nội dung mặc định nếu không có gì được nhập
            }
            txtUsername.Width = 150;  // Thu hẹp TextBox khi nó mất focus
         

        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void txtUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
          
           
        }
    }
}

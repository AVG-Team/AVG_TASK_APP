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


    }
}

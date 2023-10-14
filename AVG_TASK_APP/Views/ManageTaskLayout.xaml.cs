using FontAwesome.WPF;
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

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for ManageTaskLayout.xaml
    /// </summary>
    public partial class ManageTaskLayout : Window
    {


        public ManageTaskLayout()
        {
            InitializeComponent();


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnMenuHeader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rdbtnMenuHeader_Click(object sender, RoutedEventArgs e)
        {
            MenuItem contextMenu = this.FindResource("contextMenu") as MenuItem;
            contextMenu.IsSubmenuOpen = true;

        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateWorkspace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = itemMenuWorkspace;
            if (stackPanel.Visibility == Visibility.Collapsed)
            {
                stackPanel.Visibility = Visibility.Visible;
                iconMenu.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.CaretDown;
            }
            else
            {
                stackPanel.Visibility = Visibility.Collapsed;
                iconMenu.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.CaretUp;
            }
        }

        private void btnItemMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnItemBoard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnItemSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

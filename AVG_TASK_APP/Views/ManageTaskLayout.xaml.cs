using AVG_TASK_APP.CustomControls;
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


            ManageTaskUserControl manageTaskUserControl = new ManageTaskUserControl();
            areaManageTask.Children.Add(manageTaskUserControl);


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

            StackPanel stackPanel = sender as StackPanel;
            ContextMenu contextMenu = stackPanel.ContextMenu;
            contextMenu.PlacementTarget = stackPanel;
            contextMenu.IsOpen = true;
            e.Handled = true;

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
            AddMember addMember = new AddMember();
            addMember.ShowDialog();
        }

        private void ComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateBoard createBoard = new CreateBoard();
            createBoard.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BoardRadioButton_Click(object sender, RoutedEventArgs e)
        {
            itemWorkspace itemWorkspace = new itemWorkspace();
            areaManageTask.Children.Add(itemWorkspace);
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserInformationUi ui = new UserInformationUi();
            ui.ShowDialog();
        }

        private void MoveControlButton_Click(object sender, RoutedEventArgs e)
        {
            gridLeft.Width = new GridLength(15);
            areaManageTaskSet.HorizontalAlignment = HorizontalAlignment.Center;
            areaManageTask.Width = 1520;
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

        private void infomationUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserInformationUi userInformationUi = new UserInformationUi();
            userInformationUi.Show();
        }

        private void boderLeft_MouseMove(object sender, MouseEventArgs e)
        {
            gridLeft.Width = new GridLength(320);
            areaManageTask.Width = 1220;
        }
    }
}

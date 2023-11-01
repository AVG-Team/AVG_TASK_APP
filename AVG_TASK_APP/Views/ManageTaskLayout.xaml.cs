using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using FontAwesome.WPF;
using Microsoft.Win32;
using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for ManageTaskLayout.xaml
    /// </summary>
    public partial class ManageTaskLayout : Window
    {
        private TableRepository tableRepository = new TableRepository();
        private WorkspaceRepository workspaceRepository = new WorkspaceRepository();
        private ManageTaskUserControlViewModel manageTaskUserControlViewModel = new ManageTaskUserControlViewModel();
        private bool isUserNotifyVisible = false;
        public ManageTaskLayout()
        {
            InitializeComponent();

            loadItemTable();

            nameWorkspace.Text = workspaceRepository.GetById(116).Name;

        }

        private void loadItemTable()
        {
            listBoards.Children.Clear();
            List<Models.Table> tables = (List<Models.Table>)tableRepository.GetAllForWorkspace(116);
            foreach (Models.Table item in tables)
            {
                RadioButtonBoard radioButtonBoard = new RadioButtonBoard(item.Id);
                listBoards.Children.Add(radioButtonBoard);
                radioButtonBoard.itemTable_Click += ItemTable_Click;
            }
        }

        private void ItemTable_Click(object? sender, EventArgs e)
        {
            RadioButtonBoard radioButtonBoard = sender as RadioButtonBoard;
            this.areaManageTask.Children.Clear();
            int idTable = int.Parse(radioButtonBoard.idTable.Text);
            ManageTaskUserControl manageTaskUserControl = new ManageTaskUserControl(idTable);
            this.areaManageTask.Children.Add(manageTaskUserControl);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserInformationUi ui = new UserInformationUi();
            ui.ShowDialog();
        }

        private void MoveControlButton_Click(object sender, RoutedEventArgs e)
        {
            /*gridLeft.Width = new GridLength(15);*/
            /*areaManageTaskSet.HorizontalAlignment = HorizontalAlignment.Center;*/
            /*areaManageTask.Width = 1520;*/
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
            /*  gridLeft.Width = new GridLength(320);*/
            /* areaManageTask.Width = 1220;*/
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
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

        private void btnItemMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void starList_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateStarListMenu(object sender, EventArgs e)
        {
            /* // Clear existing items
             starList.Items.Clear();

             foreach (ManageTaskUserControl i in manageTaskUserControls)
             {
                 if (i.iconStart.Foreground == Brushes.Orange)
                 {
                     MenuItem item = new MenuItem();
                     item.Header = i.NameTable.Text;
                     item.Template = FindResource("Item_Template") as ControlTemplate;
                     bool itemExists = false;

                     // Check if an item with the same Header already exists
                     foreach (MenuItem existingItem in starList.Items)
                     {
                         if (existingItem.Header != null && existingItem.Header.ToString() == item.Header.ToString())
                         {
                             itemExists = true;
                             break;
                         }
                     }

                     if (!itemExists)
                     {
                         starList.Items.Add(item);
                     }
                 }
             }*/
        }

        private void Notifies_Click(object sender, RoutedEventArgs e)
        {
            if (isUserNotifyVisible)
            {
                // If it's currently visible, collapse it
                areaManageNotify.Children.Clear();
                areaManageNotify.Visibility = Visibility.Collapsed;
                isUserNotifyVisible = false;
            }
            else
            {
                // If it's not visible, create and show it
                NotifiesUserControl notifiesUserControl = new NotifiesUserControl();
                areaManageNotify.Width = 500;
                areaManageNotify.Height = 450;
                areaManageNotify.HorizontalAlignment = HorizontalAlignment.Left;
                areaManageNotify.VerticalAlignment = VerticalAlignment.Top;
                areaManageNotify.Children.Add(notifiesUserControl);
                areaManageNotify.Visibility = Visibility.Visible;
                isUserNotifyVisible = true;
            }
        }
    }
}

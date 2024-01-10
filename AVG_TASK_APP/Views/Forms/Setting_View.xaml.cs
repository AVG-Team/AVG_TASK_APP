using AVG_TASK_APP.Usercontrols;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views.Layouts;
using System;
using System.Collections.Generic;
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

namespace AVG_TASK_APP.Views.Forms
{
    /// <summary>
    /// Interaction logic for Setting_View.xaml
    /// </summary>
    public partial class Setting_View : Window
    {
        private int idWorkspaceCurrent;
        private bool visible;
        private string nameWorkspaceCurrent;
        private WorkspaceRepository workspaceRepository;
        private Setting_ViewModel SettingViewModel;
        private BoardUserControl boardUserControl;
        private HomeLayout_ViewModel Home_LayoutViewModel;

        public Setting_View(int idWorkspace)
        {
            InitializeComponent();
            SettingViewModel = new Setting_ViewModel();
            workspaceRepository = new WorkspaceRepository();

            DataContext = SettingViewModel;
            idWorkspaceCurrent = idWorkspace;

            Home_LayoutViewModel = new HomeLayout_ViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }
        public void Reload()
        {
            visible = workspaceRepository.GetById(idWorkspaceCurrent).Visible;
            nameWorkspaceCurrent = workspaceRepository.GetById(idWorkspaceCurrent).Name;


            SettingViewModel.GetTextVisible(visible);
            var textVisibleBinding = new Binding("TextVisible");
            this.textVisible.SetBinding(TextBlock.TextProperty, textVisibleBinding);
            this.visibilityWorkspace.SetBinding(TextBlock.TextProperty, textVisibleBinding);

            SettingViewModel.getName(idWorkspaceCurrent);
            var nameWorkspaceBinding = new Binding("NameWorkspace");
            this.nameWorkspace.SetBinding(TextBlock.TextProperty, nameWorkspaceBinding);

            SettingViewModel.ChangeVisible(visible);
            var changeVisibleBinding = new Binding("TextVisibleChange");
            this.changeVisible.SetBinding(MenuItem.HeaderProperty, changeVisibleBinding);

            if (visible)
            {
                iconVisible.Icon = (FontAwesome.Sharp.IconChar)FontAwesome.WPF.FontAwesomeIcon.Globe;
                iconVisibilityWorkspace.Icon = (FontAwesome.Sharp.IconChar)FontAwesome.WPF.FontAwesomeIcon.Globe;
                iconVisible.Foreground = Brushes.Green;

            }
            else
            {
                iconVisible.Icon = (FontAwesome.Sharp.IconChar)FontAwesome.WPF.FontAwesomeIcon.Lock;
                iconVisibilityWorkspace.Icon = (FontAwesome.Sharp.IconChar)FontAwesome.WPF.FontAwesomeIcon.Lock;
                iconVisible.Foreground = Brushes.Red;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
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

        private void changeVisible_Click(object sender, RoutedEventArgs e)
        {
            Workspace workspace = workspaceRepository.GetById(idWorkspaceCurrent);
            if (changeVisible.Header.ToString() == "Public")
            {
                workspace.Visible = true;
                workspaceRepository.Update(workspace);
                Reload();
            }
            else
            {
                workspace.Visible = false;
                workspaceRepository.Update(workspace);
                Reload();
            }
        }

        private void txtNameWorkspace_LostFocus(object sender, RoutedEventArgs e)
        {
            nameWorkspaceCurrent = txtNameWorkspace.Text;
            Workspace workspace = workspaceRepository.GetById(idWorkspaceCurrent);
            workspace.Name = nameWorkspaceCurrent;
            workspaceRepository.Update(workspace);


            nameWorkspace.Text = nameWorkspaceCurrent;
            nameWorkspace.Visibility = Visibility.Visible;
            txtNameWorkspace.Visibility = Visibility.Collapsed;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!txtNameWorkspace.IsFocused || !btnEdit.IsMouseOver)
                btnClose.Focus();
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            nameWorkspace.Visibility = Visibility.Collapsed;
            txtNameWorkspace.Visibility = Visibility.Visible;
            txtNameWorkspace.Text = nameWorkspaceCurrent;
            txtNameWorkspace.Focus();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Home_Layout Home_Layout = new Home_Layout();
            Home_Layout.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Home_Layout && window != Home_Layout)
                {
                    window.Close();
                    return;
                }
            }
        }
    }
}

using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for AddMemberToWorkspace.xaml
    /// </summary>
    public partial class AddMemberToWorkspace : Page
    {
        private System.Windows.Controls.ListBox _menuSearch = new System.Windows.Controls.ListBox();
        private IUserRepository userRepository;
        private MessageBoxView msb = new MessageBoxView();
        private List<ItemMenuSearch> selectedItems = new List<ItemMenuSearch>();

        public AddMemberToWorkspace(int idWorkspaceTmp)
        {
            InitializeComponent();
            this.idWorkspace.Text = idWorkspaceTmp.ToString();

            userRepository = new UserRepository();
            MenuSearch menuSearch = new MenuSearch();
            areaMenu.Child = menuSearch;
            _menuSearch = menuSearch.lb;

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
        }

        private void autoComplete()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        private bool IsSelected(string email)
        {
            foreach (ItemMenuSearch selectedItem in selectedItems)
            {
                if (selectedItem.Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            areaMenu.IsOpen = true;
            _menuSearch.Items.Clear();

            string search = txtEmail.Text.Trim();
            if (search.Contains(";"))
            {
                string[] emailParts = search.Split(';');
                search = emailParts[emailParts.Length - 1].Trim();
            }

            List<UserModel> users = (search.Length == 0) ? (List<UserModel>)userRepository.GetAll() : (List<UserModel>)userRepository.GetByContainEmail(search);

            if (users == null)
                return;

            foreach (UserModel user in users)
            {
                if (!IsSelected(user.Email))
                {
                    ItemMenuSearch itemMenuSearch = new ItemMenuSearch(user.Email, user.Name, user.Level);
                    itemMenuSearch.PreviewMouseDown += ItemMenuSearch_PreviewMouseDown;
                    _menuSearch.Items.Add(itemMenuSearch);
                }
            }
        }

        private void ItemMenuSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemMenuSearch item = (ItemMenuSearch)sender;
            string emailTmp = item.Email;
            valueEmail.Text += item.Email + ";";
            txtEmail.Text = valueEmail.Text;
            areaMenu.IsOpen = false;
            selectedItems.Add(item);
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            areaMenu.IsOpen = false;
            _menuSearch.Items.Clear();
        }

        private void txtEmail_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtEmail.Text.Length == 0)
            {
                areaMenu.IsOpen = false;
                _menuSearch.Items.Clear();
                valueEmail.Text = null;
                selectedItems = new List<ItemMenuSearch>();
                return;
            }
            if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                areaMenu.IsOpen = false;
                _menuSearch.Items.Clear();
                char lastChar = txtEmail.Text[txtEmail.Text.Length - 1];
                if (lastChar == ';')
                {
                    string[] tmp = txtEmail.Text.Split(";");
                    if (tmp.Length == 1)
                    {
                        valueEmail.Text = null;
                        selectedItems = new List<ItemMenuSearch>();
                        return;
                    }

                    valueEmail.Text = txtEmail.Text;
                    List<ItemMenuSearch> selectedItemsTmp = new List<ItemMenuSearch>();
                    foreach (ItemMenuSearch selectedItem in selectedItems)
                    {
                        foreach (string email in tmp)
                        {
                            if(email == selectedItem.Email)
                            {
                                selectedItemsTmp.Add(selectedItem);
                                break;
                            }
                        }
                    }

                    selectedItems = selectedItemsTmp;
                }
            }
        }
    }
}

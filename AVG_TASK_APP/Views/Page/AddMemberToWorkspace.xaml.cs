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

        public AddMemberToWorkspace(int idWorkspace)
        {
            InitializeComponent();
            this.idWorkspace.Text = idWorkspace.ToString();

            userRepository = new UserRepository();
            MenuSearch menuSearch = new MenuSearch();
            areaMenu.Child = menuSearch;
            _menuSearch = menuSearch.lb;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //Window parent = Window.GetWindow(this);
            //parent.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }
            try
            {
                MailAddress mailAddress = new MailAddress(txtEmail.Text);
                // True
            }
            catch (FormatException)
            {
                // False
            }
        }

        private void autoComplete()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Window parent = Window.GetWindow(this);
            //parent.Close();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            areaMenu.IsOpen = true;
            _menuSearch.Items.Clear();

            string search = txtEmail.Text;
            if (search.Contains(";"))
            {
                string[] tmp = search.Split(';');
                search = tmp[tmp.Length-1];
            }

            List<UserModel> users = (List<UserModel>)userRepository.GetByContainEmail(search);
            if (txtEmail.Text.Length == 0 )
            {
                users = (List<UserModel>)userRepository.GetAll();
            }
            foreach (UserModel user in users)
            {
                ItemMenuSearch itemMenuSearch = new ItemMenuSearch(user.Email, user.Name, user.Level);
                itemMenuSearch.PreviewMouseDown += ItemMenuSearch_PreviewMouseDown;
                _menuSearch.Items.Add(itemMenuSearch);
            }
        }

        private void ItemMenuSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ItemMenuSearch item = (ItemMenuSearch)sender;
            string emailTmp = item.Email;
            txtEmail.Text += emailTmp + ";";
            areaMenu.IsOpen = false;
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            areaMenu.IsOpen = false;
            _menuSearch.Items.Clear();
        }
    }
}

using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for AddMemberToWorkspace.xaml
    /// </summary>
    public partial class AddMemberToWorkspace : Page
    {
        private MessageBoxView msb = new MessageBoxView();

        private AddMemberToWorkSpaceViewModel viewModel;

        public AddMemberToWorkspace(int idWorkspace)
        {
            viewModel = new AddMemberToWorkSpaceViewModel();
            DataContext = viewModel;
            InitializeComponent();

            this.idWorkspace.Text = idWorkspace.ToString();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            areaMenu.IsOpen = false;
            if(viewModel.MenuSearch != null)
                viewModel.MenuSearch.Clear();
        }

        private void txtEmail_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtEmail.Text.Length == 0)
            {
                areaMenu.IsOpen = false;
                viewModel.MenuSearch.Clear();
                valueEmail.Text = null;
                return;
            }
            if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                areaMenu.IsOpen = false;
                viewModel.MenuSearch.Clear();
            }
            char lastChar = txtEmail.Text[txtEmail.Text.Length - 1];
            if (lastChar == ';')
            {
                string[] tmp = txtEmail.Text.Split(";");
                if (tmp.Length == 1)
                {
                    valueEmail.Text = null;
                    return;
                }

                valueEmail.Text = txtEmail.Text;
                return;
            }

            string tmp1 = txtEmail.Text.Trim();

            if (tmp1.Contains(";"))
            {
                string[] emailParts = tmp1.Split(';');
                string valueTmp = "";
                foreach (string a in emailParts.Take(emailParts.Length - 1))
                {
                    valueTmp += a + ";";
                }
                valueEmail.Text = valueTmp;

                return;
            }

            valueEmail.Text = null;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for AddMemberToWorkspace.xaml
    /// </summary>
    public partial class AddMemberToWorkspace : Window
    {
        public AddMemberToWorkspace()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("cac");
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
    }
}

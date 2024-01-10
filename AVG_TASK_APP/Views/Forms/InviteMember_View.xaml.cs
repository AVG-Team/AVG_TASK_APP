using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views.Notifies;
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
    /// Interaction logic for AddMember.xaml
    /// </summary>
    public partial class InviteMember_View : Window
    {
        private MessageBox_View msb = new MessageBox_View();

        public InviteMember_View(int idTable)
        {
            InitializeComponent();
            this.idTable.Text = idTable.ToString();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            areaMenu.IsOpen = false;
        }

        private void txtEmail_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (txtEmail.Text.Length == 0)
            {
                areaMenu.IsOpen = false;
                valueEmail.Text = null;
                return;
            }
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                areaMenu.IsOpen = false;
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
    }
}

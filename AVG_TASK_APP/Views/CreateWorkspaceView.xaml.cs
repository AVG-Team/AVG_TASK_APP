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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for CreateWorkspaceView.xaml
    /// </summary>
    public partial class CreateWorkspaceView : Window
    {
        public CreateWorkspaceView()
        {
            InitializeComponent();
            txtDateStart.SelectedDate = DateTime.Today;
        }

        private string RandomCode()
        {
            Random rnd = new Random();
            return "AVG_" + rnd.Next();
        }

        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = RandomCode();
        }

        private bool checkInput()
        {
            bool result = true;
            MessageBox.Show(2 + "A");
            if (string.IsNullOrEmpty(txtName.Text))
            {
            MessageBox.Show(1 +  "A");
                txtName.BorderBrush = Brushes.Red;
                result = false;
            }
            if(string.IsNullOrWhiteSpace(txtCode.Text))
            {
                txtCode.Text = RandomCode();
            }
            MessageBox.Show(txtName.Text);

            return result;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            checkInput();
        }
    }
}

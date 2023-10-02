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

        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string code = "AVG_" + rnd.Next();

            txtCode.Text = code;
        }

        private void checkInput()
        {

        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using AVG_TASK_APP.Usercontrols;
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
    /// Interaction logic for CreateBoard.xaml
    /// </summary>
    public partial class CreateCard_View : Window
    {
        ManageTaskUserControl manageTaskUserControl;
        public CreateCard_View(int idTableCurrent, ManageTaskUserControl userControl)
        {
            InitializeComponent();
            manageTaskUserControl = userControl;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string nameNewCard = txtTitle.Text;
            manageTaskUserControl.CreateCardView_btnCreateCard_Click(nameNewCard);
            this.Close();
        }

    }
}


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

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for InsertGmail.xaml
    /// </summary>
    public partial class InsertGmail : Page
    {
        public InsertGmail()
        {
            InitializeComponent();
        }

        private void Insert_btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Insert_txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length > 0)
            {
                // You can add further behavior here if needed
            }
        }
    }
}

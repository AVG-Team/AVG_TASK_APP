using C1.WPF.Core;
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

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for ManageTaskUserControl.xaml
    /// </summary>
    public partial class ManageTaskUserControl : UserControl
    {


        public ManageTaskUserControl()
        {
            try
            {
                InitializeComponent();

                for (int i = 1; i <= 5; i++)
                {
                    addCard(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void addCard(int pos)
        {
            CardUserControl cardUserControl = new CardUserControl();

            areaCard.Children.Add(cardUserControl);
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }


    }
}

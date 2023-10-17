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
    /// Interaction logic for CardUserControl.xaml
    /// </summary>
    public partial class CardUserControl : UserControl
    {
        private bool isDragging = false;
        private Point offset;

        public CardUserControl()
        {
            InitializeComponent();
        }

        private void taskBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Button button = sender as Button;
                Point currentPosition = e.GetPosition(this);

                double newX = currentPosition.X - offset.X;
                double newY = currentPosition.Y - offset.Y;

                button.Margin = new Thickness(newX, newY, 0, 0);
            }
        }



        private void taskBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Button button = sender as Button;
            offset = e.GetPosition(button);
        }

        private void taskBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                Button button = sender as Button;

                button.ReleaseMouseCapture();
                isDragging = false;
            }
        }

        private void taskBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Task clicked");
        }
    }
}

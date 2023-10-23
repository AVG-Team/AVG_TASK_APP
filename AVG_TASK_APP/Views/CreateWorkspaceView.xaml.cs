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
            txtDateStart.Text = DateTime.Today.ToString();
            txtDateStart.SelectedDate = DateTime.Today;

            hourComboBox.Items.Add("hour");
            for (int i = 0; i < 24; i++)
            {
                hourComboBox.Items.Add(i.ToString("00"));
            }
            hourComboBox.SelectedIndex = 1;

            minuteComboBox.Items.Add("Minute");
            for (int i = 0; i < 60; i++)
            {
                minuteComboBox.Items.Add(i.ToString("00"));
            }
            minuteComboBox.SelectedIndex = 1;
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

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateValueDateStart();
        }

        private void hourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateValueDateStart();
        }

        private void minuteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateValueDateStart();
        }

        private void UpdateValueDateStart()
        {
            DateTime selectedDate = txtDateStart.SelectedDate ?? DateTime.Now;
            int hour = hourComboBox.SelectedItem != null ? int.Parse(hourComboBox.SelectedItem.ToString()) : 0;
            int minute = minuteComboBox.SelectedItem != null ? int.Parse(minuteComboBox.SelectedItem.ToString()) : 0;

            DateTime selectedDateTime = new DateTime(
                selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minute, 0);

            txtValueDateStart.Text = selectedDateTime.ToString("yyyy-MM-dd HH:mm");
        }
    }
}

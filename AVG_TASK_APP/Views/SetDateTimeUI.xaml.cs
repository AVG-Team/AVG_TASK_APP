using AVG_TASK_APP.Repositories;
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
    /// Interaction logic for SetDateTimeUI.xaml
    /// </summary>
    public partial class SetDateTimeUI : Window
    {
        private TaskRepository TaskRepository;
        private int currIdTask;
        public SetDateTimeUI(int idTask)
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
            TaskRepository = new TaskRepository();
            currIdTask = idTask;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Models.Task currTask = TaskRepository.GetById(currIdTask);
            DateTime dateTime = DateTime.Now;
            currTask.Deadline = txtDateStart.SelectedDate ?? dateTime.Date;
            TaskRepository.Update(currTask);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

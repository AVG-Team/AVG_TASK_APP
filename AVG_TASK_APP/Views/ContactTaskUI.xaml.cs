using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for ContactTaskUI.xaml
    /// </summary>
    public partial class ContactTaskUI : Window
    {
        public event EventHandler remove_Click;
        private int idTaskCurrent;
        private int idTableCurrent;
        ManageTaskUserControl manageTaskUserControl;
        private TaskRepository taskRepository;
        public ContactTaskUI(int idTask, ManageTaskUserControl userControl)
        {
            InitializeComponent();
            taskRepository = new TaskRepository();

            idTaskCurrent = idTask;
            manageTaskUserControl = userControl;
            txtDescription.IsEnabled = false;
            btnSaveDescription.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void RichTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnSaveDescription.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            txtDescription.IsEnabled = true;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Models.Task task = taskRepository.GetById(idTaskCurrent);
            taskRepository.Remove(task);


            this.Close();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            manageTaskUserControl.Reload();
        }

        private void btnSaveDescription_Click(object sender, RoutedEventArgs e)
        {
            Models.Task updateTask = taskRepository.GetById(idTaskCurrent);

            string textDescription = new TextRange(txtDescription.Document.ContentStart, txtDescription.Document.ContentEnd).Text;
         
             updateTask.Description = textDescription;
            
            taskRepository.Update(updateTask);

            txtDescription.IsEnabled = false;
            btnSaveDescription.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnSaveDescription.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            txtDescription.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnSaveDescription.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
            txtDescription.IsEnabled = false;
        }

        private void txtDescription_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}

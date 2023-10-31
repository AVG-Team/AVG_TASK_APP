using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private TaskRepository taskRepository;
        public ContactTaskUI(int idTask , int idTable)
        {
            InitializeComponent();
            taskRepository = new TaskRepository();

            idTaskCurrent = idTask;
            idTableCurrent = idTable;
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RichTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Models.Task task = taskRepository.GetById(idTaskCurrent);
            taskRepository.Remove(task);
            
            
            this.Close();
        }
    }
}

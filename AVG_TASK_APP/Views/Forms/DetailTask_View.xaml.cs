﻿using AVG_TASK_APP.Usercontrols;
using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views.Notifies;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AVG_TASK_APP.Views.Forms
{
    /// <summary>
    /// Interaction logic for ContactTaskUI.xaml
    /// </summary>
    public partial class DetailTask_View : Window
    {
        public event EventHandler remove_Click;
        private int idTaskCurrent;
        private int idTableCurrent;

        ManageTaskUserControl manageTaskUserControl;
        private TaskRepository taskRepository;
        public DetailTask_View(int idTask, ManageTaskUserControl userControl)
        {
            InitializeComponent();
            taskRepository = new TaskRepository();

            idTaskCurrent = idTask;
            manageTaskUserControl = userControl;

            this.idTask.Text = idTask.ToString();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

            Close();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            manageTaskUserControl.Reload();
        }

        private void btnDates_Click(object sender, RoutedEventArgs e)
        {
            SettingDate_View setDateTimeUI = new SettingDate_View(idTaskCurrent);
            setDateTimeUI.Show();
        }

        private void btnSaveDescription_Click(object sender, RoutedEventArgs e)
        {
            btnSaveDescription.Visibility = Visibility.Collapsed;
            btnCancelDescription.Visibility = Visibility.Collapsed;
            description.IsReadOnly = true;
        }

        private void btnCancelDescription_Click(object sender, RoutedEventArgs e)
        {
            btnSaveDescription.Visibility = Visibility.Collapsed;
            btnCancelDescription.Visibility = Visibility.Collapsed;
            description.IsReadOnly = true;
        }

        private void description_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnSaveDescription.Visibility = Visibility.Visible;
            btnCancelDescription.Visibility = Visibility.Visible;
            description.IsReadOnly = false;
        }

        private void btnChecklist_Click(object sender, RoutedEventArgs e)
        {
            MessageBox_View msb = new MessageBox_View();
            msb.Show("This feature is under development, please try again later. We apologize for this inconvenience");
        }
    }
}

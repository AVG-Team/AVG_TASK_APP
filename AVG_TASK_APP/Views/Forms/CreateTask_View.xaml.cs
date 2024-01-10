using AVG_TASK_APP.Usercontrols;
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

namespace AVG_TASK_APP.Views.Forms
{
    /// <summary>
    /// Interaction logic for CreateBoard.xaml
    /// </summary>
    public partial class CreateTask_View : Window
    {
        private int idCardCurrnent = -1;
        private int idTableCurrent = -1;
        private TaskRepository taskRepository;
        private ManageTaskUserControl manage;
        public CreateTask_View(int idCard, int idTable, ManageTaskUserControl manageTaskUserControl)
        {
            InitializeComponent();

            taskRepository = new TaskRepository();
            idCardCurrnent = idCard;
            idTableCurrent = idTable;
            manage = manageTaskUserControl;
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
            if (idCardCurrnent != -1)
            {
                Models.Task task = new Models.Task();
                task.Name = txtTaskName.Text;
                task.Description = task.Label = task.Estimate = string.Empty;
                task.Id_Card = idCardCurrnent;
                task.Created_At = DateTime.Now;
                task.Id_Table = idTableCurrent;
                taskRepository.Add(task);
                this.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            manage.Reload();
        }
    }
}


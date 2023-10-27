using AVG_TASK_APP.ViewModels;
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
    /// Interaction logic for YourWorkspaceUserControl.xaml
    /// </summary>
    public partial class YourWorkspaceUserControl : UserControl
    {
        public YourWorkspaceUserControl(int  idWorkspace)
        {
            YourWorkspaceViewModel viewModel = new YourWorkspaceViewModel();
            DataContext = viewModel;

            InitializeComponent();

            this.idWorkspace.Text = idWorkspace.ToString();
            nameWorkspace.Text = viewModel.getName();
            countMember.Text = "( " + viewModel.countMember() + " ) Members";
        }
    }
}

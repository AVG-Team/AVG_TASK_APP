using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views;
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

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for RadioButtonBoard.xaml
    /// </summary>
    public partial class RadioButtonBoard : UserControl
    {
        private RadioButtonBoard_ViewModel viewModel;

        public event EventHandler itemTable_Click;

        public RadioButtonBoard(int idTable)
        {
            viewModel = new RadioButtonBoard_ViewModel();
            DataContext = viewModel;

            InitializeComponent();

            this.idTable.Text = idTable.ToString();

            viewModel.getName();

            var nameTableBinding = new Binding("NameTable");
            this.nameTable.SetBinding(TextBlock.TextProperty, nameTableBinding);
        }

        private void itemBoard_Click(object sender, EventArgs e)
        {
            itemTable_Click?.Invoke(this, EventArgs.Empty);
        }
    }
}

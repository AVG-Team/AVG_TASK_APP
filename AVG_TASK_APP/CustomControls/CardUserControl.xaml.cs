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
    /// Interaction logic for CardUserControl.xaml
    /// </summary>
    public partial class CardUserControl : UserControl
    {
        public ListBox lb { get { return _list; } }
        private CardUserControlViewModel viewModel;

        public CardUserControl(int idTable)
        {
            InitializeComponent();

            viewModel = new CardUserControlViewModel();
            DataContext = viewModel;

            viewModel.getNameCard(idTable);


            var cardNameBinding = new Binding("NameCard");
            this.nameCard.SetBinding(TextBlock.TextProperty, cardNameBinding);
        }
    }
}

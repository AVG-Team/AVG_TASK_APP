using AVG_TASK_APP.Migration;
using AVG_TASK_APP.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for NotifyItem.xaml
    /// </summary>
    public partial class NotifyItem : UserControl
    {
        private int currentId;
        public AppDbContext context;
        private bool isOrange = false;
        NotifyItemViewModel viewModel;

        public NotifyItem(int id)
        {
            viewModel = new NotifyItemViewModel();
            DataContext = viewModel;
            InitializeComponent();

            idNotify.Text = id.ToString();
            loadData(id);
        }
        
        private void loadData(int id)
        {
            NameUser.Text = viewModel.getNameUser();
            ContentNotify.Text = viewModel.getContentNotify(id);
            CreatedAt.Text = viewModel.getCreatedAt(id).ToString("dd/MM/yyyy HH:mm:ss");
            if(viewModel.getNotifyPin(id) == 1)
            {
                changeColor();
            }
        }
        private void changeColor()
        {
            if (Pin.Foreground == Brushes.Orange)
            {
                Pin.Foreground = (Brush)FindResource("color5");
            }
            if (Pin.Foreground == (Brush)FindResource("color5"))
            {
                Pin.Foreground = Brushes.Orange;
            }
        }        
    }
}

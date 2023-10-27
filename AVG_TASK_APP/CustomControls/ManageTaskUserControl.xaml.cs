using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views;
using C1.WPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ManageTaskUserControl.xaml
    /// </summary>
    public partial class ManageTaskUserControl : UserControl
    {
        private List<UserControl> _listCard = new List<UserControl>();
        private List<ListBox> listBoxes = new List<ListBox>();
        private ManageTaskUserControlViewModel viewModel;
        private CardRepository cardRepository;
        private TaskRepository taskRepository;

        private C1DragDropManager _dd;
        private int idTableCurrent = 0;

        public ManageTaskUserControl(int idTable)
        {
            InitializeComponent();

            viewModel = new ManageTaskUserControlViewModel();
            cardRepository = new CardRepository();
            taskRepository = new TaskRepository();

            DataContext = viewModel;
            idTableCurrent = idTable;
            viewModel.getNameTable(idTable);

            var nameTableBinding = new Binding("NameTable");
            this.NameTable.SetBinding(TextBlock.TextProperty, nameTableBinding);

            _dd = new C1DragDropManager();

            _dd.DragDrop += _dd_DragDrop;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var cards = cardRepository.GetAllForTable(idTableCurrent);
            foreach (var item in cards)
            {
                CardUserControl cardUserControl = new CardUserControl(item.Id);
                areaCard.Children.Add(cardUserControl);
                cardUserControl.Tag = item.Id;
                _listCard.Add(cardUserControl);
            }

            foreach (CardUserControl card in _listCard)
            {
                _dd.RegisterDropTarget(card.lb, true);
                card.lb.Tag = card.Tag;
                listBoxes.Add(card.lb);
            }
            LoadItem();
        }

        private void LoadItem()
        {
            foreach (ListBox lb in listBoxes)
            {
                int idCard = int.Parse(lb.Tag.ToString());
                foreach (Models.Task task in taskRepository.GetAllForCard(idCard))
                {

                    var border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.CornerRadius = new CornerRadius(10);
                    border.Background = Brushes.White;
                    border.Height = 40;
                    border.Width = 180;
                    border.Margin.Bottom.Equals(10);


                    var element = new ContentPresenter();
                    element.Content = task.Name;
                    element.MouseLeftButtonUp += personElement_MouseEnter;
                    element.VerticalAlignment = VerticalAlignment.Center;
                    element.HorizontalAlignment = HorizontalAlignment.Center;

                    border.Child = element;
                    lb.Items.Add(border);

                    _dd.RegisterDragSource(border, DragDropEffect.Move, ModifierKeys.None);


                    _dd.DragThreshold = 5;


                    border.MouseDown += (s, e) =>
                    {
                        e.Handled = true;
                    };
                }
            }
        }

        private void _dd_DragDrop(object source, DragDropEventArgs e)
        {
            // get object being dragged
            UIElement sourceElement = e.DragSource;
            // get source parent, target listboxes
            foreach (ListBox lb in listBoxes)
            {
                if (lb.Items.Contains(sourceElement))
                {
                    ListBox sourceParent = lb;
                    ListBox target = e.DropTarget as ListBox;
                    // get index into target
                    int index = GetDropIndex(e, target);
                    // adjust index
                    if (sourceParent == target)
                    {
                        int sourceIndex = sourceParent.Items.IndexOf(sourceElement);
                        if (index > sourceParent.Items.IndexOf(sourceElement))
                        {
                            index--;
                        }
                        if (index == sourceIndex)
                            return;
                    }
                    // remove from original position, insert into new position
                    sourceParent.Items.Remove(sourceElement);
                    target.Items.Insert(index, sourceElement);
                }
            }
        }


        private int GetDropIndex(DragDropEventArgs e, ListBox? target)
        {
            int index = 0;
            foreach (UIElement child in target.Items)
            {
                Point p = e.GetPosition(child);
                if (p.Y - child.DesiredSize.Height / 2 < 0) break;
                index++;
            }
            return index;
        }

        private void personElement_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            ContactTaskUI contactTaskUI = new ContactTaskUI();
            contactTaskUI.Show();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void addNameCardButton_Click(object sender, RoutedEventArgs e)
        {
            AddCard addCard = new AddCard();
            addCard.Show();
        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            ShareBoard shareBoard = new ShareBoard();
            shareBoard.Show();
        }

        private void shareButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (menuBoardControl != null)
            {
                if (menuBoardControl.Visibility == Visibility.Collapsed)
                {
                    menuBoardControl.Visibility = Visibility.Visible;
                }
                else
                {
                    menuBoardControl.Visibility = Visibility.Collapsed;
                }
            }
        }


    }
}

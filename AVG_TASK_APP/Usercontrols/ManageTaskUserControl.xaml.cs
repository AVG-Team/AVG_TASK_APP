﻿using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views.Forms;
using AVG_TASK_APP.Views.Layouts;
using C1.WPF.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for ManageTaskUserControl.xaml
    /// </summary>
    public partial class ManageTaskUserControl : UserControl
    {
        private List<UserControl> _listCard = new List<UserControl>();
        private List<ListBox> listBoxes = new List<ListBox>();
        private ManageTaskUserControl_ViewModel viewModel;
        private CardRepository cardRepository;
        private TaskRepository taskRepository;
        private ITableRepository tableRepository;

        private C1DragDropManager _dd;
        private int idTableCurrent = 0;
        private int idTaskCurrent = 0;

        public ManageTaskUserControl(int idTable)
        {
            InitializeComponent();

            viewModel = new ManageTaskUserControl_ViewModel();
            cardRepository = new CardRepository();
            taskRepository = new TaskRepository();
            tableRepository = new TableRepository();

            DataContext = viewModel;
            idTableCurrent = idTable;
            viewModel.getNameTable(idTable);

            var nameTableBinding = new Binding("NameTable");
            this.NameTable.SetBinding(TextBlock.TextProperty, nameTableBinding);

            _dd = new C1DragDropManager();

            _dd.DragDrop += _dd_DragDrop;



        }

        public void Reload()
        {
            bool pin = tableRepository.GetById(idTableCurrent).Pin;
            if (pin)
                iconStart.Foreground = Brushes.Orange;
            else
                iconStart.Foreground = Brushes.Black;
            var cards = cardRepository.GetAllForTable(idTableCurrent);
            areaCard.Children.Clear();
            foreach (var item in cards)
            {
                Card_UserControl cardUserControl = new Card_UserControl(item.Id);
                areaCard.Children.Add(cardUserControl);
                cardUserControl.Tag = item.Id;
                cardUserControl.btnCreateTask_Click += CardUserControl_btnCreateTask_Click;
                cardUserControl.btnMenuCard_Click += CardUserControl_btnMenuCard_Click;
                _listCard.Add(cardUserControl);
            }

            foreach (Card_UserControl card in _listCard)
            {
                _dd.RegisterDropTarget(card.lb, true);
                card.lb.Tag = card.Tag;
                listBoxes.Add(card.lb);
            }
            LoadItem();
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }
        public void CreateCardView_btnCreateCard_Click(string nameNewCard)
        {

            Models.Card newCard = new Models.Card();
            newCard.Name = nameNewCard;
            newCard.Id_Table = idTableCurrent;
            newCard.Created_At = DateTime.Now;
            cardRepository.Add(newCard);

            Reload();

        }
        private void CardUserControl_btnCreateTask_Click(object? sender, EventArgs e)
        {
            CreateTask_View addTask = new CreateTask_View((int)((Card_UserControl)sender).Tag, idTableCurrent, this);
            addTask.Show();
        }

        private void CardUserControl_btnMenuCard_Click(object? sender, EventArgs e)
        {
            /*MenuCard_UserControl menuCard_UserControl = new MenuCard_UserControl();
            areaCard.Children.Add(menuCard_UserControl);*/
        }

        public void LoadItem()
        {

            foreach (ListBox lb in listBoxes)
            {
                lb.Items.Clear();
                int idCard = int.Parse(lb.Tag.ToString());
                taskRepository = new TaskRepository();
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
                    element.Tag = task.Id;
                    element.MouseLeftButtonUp += personElement_MouseEnter;
                    element.VerticalAlignment = VerticalAlignment.Center;
                    element.HorizontalAlignment = HorizontalAlignment.Center;

                    border.Child = element;
                    border.Tag = task.Id;
                    border.MouseLeftButtonDown += Border_MouseLeftButtonDown;

                    lb.Items.Add(border);
                    /*cardUserControl.Card.Height = cardUserControl.Card.Height + 50;*/

                    _dd.RegisterDragSource(border, DragDropEffect.Move, ModifierKeys.None);


                    _dd.DragThreshold = 5;


                    border.MouseDown += (s, e) =>
                    {
                        e.Handled = true;
                    };
                }
            }

        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                UIElement element = sender as UIElement;
                idTaskCurrent = (int)((Border)element).Tag;
                DetailTask_View contactTaskUI = new DetailTask_View(idTaskCurrent, this);
                contactTaskUI.Show();
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

                    int itemId = (int)((Border)sourceElement).Tag;
                    int idCardParent = (int)lb.Tag;
                    int idCardTarget = (int)target.Tag;

                    // find task existed in database by id 

                    var currentTask = taskRepository.GetById(itemId);

                    // update task
                    currentTask.Id_Card = idCardTarget;
                    taskRepository.Update(currentTask);
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
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void addNameCardButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCard_View addCard = new CreateCard_View(idTableCurrent, this);
            addCard.Show();

        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            //ShareBoard shareBoard = new ShareBoard();
            //shareBoard.Show();
            InviteMember_View addMember = new InviteMember_View(idTableCurrent);
            addMember.Show();
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

        public void ButtonStar_Click(object sender, RoutedEventArgs e)
        {
            if (iconStart.Foreground == Brushes.Black)
            {
                iconStart.Foreground = Brushes.Orange;
                Models.Table table = tableRepository.GetById(idTableCurrent);
                table.Pin = true;
                tableRepository.Update(table);
            }
            else
            {
                iconStart.Foreground = Brushes.Black;
                Models.Table table = tableRepository.GetById(idTableCurrent);
                table.Pin = false;
                tableRepository.Update(table);
            }

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Manage_Layout)
                {
                    Manage_Layout layout = (Manage_Layout)window;
                    layout.loadStart();
                }
            }
        }
    }
}

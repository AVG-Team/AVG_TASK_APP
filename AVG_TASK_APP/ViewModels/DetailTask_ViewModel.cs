using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AVG_TASK_APP.ViewModels
{
    public class DetailTask_ViewModel : ViewModelBase
    {
        private string _idTask;
        private string _nameTask;
        private string _nameCard;
        private string _description;

        private ITaskRepository taskRepository;
        private ICardRepository cardRepository;

        public string IdTask
        {
            get => _idTask;
            set
            {
                _idTask = value;
                OnPropertyChanged(nameof(IdTask));
                loadData();
            }
        }

        public string NameTask
        {
            get => _nameTask;
            set
            {
                _nameTask = value;
                OnPropertyChanged(nameof(NameTask));
            }
        }

        public string NameCard
        {
            get => _nameCard;
            set
            {
                _nameCard = value;
                OnPropertyChanged(nameof(NameCard));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand SaveDescriptionCommand { get; }
        public ICommand CancelDescriptionCommand { get; }

        public DetailTask_ViewModel()
        {
            taskRepository = new TaskRepository();
            cardRepository = new CardRepository();
            SaveDescriptionCommand = new ViewModelCommand(ExcuteSaveDescription, CanExcuteSaveDescription);
            CancelDescriptionCommand = new ViewModelCommand(ExcuteCancelDescription);

        }

        private void ExcuteCancelDescription(object obj)
        {
            Task task = taskRepository.GetById(int.Parse(IdTask));
            Description = task.Description;
        }

        private bool CanExcuteSaveDescription(object obj)
        {
            if (Description == null)
            {
                return false;
            }

            return true;
        }

        private void ExcuteSaveDescription(object obj)
        {
            Task task = taskRepository.GetById(int.Parse(IdTask));
            task.Description = Description;
            taskRepository.Update(task);
        }

        private void loadData()
        {
            Task task = taskRepository.GetById(int.Parse(IdTask));

            NameTask = task.Name;
            int idTmp = task.Id_Card;
            NameCard = cardRepository.GetById(idTmp).Name;
            Description = task.Description;
        }
    }
}

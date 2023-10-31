using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.DataAccess;
using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    public class NotifyItemViewModel : ViewModelBase
    {
        private INotifyRepository notifyRepository;

        private string _nameUser;
        private string _contentNotify;
        private DateTime _createdAt;
        private int _idNotify;
        private int _idUser;
        public int _pin;
        public string NameUser
        {
            get
            {
                return _nameUser;
            }
            set
            {
                _nameUser = value;
                OnPropertyChanged(nameof(NameUser));
            }
        }
        public string ContentNotify
        {
            get
            {
                return _contentNotify;
            }
            set
            {
                _contentNotify = value;
                OnPropertyChanged(nameof(ContentNotify));
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return _createdAt;
            }
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }

        public int IdNotify
        {
            get
            {
                return _idNotify;
            }
            set
            {
                _idNotify = value;
                OnPropertyChanged(nameof(IdNotify));
            }
        }

        public int Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                _pin = value;
                OnPropertyChanged(nameof(Pin));
            }
        }

        public NotifyItemViewModel()
        {
            notifyRepository = new NotifyRepository();
        }

        public string getNameUser()
        {
            UserModel user = notifyRepository.GetUserCreated(IdNotify);
            return user.Name;
        }
        public string getContentNotify(int idNotify)
        {
            NotifyRepository NotifyRepository = new NotifyRepository();
            Notify notify = NotifyRepository.GetById(idNotify);
            return notify.Content;
        }
        public DateTime getCreatedAt(int idNotify)
        {
            NotifyRepository NotifyRepository = new NotifyRepository();
            Notify notify = NotifyRepository.GetById(idNotify);
            return notify.Created_At;
        }
        public int getNotifyId(int idNotify)
        {
            NotifyRepository NotifyRepository = new NotifyRepository();
            Notify notify = NotifyRepository.GetById(idNotify);
            return notify.Id;
        }
        public int getNotifyPin(int idNotify)
        {
            NotifyRepository NotifyRepository = new NotifyRepository();
            Notify notify = NotifyRepository.GetById(idNotify);
            return notify.Pin;
        }
        public void setContentNotify(int idNotify, string contentNotify)
        {
            _contentNotify = contentNotify;
        }


    }
    

    // In your data access layer or repository, you can use Entity Framework Core
}

using AVG_TASK_APP.Migration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    public class NotifyViewModel : INotifyPropertyChanged
    {
        public string UserName { get; set; }
        public string NotifyContent { get; set; }
        public DateTime NotifyCreatedAt { get; set; } 
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<NotifyViewModel> notifyViewModelCollection;

            public ObservableCollection<NotifyViewModel> NotifyViewModelCollection
            {
                get { return notifyViewModelCollection; }
                set
                {
                    if (notifyViewModelCollection != value)
                    {
                        notifyViewModelCollection = value;
                        OnPropertyChanged("NotifyViewModelCollection");
                    }
                }
            }

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            // Other properties and methods of your NotifyViewModel class
        
        // Add other properties as needed
        public class YourViewModel : INotifyPropertyChanged
        {
            private ObservableCollection<NotifyViewModel> notifyViewModelCollection;

            public ObservableCollection<NotifyViewModel> NotifyViewModelCollection
            {
                get { return notifyViewModelCollection; }
                set
                {
                    if (notifyViewModelCollection != value)
                    {
                        notifyViewModelCollection = value;
                        OnPropertyChanged("NotifyViewModelCollection");
                    }
                }
            }

            // Implement OnPropertyChanged method
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    // In your data access layer or repository, you can use Entity Framework Core
}

using AVG_TASK_APP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.ViewModels
{
    internal class CardUserControlViewModel : ViewModelBase
    {
        private string _nameCard;
        private CardRepository cardRepository;

        public string NameCard
        {
            get
            {
                return _nameCard;
            }
            set
            {
                _nameCard = value;
                OnPropertyChanged(nameof(NameCard));

            }
        }

        public CardUserControlViewModel()
        {
            cardRepository = new CardRepository();
        }

        public void getNameCard(int idCard)
        {
            NameCard = cardRepository.GetById(idCard).Name;
        }


    }
}

using AVG_TASK_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories.Interface
{
    internal interface ICardRepository
    {
        void Add(Card card);
        void Update(Card card);
        void Remove(Card card);
        Card GetById(int idCard);
        IEnumerable<Card> GetAll(string sort = "desc");
        IEnumerable<Card> GetAllForTable(int idTable, string sort = "desc");
    }
}

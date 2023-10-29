using AVG_TASK_APP.Migration;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories
{
    internal class CardRepository : RepositoryBase, ICardRepository
    {
        private AppDbContext dbContext
        {
            get
            {
                var connection = GetConnection();
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySql(connection, serverVersion);

                return new AppDbContext(optionsBuilder.Options);
            }
        }

        public void Add(Card card)
        {
            dbContext.Cards.Add(card);
            dbContext.SaveChanges();
        }

        public IEnumerable<Card> GetAll(string sort = "desc")
        {
            if (sort.Equals("asc"))
            {
                return dbContext.Cards.OrderBy(s => s.Created_At).ToList();
            }
            return dbContext.Cards.OrderByDescending(s => s.Created_At).ToList();
        }

        public IEnumerable<Card> GetAllForTable(int idTable, string sort = "desc")
        {
            var cards = dbContext.Cards.Where(s => s.Id_Table == idTable).ToList();
            if (sort.Equals("asc"))
            {
                return cards.OrderBy(s => s.Created_At).ToList();
            }
            return cards.OrderByDescending(s => s.Created_At).ToList();
        }

        public Card GetById(int idCard)
        {
            return dbContext.Cards.Where(s => s.Id == idCard).FirstOrDefault();
        }

        public void Remove(Card card)
        {
            dbContext.Cards.Remove(card);
            dbContext.SaveChanges();
        }

        public void Update(Card card)
        {
            dbContext.Update(card);
            dbContext.SaveChanges();
        }
    }
}

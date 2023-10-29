using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVG_TASK_APP.Models;

namespace AVG_TASK_APP.Repositories.Interface
{
    internal interface ITableRepository
    {
        void Add(Table table);
        void Update(Table table);
        void Remove(Table table);
        Table GetById(int idTable);
        IEnumerable<Table> GetAll(string sort = "desc");
        IEnumerable<Table> GetAllForWorkspace(int idWorkspace, string sort = "desc");
    }
}

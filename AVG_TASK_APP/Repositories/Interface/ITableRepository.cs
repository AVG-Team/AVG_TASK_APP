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
        Workspace GetWorkspace(int idTable);
        int GetRole(int idTable);
        IEnumerable<Table> GetAll(string sort = "desc");
        IEnumerable<Table> GetAllForWorkspace(int idWorkspace, string sort = "desc");
        IEnumerable<Table> GetAllForUser(string sort = "desc");
        IEnumerable<Table> GetByContainName(string name, string sort = "desc");
    }
}


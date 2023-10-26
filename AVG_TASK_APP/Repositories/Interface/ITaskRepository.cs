using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVG_TASK_APP.Models;

namespace AVG_TASK_APP.Repositories.Interface
{
    internal interface ITaskRepository
    {
        void Add(Models.Task task);
        void Update(Models.Task task);
        void Remove(Models.Task task);
        Models.Task GetById(int idTask);
        IEnumerable<Models.Task> GetAll();
        IEnumerable<Models.Task> GetAllForCard(int idCard);
    }
}

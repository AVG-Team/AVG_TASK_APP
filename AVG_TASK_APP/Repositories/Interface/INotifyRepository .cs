using AVG_TASK_APP.Models;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Repositories.Interface
{
    interface INotifyRepository
    {
        void Add(Notify notify);
        void Update(Notify notify);
        void Remove(Notify notify);
        IEnumerable<Notify> GetAll();
        Notify GetById(int id);

        UserModel GetUserCreated(int id);
    }
}

using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    interface IWorkspaceReposity
    {
        void Add(Workspace workspace);
        void Update(Workspace workspace);
        void Remove(Workspace workspace);
        Workspace GetByNameForUser(string name, UserModel user);
        Workspace GetByCode(string code);
        Workspace GetById(int id);
        bool AddUserToWorkspace(Workspace workspace, UserModel user);
        IEnumerable<Workspace> GetAll(string sort = "desc");
        IEnumerable<Workspace> GetAllForUser(string sort = "desc");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AVG_TASK_APP.Models
{

    public class UserModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Salt { get; set; }
        public int Level { get; set; }
        public List<Notify> notifies;
        public List<UserWorkspace> userWorkspaces;
        public List<UserTable> userTables;
        public List<UserTask> userTasks;
        public List<Comment> comments;
   
    }
}

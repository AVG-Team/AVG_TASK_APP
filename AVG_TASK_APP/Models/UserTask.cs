using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Id_Task { get; set; }
        public UserModel User { get; set; }
        public Task Task { get; set; }
    }
}

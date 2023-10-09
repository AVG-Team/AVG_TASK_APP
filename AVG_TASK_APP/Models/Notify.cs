using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class Notify
    {
        public int Id { get; set; }       
        public string Content { get; set; }
        public int Pin { get; set; }
        public int Id_User { get; set; }
        public UserModel User { get; set; }
    }
}

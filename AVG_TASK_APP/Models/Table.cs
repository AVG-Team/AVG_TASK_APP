using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Id_Workspace { get; set; }
        public string Name { get; set; }
        public bool Pin { get; set; }
        public bool Visible { get; set; }
        public string Code { get; set; }
        public DateTime? Deleted_At { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List<UserTable> UserTables;
        public Workspace Workspace { get; set; }
        public List<Task> Tasks;
        public List<Card> Cards;
    }
}

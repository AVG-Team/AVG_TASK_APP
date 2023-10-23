using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AVG_TASK_APP.Models
{
    public class Workspace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool Visible { get; set; }
        public string Code { get; set; }
        public DateTime? Deleted_At { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List<UserWorkspace> userWorkspaces { get; set; }
        public List<Table> tables { get; set; }
    }
}

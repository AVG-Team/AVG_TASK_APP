using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class MiniTask
    {
        public int Id { get; set; }
        public int Id_Task { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Task Task { get; set; }
    }
}

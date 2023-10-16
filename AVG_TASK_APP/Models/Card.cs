using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List <Task> tasks { get; set; }
    }
}

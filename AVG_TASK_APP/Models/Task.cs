using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Estimate {  get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? Deleted_At { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public int Id_Table {  get; set; }
        public int Id_Card { get; set; }
        public Card Card { get; set; }
        public Table Table { get; set; }
        public List<UserTask> UserTasks;
        public List<MiniTask> MiniTasks;
        public List<Comment> Comments;
    }
}

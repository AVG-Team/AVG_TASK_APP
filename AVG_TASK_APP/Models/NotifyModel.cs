using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    [Table("Notify")]
    public class NotifyModel
    {

        public int Id { get; set; }
        public int Id_User {  get; set; }
        public virtual UserModel UserModel { get; set; }
        public string Content { get; set; }
        public bool Pin { get; set; }
        public UserModel User { get; set; }
    }
}

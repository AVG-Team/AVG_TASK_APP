using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class UserWorkspace
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Id_Workspace { get; set; }
        public int Role { get; set; }
        public UserModel User { get; set; }
        public Workspace Workspace { get; set; }
    }
}

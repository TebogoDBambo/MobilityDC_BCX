using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Models
{
    public class tTaskDetailUser
    {
        public tTaskDetailUser()
        {
            tTaskDetailUserStatus = new HashSet<tTaskDetailUserStatu>();
        }

        public int TaskDetailUserID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> TaskDetailId { get; set; }
        public bool IsCurrentTaskUser { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedByUserID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string DolfinUser { get; set; }

        public virtual tTaskDetail tTaskDetail { get; set; }
        public virtual ICollection<tTaskDetailUserStatu> tTaskDetailUserStatus { get; set; }
    }
}

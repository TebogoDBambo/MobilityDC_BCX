using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Models
{
    public class tTaskDetailUserStatu
    {
        public int TaskDetailUserStatusID { get; set; }
        public int TaskDetailUserID { get; set; }
        public byte StatusID { get; set; }
        public byte ExceptionInd { get; set; }

        public virtual tTaskDetailUser tTaskDetailUser { get; set; }
    }
}

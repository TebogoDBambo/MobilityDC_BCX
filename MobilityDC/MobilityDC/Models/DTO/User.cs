using System;
using System.Collections.Generic;
using System.Text;

namespace MobilityDC.Models.DTO
{
    public class User
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int EquipmentId { get; set; }
        public string Password { get; set; }
        public bool MoveToException { get; set; }
    }

}

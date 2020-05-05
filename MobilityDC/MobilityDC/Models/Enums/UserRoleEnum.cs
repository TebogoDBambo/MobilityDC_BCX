using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Api.Models.Enums
{
    public enum UserRole : int
    {
        Sealer = 1,
        FinePicker = 2,
        BulkPicker = 3,
        ForkliftDrivers = 4,
        WHSServices = 5,
        UCSAdmin = 6,
        Manifest = 7,
        Checker = 8,
        Receiver = 9,
        Admin = 10,
        ExceptionPicker = 11,
    }
}

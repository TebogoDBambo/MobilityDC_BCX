using System;
using System.Collections.Generic;
using System.Text;

namespace MobilityDC.Models.Commons
{
    public class AppSession
    {
        public static string AccessToken { get; set; }
        public static int TimeOut { get; set; }
        public static string EquipmentID { get; set; }
        public static string CompanyId { get; set; }
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public static int ScannerId { get; set; }
        public static int ExecutingTaskId { get; set; }
        public static int CurrentWaveId { get; set; }
        public static bool MoveToException { get; set; }

        public static List<int> TaskIds { get; set; }
        public static int LastRoleId { get; set; }
        public static bool IsHireWave { get; set; }
        public static bool BulkPickByQty { get; set; }
    }
}

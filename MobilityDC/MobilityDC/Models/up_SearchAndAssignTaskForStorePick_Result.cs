using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Models
{
    public class up_SearchAndAssignTaskForStorePick_Result
    {
        public Nullable<System.Guid> RowGuid { get; set; }
        public int TaskDetailId { get; set; }
        public int TaskHeaderId { get; set; }
        public int SourceId { get; set; }
        public int FromLocationID { get; set; }
        public int SKUID { get; set; }
        public int Quantity { get; set; }
        public int ToLocationID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<long> SourceTaskId { get; set; }
        public Nullable<long> SourceReqTaskDetailId { get; set; }
        public bool IsSentToQueue { get; set; }
        public string BoxNo { get; set; }
        public string QueueXML { get; set; }
        public Nullable<System.DateTime> DateSentToQueue { get; set; }
        public Nullable<bool> IsStoreDiscrepancy { get; set; }
        public Nullable<byte> CurrentStatusID { get; set; }
        public byte Sequence { get; set; }
        public string SKUDescr { get; set; }
        public string ProductTypeDescription { get; set; }
        public string SKUColour { get; set; }
        public string SKUcode { get; set; }
        public string SKUSize { get; set; }
        public string SKUBrand { get; set; }
        public string SKUFriendlyName { get; set; }
        public string SKUDescription { get; set; }
        public string FromLocationDescr { get; set; }
        public string ToLocationDescr { get; set; }
        public string FromLocationCode { get; set; }
        public string ToLocationCode { get; set; }
        public string PriorityDescr { get; set; }
        public Nullable<int> LastUserID { get; set; }
        public Nullable<int> LastRoleID { get; set; }
        public int TaskDetailUserID { get; set; }
        public int TaskDetailUserStatusID { get; set; }
        public string DocumentNo { get; set; }
        public Nullable<int> UserID { get; set; }
        public int FinalLocationID { get; set; }
        public string FinalLocationCode { get; set; }
        public string FinalLocationDescr { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Models
{
    public class TaskModel
    {
        public Guid? RowGuid { get; set; }
        public int TaskDetailId { get; set; }
        public int TaskHeaderId { get; set; }
        public int SourceId { get; set; }
        public int FromLocationID { get; set; }
        public int SKUID { get; set; }
        public int Quantity { get; set; }
        public int ToLocationID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public long? SourceTaskId { get; set; }
        public long? SourceReqTaskDetailId { get; set; }
        public bool IsSentToQueue { get; set; }
        public int? BoxNo { get; set; }
        public string QueueXML { get; set; }
        public DateTime? DateSentToQueue { get; set; }
        public bool? IsStoreDiscrepancy { get; set; }
        public byte? CurrentStatusID { get; set; }
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
        public int? LastUserID { get; set; }
        public int? LastRoleID { get; set; }
        public int TaskDetailUserID { get; set; }
        public int TaskDetailUserStatusID { get; set; }
        public string DocumentNo { get; set; }
        public int? UserID { get; set; }
        public int FinalLocationID { get; set; }
        public string FinalLocationCode { get; set; }
        public string FinalLocationDescr { get; set; }

        public int Status { get; set; }
        public string Barcode { get; set; }
        public string TaskType { get; set; }
        public int ByStore { get; set; }


    }
}

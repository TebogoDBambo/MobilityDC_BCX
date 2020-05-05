using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Models
{
    public class tTaskDetail
    {
        public tTaskDetail()
        {
            tTaskDetailUsers = new HashSet<tTaskDetailUser>();
        }

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
        public string SizeCode { get; set; }
        public string Colour { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<int> FinalLocationID { get; set; }
        public Nullable<int> StorageTypeID { get; set; }
        public int ManifestHeaderID { get; set; }
        public Nullable<int> VBoxId { get; set; }
        public Nullable<decimal> AvgCost { get; set; }
        public Nullable<decimal> SellingPriceInc { get; set; }
        public Nullable<decimal> ForeignCurrencyPrice { get; set; }
        public string ForeignCurrencyCode { get; set; }
        public Nullable<int> NoOfDaysReturnPolicy { get; set; }
        public Nullable<decimal> SellingPriceExc { get; set; }
        public Nullable<decimal> DiscountAmtInc { get; set; }
        public Nullable<decimal> ShippingAmtInc { get; set; }
        public Nullable<bool> NoStockPicked { get; set; }
        public Nullable<bool> IsPutAwayComplete { get; set; }
        public Nullable<int> OrigenalLocationID { get; set; }
        public string SizeCode1 { get; set; }
        public string SizeCode2 { get; set; }
        public Nullable<int> SizeSequence1 { get; set; }
        public Nullable<int> SizeSequence2 { get; set; }
        public byte ExceptionInd { get; set; }
        public Nullable<int> UniqueHireItemID { get; set; }
        public Nullable<int> SavedTaskDetailID { get; set; }
        
        public virtual ICollection<tTaskDetailUser> tTaskDetailUsers { get; set; }
    }
}

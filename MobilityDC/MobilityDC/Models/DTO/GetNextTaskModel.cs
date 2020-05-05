using System;
using System.Collections.Generic;
using System.Text;

namespace MobilityDC.Models.DTO
{
    public class GetNextTaskModel
    {
        public string FromLocationCode { get; set; }
        public string OriginalFromLocationCode { get; set; }
        public string FromLocationDescr { get; set; }
        public string Barcode { get; set; }
        public List<string> BarCodes { get; set; }
        public string ScannedBarcode { get; set; }
        public string SerialNumber { get; set; }
        public List<string> SerialNumbers { get; set; }
        public string SKUDescr { get; set; }
        public string ToLocationCode { get; set; }
        public string OriginalToLocationCode { get; set; }
        public string ToLocationDescr { get; set; }
        public string FinalLocationCode { get; set; }
        public string FinalLocationDescr { get; set; }
        public int Quantity { get; set; }
        public int OriginalQuantity { get; set; }
        public int ScannedQuantity { get; set; }
        public int RemainQuantity { get; set; }
        public int TaskDetailID { get; set; }
        public string BoxNo { get; set; }
        public bool IsHire { get; set; }
        public string TaskType { get; set; }
        public int DocumentNo { get; set; }
        public string FormattedSKU { get; set; }
        public int FromLocationID { get; set; }
        public int ToLocationID { get; set; }
        public int FinalLocationID { get; set; }
        public int OriginalFromLocationID { get; set; }
        public int OriginalToLocationID { get; set; }
        public int SKUID { get; set; }
        public string PriorityDescr { get; set; }
        public int SourceTaskID { get; set; }
        public int LastRoleID { get; set; }
        public int LastUserID { get; set; }
        public string SearchFromLocation { get; set; }
        public string SearchToLocation { get; set; }
        public string SearchBarcode { get; set; }
        public string SearchFinalLocation { get; set; }
        public string SearchProduct { get; set; }
        public string SearchDocument { get; set; }
        public string SearchWaveCode { get; set; }
        public int WaveID { get; set; }
        public bool TaskWasWIP { get; set; }
        public bool BulkPickByStore { get; set; }
        public bool SingleSKU { get; set; }
        public string LastBoxNumber { get; set; }
        public string LastFinalLocationCode { get; set; }
        public string PostbackAction { get; set; }
        public DateTime StartTime { get; set; }
        public string CSDocumentNo { get; set; }
        public bool NoStockPicked { get; set; }
        public int CurrentStatusID { get; set; }
        public int ItemsRemaining { get; set; }
        public bool IsNonUnique { get; set; }
        public bool IsEcommSearch { get; set; }

        public bool MoveToException { get; set; }

        //public int finepicklocatonid { get; set; }
        public string ExceptionZone { get; set; }
        public string PigeonHole { get; set; }
        public int UserId { get; set; }
        public string WaveDetailCode { get; set; }
        public List<int> execTaskId { get; set; }
        public int BulkFromLocationID { get; set; }
        public string BulkFromLocationCode { get; set; }
        public string BulkFromLocationDescr { get; set; }

        public string ProductTypeDescription { get; set; }
        public string SKUColour { get; set; }
        public string SKUSize { get; set; }
        public string SKUBrand { get; set; }
        public string SKUFriendlyName { get; set; }
        public string SKUDescription { get; set; }
    }
}

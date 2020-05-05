using System;
using System.Collections.Generic;
using System.Text;

namespace MobilityDC.Models.DTO
{
    public class BulkPickModelSearch
    {
        public string SearchFromLocationCode { get; set; }
        public string SearchBarcode { get; set; }
        public string SearchFinalLocationCode { get; set; }
        public string SearchDocumentNo { get; set; }
        public bool IshireItems { get; set; }
        public bool WIP { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MobilityDC.Models
{
    public class FinePickModelSearch
    {
        public string SearchFromLocationCode { get; set; }

        public string SearchBarcode { get; set; }

        public string SearchFinalLocationCode { get; set; }

        public string SearchDocumentNo { get; set; }

        public bool WIP { get; set; }
        public string SearchSerialNumber { get; set; }

        public string SearchToLocationCode { get; set; }

        public string SearchProduct { get; set; }

        public int? PriorityId { get; set; }

        public bool StorePick { get; set; }

        public string TaskType { get; set; }

        public int UserRole { get; set; }

        public string LastFinalLocationCode { get; set; }
    }
}

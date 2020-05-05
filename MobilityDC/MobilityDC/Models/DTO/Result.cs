using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilityDC.Api.Models.DTO
{
    public class Result
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int Value { get; set; }
        public object Data { get; set; }
        public bool IsException { get; set; }

    }
}

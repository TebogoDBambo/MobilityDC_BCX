using System;
namespace MobilityDC.Models.DTO
{
    public class AuthResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string MessageToken { get; set; }
        public string access_token { get; set; }
    }
}

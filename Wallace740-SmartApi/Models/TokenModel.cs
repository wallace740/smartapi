using System;
namespace Wallace740_SmartApi.Models
{
    public class TokenModel
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}

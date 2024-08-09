using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAppMvc.Models.Requests
{
    public class ApiKeyRequest
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public int ExchangeId { get; set; }
        public Guid UserId { get; set; }
    }
}
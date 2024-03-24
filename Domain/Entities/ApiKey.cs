using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Entities
{
    public class ApiKey : IEntity
    {
        public string PublicKey { get; private set; }
        public string PrivateKey { get; private set; }
        public Guid UserId { get; private set; }
        public int ExchangeId { get; private set; }
    }
}
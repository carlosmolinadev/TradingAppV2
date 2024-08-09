

using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Domain.Entities
{
   public class ApiKey : IEntity
   {
      public ApiKey(string publicKey, string privateKey, int exchangeId, Guid madre)
      {
         PublicKey = publicKey;
         PrivateKey = privateKey;
         UserId = madre;
         ExchangeId = exchangeId;
      }

      public string PublicKey { get; private set; }
      public string PrivateKey { get; private set; }
      public Guid UserId { get; private set; }
      public int ExchangeId { get; private set; }
   }
}
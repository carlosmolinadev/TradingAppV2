using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Domain.Entities
{
   public class TradeSetting : IEntity
   {
      public int Id { get; private set; }
      public decimal RiskReward { get; private set; }
      public string Name { get; private set; }
      public int RetryAttempt { get; private set; }
      public int SkipAttempt { get; private set; }
      public bool CandleClosed { get; private set; }
      public decimal RiskPerTrade { get; private set; }
      public Guid UserId { get; private set; }
   }
}

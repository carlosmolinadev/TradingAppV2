using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Models.Views
{
   public class TradeSettingRequest
   {
      public int Id { get; set; }
      public decimal RiskReward { get; set; }
      public string Name { get; set; }
      public int RetryAttempt { get; set; }
      public int SkipAttempt { get; set; }
      public bool CandleClosed { get; set; }
      public decimal RiskPerTrade { get; set; }
      public Guid UserId { get; set; }
   }
}

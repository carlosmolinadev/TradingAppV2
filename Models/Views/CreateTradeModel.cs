using TradingAppMvc.Models.Requests;
using TradingAppMvc.Models.Shared;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Models.Views
{
   public class CreateTradeModel
   {
      public CreateTradeRequest Request { get; set; } = new();
      public int ExchangeId { get; set; }
      public string Symbol { get; set; }
      public string OrderType { get; set; }
      public decimal? Amount { get; set; }
      public decimal? Quantity { get; set; }
      public string Side { get; set; }
      public string Mode { get; set; }
      public List<TradeOrderRequest> TradeOrders { get; set; } = new();
      public Guid? TradingSettingId { get; set; }
      public string? ErrorMessage { get; set; }
   }
}


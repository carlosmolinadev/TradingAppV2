using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradingAppMvc.Models.Responses;

namespace TradingAppMvc.Models.Requests
{
   public class CreateTradeRequest
   {
      // public CreateTradeOptionList CreateTradeOptionList { get; set; }
      public int ExchangeId { get; set; }
      public string Symbol { get; set; }
      public string OrderType { get; set; }
      [Required]
      public decimal? Amount { get; set; }
      public decimal? Quantity { get; set; }
      public string Side { get; set; }
      public string Category { get; set; }
      public List<TradeOrderRequest> TradeOrders { get; set; } = new();
      public Guid? TradingSettingId { get; set; }
   }
}

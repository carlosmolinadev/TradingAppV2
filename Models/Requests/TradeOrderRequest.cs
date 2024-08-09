namespace TradingAppMvc.Models.Requests
{
   public class TradeOrderRequest
   {
      public decimal Price { get; set; }
      public decimal Amount { get; set; }
      public decimal Quantity { get; set; }
      public string Type { get; set; }
      public int Parent { get; set; }
      public string Conditional { get; set; }
   };

}

using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Domain.Entities
{
   public class OrderType : IEntity
   {
      public int Id { get; set; }
      public string Value { get; set; }
      public int Binance { get; set; }
      public enum Flag
      {
         Limit = 1,
         Market,
         Stop,
         StopMarket,
         TakeProfit,
         TakeProfitMarket,
         TrailingStop
      }
   }
}


using System.ComponentModel;
using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Domain.Entities
{
   public class Exchange : IEntity
   {
      public int Id { get; private set; }
      public string Name { get; private set; }
      // public enum Flag
      // {
      //    [Description("Binance")]
      //    Binance,
      //    [Description("BingX")]
      //    BingX
      // }
   }

   public enum ExchangeId
   {
      [Description("Binance")]
      Binance,
      [Description("BingX")]
      BingX
   }
}
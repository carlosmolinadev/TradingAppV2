

using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Domain.Entities
{
   public class Symbol : IEntity
   {
      private Symbol() { }
      public Symbol(string value, int quantityPrecision, int pricePrecision, string baseAsset, string marginAsset, int exchangeId)
      {
         Value = value;
         QuantityPrecision = quantityPrecision;
         PricePrecision = pricePrecision;
         ExchangeId = exchangeId;
         BaseAsset = baseAsset;
         MarginAsset = marginAsset;
      }

      public int Id { get; private set; }
      public string Value { get; init; }
      public int QuantityPrecision { get; private set; }
      public int PricePrecision { get; private set; }
      public string MarginAsset { get; private set; }
      public string BaseAsset { get; private set; }
      public int ExchangeId { get; private set; }
   }
}
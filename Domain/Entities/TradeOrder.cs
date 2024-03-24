using TradingAppMvc.Domain.Enums;
using TradingAppMvc.Domain.Exceptions;

namespace TradingAppMvc.Domain.Entities
{
    public partial class TradeOrder
    {
        public int Id { get; set; }
        public OrderType Type { get; set; }
        public string Side { get; private set; }
        public string Status { get; private set; }
        public string Category { get; private set; }
        public string EntryType { get; private set; }
        public decimal LotSize { get; private set; }
        public int ParentTradeOrder { get; private set; }
        public decimal StopPrice { get; private set; }
        public decimal LimitPrice { get; private set; }
        public string? Symbol { get; set; }
        public Guid AccountId { get; private set; }
        public bool ReduceOnly { get; private set; }


        public void SetSide(string side)
        {
            if(TradeSide.IsValid(side)){

            }

            throw new EntityException("Invalid value for side");
        }

        public void SetCategory(string category)
        {
            if (category == TradeCategory.Open || category == TradeCategory.StopLoss || category == TradeCategory.TakeProfit)
            {
                Category = category;
            }
            throw new ArgumentException("Invalid value for category");
        }

        public void SetEntryType(string entryType)
        {
            throw new ArgumentException("Invalid value for entry type");
        }

        public struct TradeCategory
        {
            public bool IsValid(string value){
                return value == Open || value == StopLoss || value == TakeProfit;
            }
            public const string Open = "OPEN";
            public const string StopLoss = "STOP_LOSS";
            public const string TakeProfit = "TAKE_PROFIT";
        }

        public struct TradeType
        {
            public const string New = "NEW";
            public const string PartiallyFilled = "PARTIALLY_FILLED";
            public const string Filled = "FILLED";
            public const string Canceled = "CANCELED";
            public const string PendingCancel = "PENDING_CANCEL";
            public const string Rejected = "REJECTED";
            public const string Expired = "EXPIRED";
        }


    }
}
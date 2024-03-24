namespace TradingAppMvc.Domain.Entities
{
    public class EntryStrategy
    {
        public int Id { get; private set; }
        public decimal RiskReward { get; private set; }
        public bool AutoStopLoss { get; private set; }
        public int RetryAttempt { get; private set; }
        public bool CandleClosedEntry { get; private set; }
        public decimal ActivationPrice { get; private set; }
        public int TradeOrderId { get; private set; }
        public decimal RiskPerTrade { get; private set; }
    }
}
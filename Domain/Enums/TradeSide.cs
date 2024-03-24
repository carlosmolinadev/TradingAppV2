namespace TradingAppMvc.Domain.Enums
{
    public static class TradeSide
    {
        public static bool IsValid(string value)
        {
            return value == Buy || value == Sell;
        }
        public static readonly string Buy = "BUY";
        public static readonly string Sell = "SELL";
    }
}
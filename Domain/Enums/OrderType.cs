namespace TradingAppMvc.Domain.Enums
{
    public enum OrderType
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
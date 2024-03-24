
namespace TradingAppMvc.Domain.Enums
{
    public enum OrderStatus
    {
        New = 1,
        PartiallyFilled,
        Filled,
        Canceled,
        PendingCancel,
        Rejected,
        Expired
    }
}

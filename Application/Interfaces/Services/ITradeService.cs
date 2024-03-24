
using TradingAppMvc.Application.Models.Shared;

namespace TradingAppMvc.Application.Interfaces.Services
{
    public interface ITradeService
    {
        public void LoadSymbolByExchange(int exchangeId);
    }
}
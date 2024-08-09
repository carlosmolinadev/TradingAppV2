using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Models.Requests;
using TradingAppMvc.Models.Shared;

namespace TradingAppMvc.Services
{
    public interface ITradeService
    {
        // Task<bool> CreateTrade(CreateTradeRequest request, Guid userId);
        Task<bool> SaveApiKey(ApiKey apiKey);
        Task<ApiKey> GetApiKey(Guid userId);
    }
}
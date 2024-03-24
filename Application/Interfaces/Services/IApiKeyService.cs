
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Application.Interfaces.Services
{
    public interface IApiKeyService
    {
        public Task<ApiKey?> GetApiKey(Guid userId, int exchangeId);
    }
}
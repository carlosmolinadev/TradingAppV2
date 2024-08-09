using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Models.Requests;
using TradingAppMvc.Models.Responses;

namespace TradingAppMvc.Services
{
    public interface ISettingsService
    {
        public Task<ApplicationResponse<ApiKey>> GetApiKey(Guid userId, int exchangeId);
        public Task<ApplicationResponse<ApiKey>> SetApiKey(Guid userId, ApiKeyRequest apiKey);
        public Task<ApplicationResponse<ApiKey>> UpdateApiKey(Guid userId, ApiKeyRequest apiKey);
    }
}
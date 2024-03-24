using Microsoft.Extensions.Caching.Memory;
using TradingAppMvc.Application.Interfaces.Services;
using TradingAppMvc.Application.Models.Responses;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Domain.Repositories;
using TradingAppMvc.Infraestructure.Repositories.Models;

namespace TradingAppMvc.Application.Features
{
    public class ApiKeyService : IApiKeyService
    {

        private readonly IRepository<ApiKey> _apiKeyRepository;
        private readonly IMemoryCache _memoryCache;
        public ApiKeyService(IRepository<ApiKey> apiKeyRepository, IMemoryCache memoryCache)
        {
            _apiKeyRepository = apiKeyRepository;
            _memoryCache = memoryCache;
        }

        public async Task<ApiKey?> GetApiKey(Guid userId, int exchangeId)
        {
            var apiKey = _memoryCache.Get<ApiKey>(string.Concat(userId, "-", exchangeId));
            if (apiKey is null)
            {
                var apiKeyParameters = new QueryParameter().AddEqualCondition(nameof(ApiKey.UserId), userId).AddEqualCondition(nameof(ApiKey.ExchangeId), exchangeId);
                apiKey = (await _apiKeyRepository.GetEntitiesByParameterAsync(apiKeyParameters)).FirstOrDefault();
                _memoryCache.Set(string.Concat(userId, "-", exchangeId), apiKey, TimeSpan.FromMinutes(20));
            }
            return apiKey;
        }
    }
}
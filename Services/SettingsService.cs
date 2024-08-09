using Microsoft.Extensions.Caching.Memory;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Repositories;
using TradingAppMvc.Application.Models.Repository;
using TradingAppMvc.Services;
using TradingAppMvc.Models.Requests;
using TradingAppMvc.Models.Responses;

namespace TradingAppMvc.Application.Features
{
   public class SettingsService : ISettingsService
   {
      private readonly IRepository<ApiKey> _apiKeyRepository;
      public SettingsService(IRepository<ApiKey> apiKeyRepository)
      {
         _apiKeyRepository = apiKeyRepository;
      }

      public async Task<ApplicationResponse<ApiKey>> GetApiKey(Guid userId, int exchangeId)
      {
         var response = new ApplicationResponse<ApiKey>();
         try
         {
            var apiKeyParameters = new QueryParameter().EqualCondition(nameof(ApiKey.UserId), userId).EqualCondition(nameof(ApiKey.ExchangeId), exchangeId);
            var result = (await _apiKeyRepository.FindByParameter(apiKeyParameters)).FirstOrDefault();
            if (result == null)
            {
               response.SetError("Could not find api key");
               return response;
            }
            response.SetResult(result);
         }
         catch (Exception)
         {
            response.SetError("Could not complete request, try later");
         }
         return response;
      }

      public async Task<ApplicationResponse<ApiKey>> SetApiKey(Guid userId, ApiKeyRequest request)
      {
         var response = new ApplicationResponse<ApiKey>();
         try
         {
            var apiKey = new ApiKey(request.PublicKey, request.PrivateKey, request.ExchangeId, userId);
            var result = await _apiKeyRepository.AddEntity(apiKey);
            if (result == null)
            {
               response.SetError("Could not set api key");
               return response;
            }
            response.SetResult(result);
         }
         catch (Exception)
         {
            response.SetError("Could not complete request, try later");
         }
         return response;
      }

      public async Task<ApplicationResponse<ApiKey>> UpdateApiKey(Guid userId, ApiKeyRequest request)
      {
         var response = new ApplicationResponse<ApiKey>();
         try
         {
            var apiKey = new ApiKey(request.PublicKey, request.PrivateKey, request.ExchangeId, userId);
            var result = await _apiKeyRepository.UpdateEntity(apiKey);
            if (!result)
            {
               response.SetError("Could not update api key");
               return response;
            }
            response.SetResult(apiKey);
         }
         catch (Exception)
         {
            response.SetError("Could not complete request, try later");
         }
         return response;
      }
   }
}

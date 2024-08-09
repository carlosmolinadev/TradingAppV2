using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using TradingAppMvc.Models.Requests;
using TradingAppMvc.Application.Models.Repository;
using TradingAppMvc.Services;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Repositories;
using TradingAppMvc.Models.Responses;

namespace TradingAppMvc.Services
{
   public class TradeService : ITradeService
   {
      private readonly IRepository<TradeOrder> _tradeRepository;
      private readonly IRepository<ApiKey> _apiKeyRepository;
      private readonly IRepository<Symbol> _symbolRepository;
      private readonly IRepository<OrderType> _orderRepository;
      private readonly IBinanceRestClient _binanceClient;

      public TradeService(IRepository<TradeOrder> tradeRepository, IBinanceRestClient binanceClient)
      {
         _tradeRepository = tradeRepository;
         _binanceClient = binanceClient;
      }

      public async Task<ApplicationResponse<CreateTradeResponse>> CreateTrade(CreateTradeRequest request, Guid userId)
      {
         var response = new ApplicationResponse<CreateTradeResponse>();
         try
         {
            var apiKeyCondition = new List<QueryCondition>() { new QueryCondition(nameof(ApiKey.UserId), userId) };
            var apiKey = (await _apiKeyRepository.FindByParameter(new QueryParameter(apiKeyCondition))).First();
            var symbolCondition = new List<QueryCondition>{
               new (nameof(Symbol.Value), request.Symbol),
               new (nameof(Symbol.ExchangeId), request.ExchangeId),
            };

            var symbol = (await _symbolRepository.FindByParameter(new QueryParameter(symbolCondition))).First().Value;
            // if (request.ExchangeId == (int)ExchangeId.Binance)
            // {
            //    orderType = await BinanceOrderType(request.OrderTypeId);
            // _binanceClient.SetApiCredentials(new CryptoExchange.Net.Authentication.ApiCredentials(key.PublicKey, key.PrivateKey));
            // var result = await _binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)request.Side, (Binance.Net.Enums.FuturesOrderType)orderType, request.Quantity, request.Price, stopPrice: request.StopPrice);
            // if (result.Success)
            // {

            //    return true;
            // }
            // }
            // return false;
         }
         catch (System.Exception)
         {

            throw;
         }
         return response;
      }


      public Task<ApiKey> GetApiKey(Guid userId)
      {
         throw new NotImplementedException();
      }

      public Task<bool> SaveApiKey(ApiKey apiKey)
      {
         throw new NotImplementedException();
      }

      private async Task<int> BinanceOrderType(int orderType)
      {
         var query = new List<QueryCondition>(){
            new QueryCondition(nameof(OrderType.Binance), orderType)
         };
         return (await _orderRepository.FindByParameter(new QueryParameter(query))).First().Id;
      }

      public class CreateTradeResponse
      {
      }
   }
}
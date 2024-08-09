using Microsoft.AspNetCore.Mvc.Rendering;
using TradingAppMvc.Models.Responses;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Repositories;
using TradingAppMvc.Application.Models.Repository;
using TradingAppMvc.Services;

namespace TradingAppMvc.Services
{
   public class PromptService : IPromptService
   {
      private readonly IExchangeRepository _exchangeRepo;
      private readonly IReadRepository<TradeSetting> _tradeSettingRepo;
      private readonly IReadRepository<Symbol> _symbolRepo;
      private readonly IReadRepository<OrderType> _orderTypeRepo;

      public PromptService(IExchangeRepository exchangeRepo, IReadRepository<TradeSetting> tradeParameterRepo, IReadRepository<Symbol> symbolRepo, IReadRepository<OrderType> orderTypeRepo)
      {
         _exchangeRepo = exchangeRepo;
         _tradeSettingRepo = tradeParameterRepo;
         _symbolRepo = symbolRepo;
         _orderTypeRepo = orderTypeRepo;
      }

      public async Task<ApplicationResponse<CreateTradeOptionList>> GetCreateTradeOptionLists(Guid userId, string currency)
      {
         var response = new ApplicationResponse<CreateTradeOptionList>();
         try
         {
            var options = new CreateTradeOptionList();
            var exchanges = await _exchangeRepo.FindAll();
            if (!exchanges.Any())
            {
               response.SetError("Unable to find exchanges for user.");
               return response;
            }
            options.Exchanges = exchanges.Select(e => new SelectListItem(e.Name, e.Id.ToString())).ToList();
            var symbolConditions = new List<QueryCondition>{
               new(nameof(Symbol.ExchangeId), exchanges.First().Id),
               new(nameof(Symbol.MarginAsset), currency),
            };

            var symbolList = await _symbolRepo.FindByParameter<string>(new QueryParameter(symbolConditions), ["value"]);

            if (!symbolList.Any())
            {
               response.SetError("Unable to find symbol to trade.");
               return response;
            }
            options.Symbols = symbolList.Select(x => new SelectListItem(x, x)).ToList();

            var orderTypes = await _orderTypeRepo.FindAll();
            options.OrderTypes = orderTypes.Select(o => new SelectListItem(o.Value, o.Id.ToString())).ToList();
            response.SetResult(options);
         }
         catch (System.Exception)
         {
            response.SetError("Unexpected error occured, try again later");
         }

         return response;
      }

      public async Task<ApplicationResponse<IEnumerable<SelectListItem>>> GetBalanceOptions(Guid userId, int exchangeId)
      {
         var response = new ApplicationResponse<IEnumerable<SelectListItem>>([]);
         try
         {
            var exchange = await _exchangeRepo.FindById(exchangeId);
            if (exchange == null)
            {
               response.SetError("Unable to get exchanges");
               return response;
            }
            // var options = account.GetAssets().Select(a => new SelectListItem(
            //    $"{a.Currency}~{a.AvailableBalance}",
            //    string.Format("{0}: {1}", a.Currency, a.AvailableBalance)
            // ));

            // response.SetResult(options);
            return response;
         }
         catch (Exception ex)
         {
            response.SetError(ex.Message);
            return response;
         }
      }

      public async Task<ApplicationResponse<List<TradeSetting>>> GetTradingSettings(Guid userId)
      {
         var response = new ApplicationResponse<List<TradeSetting>>([]);
         var tradeSettings = await _tradeSettingRepo.FindByParameter(new QueryParameter().EqualCondition(nameof(TradeSetting.UserId), userId));
         if (tradeSettings.Any())
         {
            response.SetResult(tradeSettings.ToList());
         }
         return response;
      }
   }
}

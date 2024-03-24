using System.Linq;
using CryptoExchange.Net.CommonObjects;
using TradingAppMvc.Application.Interfaces.Services;
using TradingAppMvc.Application.Models.Pages;
using TradingAppMvc.Application.Models.Responses;
using TradingAppMvc.Application.Models.Shared;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Domain.Enums;
using TradingAppMvc.Domain.Repositories;
using TradingAppMvc.Infraestructure.Repositories.Models;

namespace TradingAppMvc.Application.Features
{
    public class PromptService : IPromptService
    {
        private readonly IReadRepository<Exchange> _exchangeRepo;
        private readonly IReadRepository<Account> _accountRepo;
        private readonly IAccountService _accountService;
        private readonly IReadRepository<TradeSetting> _tradeSettingRepo;
        private readonly IReadRepository<Domain.Entities.Symbol> _symbolRepo;

        public PromptService(IReadRepository<Exchange> exchangeRepo, IAccountService accountService, IReadRepository<Account> accountRepo, IReadRepository<TradeSetting> tradeParameterRepo, IReadRepository<Domain.Entities.Symbol> symbolRepo)
        {
            _exchangeRepo = exchangeRepo;
            _accountService = accountService;
            _accountRepo = accountRepo;
            _tradeSettingRepo = tradeParameterRepo;
            _symbolRepo = symbolRepo;
        }

        public async Task<BaseResponse<FuturesPage>> GetFuturesPageData(Guid userId, FuturesPage model)
        {
            var response = new BaseResponse<FuturesPage>();
            response.SetResult(model);
            try
            {
                var exchanges = await _exchangeRepo.GetAllEntitiesAsync();
                var orderTypeList = (OrderType[])Enum.GetValues(typeof(OrderType));
                var orderTypeOptions = new List<SelectOptionPrompt>();
                var account = await _accountRepo.GetEntitiesByParameterAsync(new QueryParameter().AddEqualCondition(nameof(Account.UserId), userId));
                var tradeSettings = await _tradeSettingRepo.GetEntitiesByParameterAsync(new QueryParameter().AddEqualCondition(nameof(TradeSetting.UserId), userId));

                if (!exchanges.Any() || account == null)
                {
                    response.SetError("Unable to find exchanges for user.");
                    return response;
                }
                var userExchanges = account.Join(exchanges, acc => acc.ExchangeId, ex => ex.Id, (acc, ex) => new SelectOptionPrompt(ex.Id.ToString(), ex.Name)).ToList();


                var symbolList = await _symbolRepo.GetEntityPropertiesByParameterAsync<string>(new QueryParameter()
                                .AddEqualCondition(nameof(Domain.Entities.Symbol.ExchangeId), exchanges.First().Id), [nameof(Domain.Entities.Symbol.Value)]);

                for (int i = 0; i < orderTypeList.Length; i++)
                {
                    var value = i + 1;
                    orderTypeOptions.Add(new SelectOptionPrompt(value.ToString(), orderTypeList[i].ToString()));
                }

                if (!symbolList.Any())
                {
                    response.SetError("Unable to find symbol to trade.");
                    return response;
                }
                if (tradeSettings.Any())
                {
                    model.TradeSetting = tradeSettings.First();
                }
                model.OrderTypeOptions = orderTypeOptions;
                model.ExchangeOptions.AddRange(userExchanges);
                model.SymbolOptions = symbolList.Select(x => new SelectOptionPrompt(x)).ToList();
                response.SetSuccess();
            }
            catch (Exception)
            {
                response.SetError("Unexpected error while loading futures page");
            }
            return response;
        }

        public async Task<List<Prompt>> GetTradeSettingPrompts(Guid userId)
        {
            var prompts = new List<Prompt>
            {

            };
            return prompts;
        }

        public async Task<BaseResponse<IEnumerable<SelectOptionPrompt>>> GetBalanceOptions(Guid userId, int exchangeId)
        {
            var response = new BaseResponse<IEnumerable<SelectOptionPrompt>>();
            try
            {
                var exchange = await _exchangeRepo.GetEntityByIdAsync(exchangeId);
                var account = (await _accountRepo.GetEntitiesByParameterAsync(new QueryParameter().AddEqualCondition(nameof(Account.UserId), userId).AddEqualCondition(nameof(Account.ExchangeId), exchangeId))).FirstOrDefault();
                if (exchange == null)
                {
                    response.SetError("Unable to get exchanges");
                    return response;
                }
                if (account == null)
                {
                    response.SetError("Could not find account for user");
                    return response;
                }
                var accountResponse = await _accountService.GetFuturesBalance(account);
                if (!accountResponse.Success)
                {
                    response.SetError("Could not load assets for account");
                    return response;
                }
                account = accountResponse.Result;
                var options = account.GetAssets().Select(a => new SelectOptionPrompt(

                    $"{a.Currency}~{a.AvailableBalance}",
                    string.Format("{0}: {1}", a.Currency, a.AvailableBalance)
                ));

                response.SetResult(options);
                response.SetSuccess();
                return response;
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
                return response;
            }
        }
    }
}
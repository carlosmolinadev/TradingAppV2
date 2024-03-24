using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Caching.Memory;
using TradingAppMvc.Application.Interfaces.Services;
using TradingAppMvc.Application.Models.Responses;
using TradingAppMvc.Application.Models.Shared;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Domain.Repositories;
using TradingAppMvc.Infraestructure.Repositories.Models;

namespace TradingAppMvc.Infraestructure.Services
{
    public class AccountService : IAccountService
    {

        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Symbol> _symbolRepo;
        private readonly IReadRepository<Exchange> _exchangeRepository;
        private readonly IBinanceRestClient _binanceClient;
        private readonly IApiKeyService _apiKeyService;
        private readonly IMemoryCache _memoryCache;

        public AccountService(IReadRepository<Exchange> exchangeRepository, IRepository<Account> accountRepository, IBinanceRestClient binanceClient, IApiKeyService apiKeyService, IMemoryCache memoryCache, IRepository<Symbol> symbolRepo)
        {
            _accountRepository = accountRepository;
            _exchangeRepository = exchangeRepository;
            _binanceClient = binanceClient;
            _memoryCache = memoryCache;
            _apiKeyService = apiKeyService;
            _symbolRepo = symbolRepo;
        }

        public async Task<BaseResponse<Account>> GetFuturesBalance(Account account)
        {
            var result = new BaseResponse<Account>();
            var apiKey = await _apiKeyService.GetApiKey(account.UserId, account.ExchangeId);
            if (apiKey is not null)
            {
                var credentialKey = new ApiCredentials(apiKey.PublicKey, apiKey.PrivateKey);
                _binanceClient.SetApiCredentials(credentialKey);
                var accountInfo = await _binanceClient.UsdFuturesApi.Account.GetAccountInfoAsync();
                // var exchangeInfo = await _binanceClient.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync();
                // foreach (var symbol in exchangeInfo.Data.Symbols)
                // {
                //     if (symbol.ContractType == Binance.Net.Enums.ContractType.Perpetual)
                //     {
                //         await _symbolRepo.AddEntityAsync(new Symbol(symbol.Pair, symbol.QuantityPrecision, symbol.PricePrecision, symbol.BaseAsset, symbol.MarginAsset ,1));
                //     }
                // }
                if (accountInfo.Success)
                {
                    var assets = accountInfo.Data.Assets;
                    foreach (var asset in assets)
                    {
                        if (asset.AvailableBalance > decimal.Zero)
                        {
                            account.AddAset(new AccountAsset(asset.Asset, asset.MarginBalance, asset.WalletBalance, account.Id));
                        }
                    }
                    result.SetResult(account);
                    result.SetSuccess();
                }
            }
            return result;
        }

        public async Task<BaseResponse<List<Prompt>>> GetAccountByUserId(Guid userId)
        {
            var result = new BaseResponse<List<Prompt>>();
            List<Prompt> prompts = new();
            // var exchanges = await _accountRepository.GetEntityColumnsByParameterAsync<string>(new QueryParameter().AddEqualCondition("user_id", userId), [nameof(Exchange.Name)]);
            // if (exchanges.Any())
            // {
            //     prompts.Add(Prompt.CreateSelectOptionPrompt(nameof(Exchange).ToLower(), "Pick exchange", Prompt.Option, true));
            //     foreach (var exchange in exchanges)
            //     {
            //         prompts.Add(Prompt.CreateSelectOptionPrompt(nameof(Exchange).ToLower(), exchange, Prompt.Option));
            //     }
            //     result.SetSuccess();
            //     result.SetResult(prompts);
            //     return result;
            // }
            result.SetError("No exchanges were found for this account");
            return result;
        }

        public async Task<BaseResponse<List<Prompt>>> GetAccountByAccount(Guid userId)
        {
            var result = new BaseResponse<List<Prompt>>();
            var prompts = new List<Prompt>();
            return result;
        }

        public Task<BaseResponse<Exchange>> GetExchanges()
        {
            throw new NotImplementedException();
        }

        public async Task<Exchange> GetExchange(int exchangeId)
        {
            if (_memoryCache.TryGetValue("exchanges", out IEnumerable<Exchange>? exchanges))
            {
                return exchanges.Where(x => x.Id == exchangeId).First();
            }
            else
            {
                var exchangeParameter = new QueryParameter().AddEqualCondition(nameof(Exchange.Id), exchangeId);
                exchanges = await _exchangeRepository.GetEntitiesByParameterAsync(exchangeParameter);
                _memoryCache.Set("exchanges", exchanges);
                return exchanges.Where(x => x.Id == exchangeId).First();
            }

        }
        private async Task<Exchange> GetExchanges(int exchangeId)
        {
            if (_memoryCache.TryGetValue("exchanges", out IEnumerable<Exchange>? exchanges))
            {
                return exchanges.Where(x => x.Id == exchangeId).First();
            }
            else
            {
                var exchangeParameter = new QueryParameter().AddEqualCondition(nameof(Exchange.Id), exchangeId);
                exchanges = await _exchangeRepository.GetEntitiesByParameterAsync(exchangeParameter);
                _memoryCache.Set("exchanges", exchanges);
                return exchanges.Where(x => x.Id == exchangeId).First();
            }

        }

        Task<BaseResponse<IEnumerable<Exchange>>> IAccountService.GetExchanges()
        {
            throw new NotImplementedException();
        }
    }
}
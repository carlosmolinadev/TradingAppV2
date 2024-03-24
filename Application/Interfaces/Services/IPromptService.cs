using TradingAppMvc.Application.Models.Pages;
using TradingAppMvc.Application.Models.Responses;
using TradingAppMvc.Application.Models.Shared;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Application.Interfaces.Services{
    public interface IPromptService
    {
       Task<BaseResponse<FuturesPage>> GetFuturesPageData(Guid userId, FuturesPage model);
       Task<BaseResponse<IEnumerable<SelectOptionPrompt>>> GetBalanceOptions(Guid userId, int exchangeId);
       Task<List<Prompt>> GetTradeSettingPrompts(Guid userId);
    } 
}
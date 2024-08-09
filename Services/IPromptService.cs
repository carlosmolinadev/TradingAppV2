using Microsoft.AspNetCore.Mvc.Rendering;
using TradingAppMvc.Models.Responses;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Services
{
   public interface IPromptService
   {
      Task<ApplicationResponse<CreateTradeOptionList>> GetCreateTradeOptionLists(Guid userId, string currency = "USDT");
      Task<ApplicationResponse<IEnumerable<SelectListItem>>> GetBalanceOptions(Guid userId, int exchangeId);
      Task<ApplicationResponse<List<TradeSetting>>> GetTradingSettings(Guid userId);
   }
}
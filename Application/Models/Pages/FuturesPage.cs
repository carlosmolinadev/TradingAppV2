using TradingAppMvc.Application.Models.Responses;
using TradingAppMvc.Application.Models.Shared;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Application.Models.Pages
{
    public class FuturesPage
    {
       public List<SelectOptionPrompt> ExchangeOptions {get; set; } = [];
       public List<SelectOptionPrompt> SymbolOptions {get; set; } = [];
       public List<SelectOptionPrompt> BalanceOptions {get; set; } = [];
       public List<SelectOptionPrompt> OrderTypeOptions {get; set; } = [];
       public decimal? Price {get; set;} 
       public decimal Amount {get; set;}
       public TradeSetting? TradeSetting  {get; set; } 
    }
}


using Microsoft.AspNetCore.Mvc.Rendering;

namespace TradingAppMvc.Models.Responses
{
   public class CreateTradeOptionList
   {
      public List<SelectListItem> Exchanges { get; set; } = [];
      public List<SelectListItem> Symbols { get; set; } = [];
      public List<SelectListItem> OrderTypes { get; set; } = [];
   }
}

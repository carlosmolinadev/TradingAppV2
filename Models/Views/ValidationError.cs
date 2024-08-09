namespace TradingAppMvc.Models.Views
{
   public record ValidationError(string Key, IEnumerable<string> Errors);
}
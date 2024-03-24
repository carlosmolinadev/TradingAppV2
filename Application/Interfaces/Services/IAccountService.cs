using TradingAppMvc.Application.Models.Responses;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<BaseResponse<Account>> GetFuturesBalance(Account account);
        Task<BaseResponse<IEnumerable<Exchange>>> GetExchanges();
    }
}
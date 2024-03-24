using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Domain.Repositories
{
    public interface IExchangeRepository : IReadRepository<Exchange>
    {
        public Task<IEnumerable<string>> ExchangesByAccount(Guid accountId);
    }
}
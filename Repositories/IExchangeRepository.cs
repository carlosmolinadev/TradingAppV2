using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Repositories
{
    public interface IExchangeRepository : IReadRepository<Exchange>
    {
        public Task<IEnumerable<Exchange>> GetAvailableExchangesByUser(Guid accountId);
    }
}
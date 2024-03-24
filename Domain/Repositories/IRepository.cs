using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IEntity
    {
    }
}
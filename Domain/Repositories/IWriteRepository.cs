using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Repositories
{
    public interface IWriteRepository<T> where T : IEntity
    {
        Task<T> AddEntityAsync(T entity);
        Task<bool> UpdateEntityAsync(T entity);
        Task<bool> DeleteEntityAsync(T entity);
    }
}
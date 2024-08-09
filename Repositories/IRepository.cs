

using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Repositories
{
   public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IEntity
   {
   }
}
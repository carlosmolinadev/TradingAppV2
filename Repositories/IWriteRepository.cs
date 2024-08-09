

using System.Collections;
using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Repositories
{
   public interface IWriteRepository<T> where T : IEntity
   {
      Task<T> AddEntity(T entity);
      Task<bool> UpdateEntity(T entity);
      Task<bool> UpdateEntity(T entity, string[] columns);
      Task<bool> DeleteEntity(T entity);
   }
}

using System.Collections;
using TradingAppMvc.Repositories;
using TradingAppMvc.Application.Models.Repository;

namespace TradingAppMvc.Repositories
{
   public interface IReadRepository<T> where T : class
   {
      public Task<T> FindById<Id>(Id id);
      public Task<IEnumerable<T>> FindAll();
      public Task<IEnumerable<T>> FindByParameter(QueryParameter queryParameter);
      public Task<IEnumerable<U>> FindByParameter<U>(QueryParameter queryParameter, IEnumerable<string> columns);
   }
}

using System.Collections;
using TradingAppMvc.Domain.Interfaces;
using TradingAppMvc.Infraestructure.Repositories.Models;

namespace TradingAppMvc.Domain.Repositories
{
    public interface IReadRepository<T> where T : IEntity
    {
        public Task<T> GetEntityByIdAsync<Id>(Id id);
        public Task<IEnumerable<T>> GetAllEntitiesAsync();
        public Task<IEnumerable<T>> GetEntitiesByParameterAsync(QueryParameter queryParameter);
        public Task<IEnumerable<U>> GetEntityPropertiesByParameterAsync<U>(QueryParameter queryParameter, IEnumerable<string> properties);
    }
}
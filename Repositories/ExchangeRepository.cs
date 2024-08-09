using System.Data;
using Dapper;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Repositories;

namespace TradingAppMvc.Repositories
{
    public class ExchangeRepository : Repository<Exchange>, IExchangeRepository
    {
        private readonly IDbConnection _sqlCon;
        public ExchangeRepository(IDbConnection sqlCon) : base(sqlCon)
        {
            _sqlCon = sqlCon;
        }

        public async Task<IEnumerable<Exchange>> GetAvailableExchangesByUser(Guid userId)
        {
            try
            {
                var sql = @"select distinct e.id, e.name 
                            from exchange e
                            join api_key api on e.id = api.exchange_id
                            where api.user_id = @userId";

                return await _sqlCon.QueryAsync<Exchange>(sql, new { userId });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

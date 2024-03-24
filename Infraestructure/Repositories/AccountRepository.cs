using System.Data;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Domain.Repositories;

namespace TradingAppMvc.Infraestructure.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(IDbConnection sqlCon) : base(sqlCon)
        {
        }
    }
}

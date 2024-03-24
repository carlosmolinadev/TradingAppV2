using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Entities
{
    public class Exchange : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
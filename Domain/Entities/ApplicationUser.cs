using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Entities;

public class ApplicationUser: IEntity
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
}
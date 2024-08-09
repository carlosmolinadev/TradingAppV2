

namespace TradingAppMvc.Domain.Entities;

public class ApplicationUser {
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
}
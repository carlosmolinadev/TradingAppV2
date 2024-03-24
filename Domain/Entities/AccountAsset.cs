using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Entities
{
    public class AccountAsset : IEntity
    {
        public AccountAsset(string currency, decimal availableBalance, decimal walletBalance, Guid accountId)
        {
            Currency = currency;
            AvailableBalance = availableBalance;
            AccountId = accountId;
            WalletBalance = walletBalance;
        }

        public string Currency { get; private set; }
        public decimal AvailableBalance { get; private set; }
        public decimal WalletBalance { get; private set; }
        public Guid AccountId { get; private set; }
    }
}
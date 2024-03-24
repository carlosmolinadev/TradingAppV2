using TradingAppMvc.Domain.Interfaces;

namespace TradingAppMvc.Domain.Entities
{
    public class Account : IEntity
    {

        public Guid Id { get; private set; }
        public int RiskPerTrade { get; private set; }
        public int ExchangeId { get; private set; }
        public Guid UserId { get; private set; }
        private List<AccountAsset> _assets = new();
        private Account(){}
        public Account(Guid id, int riskPerTrade, int exchangeId, Guid userId)
        {
            Id = id;
            ExchangeId = exchangeId;
            RiskPerTrade = riskPerTrade;
            UserId = userId;
        }
        public void AddAset(AccountAsset asset){
            _assets.Add(asset);
        }

        public IReadOnlyList<AccountAsset>GetAssets(){
            return _assets;
        }
    }
}
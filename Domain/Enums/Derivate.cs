namespace TradingAppMvc.Domain.Enums{

    public static class Derivate
    {
        public static bool IsValid(string value)
        {
            return value == Spot || value == Futures || value == Futures;
        }
        public static List<string> GetDerivates(){
            return new List<string>
            {
                Spot,
                Futures,
                Coin
            };
        }
        public static readonly string Spot = "SPOT";
        public static readonly string Futures = "FUTURES";
        public static readonly string Coin = "COIN";
    }
}
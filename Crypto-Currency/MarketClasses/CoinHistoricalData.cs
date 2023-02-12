using System.Collections.Generic;

namespace Crypto_Currency.MarketClasses
{
    public class CoinHistoricalData
    {
        public float HighPrice { get; set; }
        public float LowPrice { get; set; }
        public List<Coin> CoinInfo { get; set; }
    }
}

using System.Collections.Generic;

namespace Crypto_Currency.MarketClasses
{
    public interface IMarkets
    {
        List<Coin> GetCoinsList();
        List<CoinHistoricalData> GetCoinHistoricalData(string CoinName);
        List<CoinMarketData> GetCoinMarkets(string CoinName);
    }
}

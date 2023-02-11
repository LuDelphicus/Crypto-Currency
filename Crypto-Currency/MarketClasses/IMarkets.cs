using System.Collections.Generic;

namespace Crypto_Currency.MarketClasses
{
    public interface IMarkets
    {
        List<CoinsList> GetCoinsList();
        dynamic GetCoinChanges(string CoinName);
    }
}

using System.Collections.Generic;

namespace Crypto_Currency.MarketClasses
{
    public interface IMarkets
    {
        List<Coin> GetCoinsList();
        List<CoinChanges> GetCoinChanges(string CoinName);
    }
}

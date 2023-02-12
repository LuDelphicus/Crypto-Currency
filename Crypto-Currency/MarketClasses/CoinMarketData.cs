using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Currency.MarketClasses
{
    public class CoinMarketData
    {
        public string ExchangeId { get; set; }
        public string Symbol { get; set; }
        public string CoinName { get; set; }
        public string Price { get; set; }
        public string MarketUri { get; set; }
    }
}

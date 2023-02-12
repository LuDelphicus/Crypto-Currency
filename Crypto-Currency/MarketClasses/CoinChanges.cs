using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Crypto_Currency.MarketClasses
{
    public class CoinChanges
    {
        public float HighPrice { get; set; }
        public float LowPrice { get; set; }
        public List<Coin> CoinInfo { get; set; }
    }
}

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Crypto_Currency.MarketClasses
{
    public class Coin
    {
        public int Id { get; set; }
        public string CoinId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public BitmapImage ImagePath { get; set; }
        public string Explorer { get; set; }
        public string Price { get; set; }
        public string Changin24h { get; set; }
        public string Chaning24hColor { get; set; }
    }
}

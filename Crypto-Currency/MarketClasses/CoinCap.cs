using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Crypto_Currency.MarketClasses
{
    public class CoinCap : IMarkets
    {
        private string coinUri = "https://api.coincap.io/v2/assets/?limit=10";
        public static HttpClientHandler httpClientHandler = new HttpClientHandler();
        private static HttpClient httpClient = new HttpClient();
        public List<CoinsList> GetCoinsList()
        {
            var response = httpClient.GetAsync(coinUri).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
            dynamic Data = responseData["data"];

            int id = 1;
            List<CoinsList> list = new List<CoinsList>();
            
            foreach (var element in Data)
            {
                string img_Uri = $"https://assets.coincap.io/assets/icons/{((string)element.symbol).ToLower()}@2x.png";

                float price = (float)Math.Round((double)element.priceUsd, 2);

                float changing24H = (float)Math.Round((double)element.changePercent24Hr, 2);
                string changing24hColor;

                if (changing24H > 0)
                {
                    changing24hColor = "#008A45";
                }
                else
                {
                    changing24hColor = "#CC1010";
                }

                list.Add(new CoinsList()
                {
                    Id = id,
                    Name = $"{(string)element.name}",
                    Symbol = (string)element.symbol,
                    ImagePath = new BitmapImage(new Uri(img_Uri)),
                    Price = $"${price.ToString()}",
                    Changin24h = $"{changing24H.ToString()}%",
                    Chaning24hColor = changing24hColor
                });

                id++;
            }

            return list;
        }
    }
}

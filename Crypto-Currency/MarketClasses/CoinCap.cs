using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Markup;

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
            dynamic data = responseData["data"];

            var list = CreateCoinsList(data);
            return list;
        }

        private List<CoinsList> CreateCoinsList(dynamic data)
        {
            int id = 1;
            List<CoinsList> resultList = new List<CoinsList>();

            foreach (var element in data)
            {
                string imgUri = $"https://assets.coincap.io/assets/icons/{((string)element.symbol).ToLower()}@2x.png";
                float price = (float)Math.Round((double)element.priceUsd, 2);
                float changing24H = (float)Math.Round((double)element.changePercent24Hr, 2);
                string changing24hColor = (changing24H > 0) ? "#008A45" : "#CC1010";

                resultList.Add(new CoinsList()
                {
                    Id = id,
                    Name = (string)element.name,
                    Symbol = (string)element.symbol,
                    ImagePath = new BitmapImage(new Uri(imgUri)),
                    Price = $"${price.ToString()}",
                    Changin24h = $"{changing24H.ToString()}%",
                    Chaning24hColor = changing24hColor
                });

                id++;
            }
            return resultList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

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
            
            List<CoinsList> list = new List<CoinsList>();
            int id = 1;
            foreach (var element in Data)
            {
                string img_Uri = $"https://assets.coincap.io/assets/icons/{((string)element.symbol).ToLower()}@2x.png";

                list.Add(new CoinsList()
                {
                    Id = id,
                    Name = $"{id}. {(string)element.name}",
                    ImagePath = new BitmapImage(new Uri(img_Uri))
                });

                id++;
            }

            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace Crypto_Currency.MarketClasses
{
    public class CoinCap : IMarkets
    {
        private string coinUri = "https://api.coincap.io/v2/assets/?limit=10";

        public static HttpClientHandler httpClientHandler = new HttpClientHandler();
        private static HttpClient httpClient = new HttpClient();

        public List<CoinsList> GetCoinsList() // Get list of top 10 coins
        {
            var response = httpClient.GetAsync(coinUri).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
            dynamic data = responseData["data"];

            var list = CreateCoinsList(data);
            return list;
        }

        private List<CoinsList> CreateCoinsList(dynamic data) // Create list of top 10 coins
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

        public dynamic GetCoinChanges(string coinName) // Get info about coin and changes of price
        {
            string coinChangesUri = $"https://api.coincap.io/v2/assets/{coinName.ToLower()}/history?interval=d1";
            var response = httpClient.GetAsync(coinChangesUri).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
            dynamic data = responseData["data"];

            var coinChanges = CreateCoinChanges(data);
            coinChanges.CoinInfo = GetCoinInfo(coinName);

            return coinChanges;
        }

        private dynamic GetCoinInfo(string coinName) // Get info about coin ( price, name, symbol, image )
        {
            string coinChangesUri = $"https://api.coincap.io/v2/assets/{coinName.ToLower()}";
            var response = httpClient.GetAsync(coinChangesUri).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
            dynamic respondData = responseData["data"];

            var data = CreateCoinData(respondData);

            return data;
        }

        private List<CoinsList> CreateCoinData(dynamic data) // Create data about coin ( price, name, symbol, image )
        {
            List<CoinsList> resultList = new List<CoinsList>();

           
            string imgUri = $"https://assets.coincap.io/assets/icons/{((string)data.symbol).ToLower()}@2x.png";
            float price = (float)Math.Round((double)data.priceUsd, 2);
            float changing24H = (float)Math.Round((double)data.changePercent24Hr, 2);
            string changing24hColor = (changing24H > 0) ? "#008A45" : "#CC1010";

            resultList.Add(new CoinsList()
            {
                Id = 1,
                Name = (string)data.name,
                Symbol = (string)data.symbol,
                ImagePath = new BitmapImage(new Uri(imgUri)),
                Price = $"${price.ToString()}",
                Changin24h = $"{changing24H.ToString()}%",
                Chaning24hColor = changing24hColor
            });

            return resultList;
        }

        private CoinChanges CreateCoinChanges(dynamic data) // Create changing list of coin price 
        {
            CoinChanges coinChanges = new CoinChanges();
            float priceUsd;

            foreach (var element in data)
            {
                priceUsd = (float)element.priceUsd;

                if (coinChanges.HighPrice < priceUsd) {
                    coinChanges.HighPrice = priceUsd;
                    coinChanges.LowPrice = priceUsd;
                }

                if (coinChanges.LowPrice > priceUsd)
                {
                    coinChanges.LowPrice = priceUsd;
                }
            }

            return coinChanges;
        }
    }
}

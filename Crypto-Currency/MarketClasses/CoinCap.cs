using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace Crypto_Currency.MarketClasses
{
    public class CoinCap : IMarkets
    {
        public int id = 1;

        public static HttpClientHandler httpClientHandler = new HttpClientHandler();
        private static HttpClient httpClient = new HttpClient();

        public List<CoinsList> GetCoinsList() // Get list of top 10 coins
        {
            string coinUri = "https://api.coincap.io/v2/assets/?limit=10";
            dynamic data = GetUriRespond(coinUri);
            var list = GetCoinsInfo(data); // Create list of top 10 coins

            return list;
        }

        public dynamic GetCoinChanges(string coinName) // Get info about coin and changes of price
        {
            string coinChangesUri = $"https://api.coincap.io/v2/assets/{coinName.ToLower()}/history?interval=d1";
            string coinUri = $"https://api.coincap.io/v2/assets/{coinName.ToLower()}";

            dynamic changesRespond = GetUriRespond(coinChangesUri);
            dynamic coinRespond = GetUriRespond(coinUri);

            var coinChanges = CreateCoinChanges(changesRespond);
            coinChanges.CoinInfo = GetCoinsInfo(coinRespond);

            return coinChanges;
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

        private dynamic GetUriRespond(string Uri) // Return respond from URI
        {
            var response = httpClient.GetAsync(Uri).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
            dynamic data = responseData["data"];

            return data;
        }

        private List<CoinsList> GetCoinsInfo(dynamic data) // Return detail of coin(s) ( price, name, symbol and etc )
        {
            List<CoinsList> resultList = new List<CoinsList>();

            if (data.Count > 1)
            {
                foreach (var element in data)
                {
                    CreateCoinsInfo(element, resultList);
                }

                return resultList;
            }

            CreateCoinsInfo(data, resultList);
            return resultList;
        }

        private List<CoinsList> CreateCoinsInfo(dynamic data, List<CoinsList> resultList)
        {
            string imgUri = $"https://assets.coincap.io/assets/icons/{((string)data.symbol).ToLower()}@2x.png";
            float price = (float)Math.Round((double)data.priceUsd, 2);
            float changing24H = (float)Math.Round((double)data.changePercent24Hr, 2);
            string changing24hColor = (changing24H > 0) ? "#008A45" : "#CC1010";

            resultList.Add(new CoinsList()
            {
                Id = id,
                Name = (string)data.name,
                Symbol = (string)data.symbol,
                ImagePath = new BitmapImage(new Uri(imgUri)),
                Explorer = (string)data.explorer,
                Price = $"${price.ToString()}",
                Changin24h = $"{changing24H.ToString()}%",
                Chaning24hColor = changing24hColor
            });
            id++;

            return resultList;
        }
    }
}

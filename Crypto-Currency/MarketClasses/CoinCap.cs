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

        public List<Coin> GetCoinsList() // Get list of top 10 coins
        {
            string coinUri = "https://api.coincap.io/v2/assets/?limit=10";
            var data = _GetUriRespond(coinUri);
            var list = _GetCoinInfo(data); // Create list of top 10 coins

            return list;
        }

        public List<CoinHistoricalData> GetCoinHistoricalData(string coinName) // Get info about coin and changes of price
        {
            string coinChangesUri = $"https://api.coincap.io/v2/assets/{coinName.ToLower()}/history?interval=d1";
            string coinUri = $"https://api.coincap.io/v2/assets/{coinName.ToLower()}";

            var changesRespond = _GetUriRespond(coinChangesUri);
            var coinRespond = _GetUriRespond(coinUri);

            var dataChanges = _CreateCoinHistoricalData(changesRespond);
            List<CoinHistoricalData> resultList = new List<CoinHistoricalData>();
            resultList.Add(new CoinHistoricalData()
            {
                HighPrice = dataChanges.HighPrice,
                LowPrice = dataChanges.LowPrice,
                CoinInfo = _GetCoinInfo(coinRespond)
            });

            return resultList;
        }

        private CoinHistoricalData _CreateCoinHistoricalData(dynamic data) // Create changing list of coin price 
        {
            CoinHistoricalData coinChanges = new CoinHistoricalData();

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

        private dynamic _GetUriRespond(string Uri) // Return respond from URI
        {
            var response = httpClient.GetAsync(Uri).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
            var data = responseData["data"];

            return data;
        }

        private List<Coin> _GetCoinInfo(dynamic data) // Return detail of coin(s) ( price, name, symbol and etc )
        {
            List<Coin> resultList = new List<Coin>();

            if (data.Count > 1)
            {
                foreach (var element in data)
                {
                    _CreateCoinInfo(element, resultList);
                }

                return resultList;
            }

            _CreateCoinInfo(data, resultList);
            return resultList;
        }

        private List<Coin> _CreateCoinInfo(dynamic data, List<Coin> resultList)
        {
            string imgUri = $"https://assets.coincap.io/assets/icons/{((string)data.symbol).ToLower()}@2x.png";
            float price = (float)Math.Round((double)data.priceUsd, 2);
            float changing24H = (float)Math.Round((double)data.changePercent24Hr, 2);
            string changing24hColor = (changing24H > 0) ? "#008A45" : "#CC1010";

            resultList.Add(new Coin()
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

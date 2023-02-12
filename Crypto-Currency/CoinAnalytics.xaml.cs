using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Crypto_Currency.MarketClasses;

namespace Crypto_Currency
{
    public partial class CoinAnalytics : Page
    {
        public CoinAnalytics(string coinName)
        {
            IMarkets coincap = new CoinCap();
            var list = coincap.GetCoinHistoricalData(coinName); // Load CoinChanges
            DataContext = list;

            InitializeComponent();

            var coinMarkets = coincap.GetCoinMarkets(coinName); // Markets where u can buy coin
            buysell_button.DataContext = coinMarkets;
        }

        private void BackPage(object sender, MouseButtonEventArgs e)
        {
            CoinsPage coinsPage = new CoinsPage();
            NavigationService.Navigate(coinsPage);
        }

        private void BuySell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BuySell buysellPage = new BuySell((List<CoinMarketData>)buysell_button.DataContext);
            NavigationService.Navigate(buysellPage);
        }
    }
}

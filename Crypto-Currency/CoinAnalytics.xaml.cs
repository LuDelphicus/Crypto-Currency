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
            //buysell_button.DataContext = list.CoinInfo[0].Explorer;
        }

        private void BackPage(object sender, MouseButtonEventArgs e)
        {
            CoinsPage coinsPage = new CoinsPage();
            NavigationService.Navigate(coinsPage);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BuySell buysell = new BuySell();
            NavigationService.Navigate(buysell);

            //string data = (string)buysell_button.DataContext;
            //System.Diagnostics.Process.Start(data);
        }
    }
}

using Crypto_Currency.MarketClasses;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace Crypto_Currency
{
    public partial class BuySell : Page
    {
        public BuySell(List<CoinMarketData> marketsList)
        {
            InitializeComponent();
            listbox1.ItemsSource = marketsList;
        }

        private void Market_OpenUri(object sender, MouseButtonEventArgs e)
        {
            ListView ListMarkets = (ListView)sender;
            var row = (CoinMarketData)ListMarkets.SelectedItem; // Data of selected row
            System.Diagnostics.Process.Start(row.MarketUri);
        }

        private void BackPage(object sender, MouseButtonEventArgs e)
        {
            var ItemSource = (List<CoinMarketData>)listbox1.ItemsSource;
            CoinAnalytics coinAnaltics = new CoinAnalytics(ItemSource[1].CoinName);
            NavigationService.Navigate(coinAnaltics);
        }
    }
}

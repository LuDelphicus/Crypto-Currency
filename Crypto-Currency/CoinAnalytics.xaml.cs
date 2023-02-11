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
            var list = coincap.GetCoinChanges(coinName); // Load CoinChanges
            
            DataContext = list;
            InitializeComponent();
        }

        private void BackPage(object sender, MouseButtonEventArgs e)
        {
            CoinsPage coinsPage = new CoinsPage();
            NavigationService.Navigate(coinsPage);
        }
    }
}

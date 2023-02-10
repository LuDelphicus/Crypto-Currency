using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Crypto_Currency.MarketClasses;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crypto_Currency
{
    public partial class CoinsPage : Page
    {
        public CoinsPage()
        {
            InitializeComponent();

            IMarkets coincap = new CoinCap();
            var list = coincap.GetCoinsList();
            listbox1.ItemsSource = list;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listbox1.ItemsSource); // Задаю внимание на содержание ListBox'a
            view.Filter = UseFilter;
        }

        private bool UseFilter(object coin)
        {
            bool searchBoxEmpty = String.IsNullOrEmpty(SearchBoxFilter.Text);
            bool searchBoxHasEquals = ((coin as CoinsList).
                Symbol.IndexOf(SearchBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ? true : ((coin as CoinsList).
                Name.IndexOf(SearchBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            if (searchBoxEmpty) return true;
            return searchBoxHasEquals;
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listbox1.ItemsSource).Refresh(); // refresh to default focus
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CoinAnalytics coinAnalytics = new CoinAnalytics();
            NavigationService.Navigate(coinAnalytics);
        }
    }
}

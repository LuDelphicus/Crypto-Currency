using System;
using System.Windows.Controls;
using System.Windows.Data;
using Crypto_Currency.MarketClasses;
using System.Windows.Input;
using System.Windows.Navigation;


namespace Crypto_Currency
{
    public partial class CoinsPage : Page
    {
        public CoinsPage()
        {
            InitializeComponent();

            IMarkets coincap = new CoinCap();
            var list = coincap.GetCoinsList(); // Load CoinsList
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

        private void CoinsPage_SendData(object sender, MouseButtonEventArgs e)
        {
            ListView ListItems = (ListView)sender;
            var selectedItem = ListItems.SelectedItem;
            var data = (CoinsList)selectedItem; // Data of selected row

            CoinAnalytics coinAnalytics = new CoinAnalytics(data);
            NavigationService.Navigate(coinAnalytics);
        }
    }
}

using Crypto_Currency.MarketClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crypto_Currency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            if (String.IsNullOrEmpty(SearchBoxFilter.Text)) {
                return true;
            } 
            else if ((coin as CoinsList).
                Symbol.IndexOf(SearchBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) 
            {
                return true;
            }
            else
            {
                return ((coin as CoinsList).
                Name.IndexOf(SearchBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
         }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listbox1.ItemsSource).Refresh(); // refresh to default focus
        }
    }
}

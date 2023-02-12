using System.Windows;


namespace Crypto_Currency
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new CoinsPage();
        }
    }
}

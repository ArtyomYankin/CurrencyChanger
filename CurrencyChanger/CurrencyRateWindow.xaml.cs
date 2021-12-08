using CurrencyChanger.Data;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CurrencyChanger
{
    /// <summary>
    /// Interaction logic for CurrencyRate.xaml
    /// </summary>
    public partial class CurrencyRate : Window
    {
        public CurrencyRate()
        {
            InitializeComponent();

            //            InitializeComponent();
            List<Currency> currencies = new List<Currency>();
            currencies.Add(new Currency() { CurrencyId = 1, CurrencyName = "Dollar", BuyRate = 2.533, SellRate = 2.542 });
            currencies.Add(new Currency() { CurrencyId = 2, CurrencyName = "Euro", BuyRate = 2.533, SellRate = 2.542 });
            currencies.Add(new Currency() { CurrencyId = 3, CurrencyName = "Zloty", BuyRate = 2.533, SellRate = 2.542 });

            dgSimple.ItemsSource = currencies;
        }

        public void ShowViewModel()
        {
            MessageBox.Show("Currency rate:");
        }
    }
}

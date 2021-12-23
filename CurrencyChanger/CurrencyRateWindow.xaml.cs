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

        private readonly ApplicationDbContext _applicationDbContext;
        public CurrencyRate(ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();

            _applicationDbContext = applicationDbContext;
            GetCurrencies();
        }
        private void GetCurrencies()
        {
            CurrencyDG.ItemsSource = _applicationDbContext.Currencies.ToList();
        }
        public void ShowViewModel()
        {
            MessageBox.Show("Currency rate:");
        }
    }
}

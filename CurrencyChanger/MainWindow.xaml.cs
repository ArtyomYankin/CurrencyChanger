using CurrencyChanger.Data;
using CurrencyChanger.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CurrencyChanger
{

    public partial class MainWindow : Window
    {
        ApplicationDbContext _applicationDbContext;
        private DateTime Date;
        public MainWindow(ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            _applicationDbContext = applicationDbContext;
            Date = DateTime.Now;
        }
        private void AsCustomerButtonClick(object sender, RoutedEventArgs e)
        {
            CustomerLoginWindow customerWindow = new CustomerLoginWindow(_applicationDbContext);
            customerWindow.Show();
        }
        private void AsCashierButtonClick(object sender, RoutedEventArgs e)
        {
            CashierWindow cashierWindow = new CashierWindow(_applicationDbContext);
            cashierWindow.Show();
        }
        private void NextDayClick(object sender, RoutedEventArgs e)
        {
            Date = Date.AddDays(1);
            ChangeCurrencyRates();
        }
        private void ChangeCurrencyRates()
        {
            var currencies = _applicationDbContext.Currencies.ToList();
            var customers = _applicationDbContext.Customers.ToList();
            foreach (var c in currencies)
            {
                Random random = new Random();
                c.SellRate = random.NextDouble()/10 + c.SellRate - random.NextDouble()/10;
                c.BuyRate = random.NextDouble()/10 + c.SellRate - random.NextDouble()/10;
                c.BuyRate = Math.Round(c.BuyRate, 2);
                c.SellRate = Math.Round(c.SellRate, 2);
            }
            foreach (var c in customers)
            {
                c.CurrencyLimit = 0;
            }
            _applicationDbContext.SaveChanges();
        }
    }
}
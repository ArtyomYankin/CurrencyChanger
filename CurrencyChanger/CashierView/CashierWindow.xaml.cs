using CurrencyChanger.Data;
using CurrencyChanger.Data.Model;
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
    /// Interaction logic for CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        ApplicationDbContext _applicationDbContext;
        Currency newCurrency = new Currency();
        public static double currencyLimit = 1000; 

        public CashierWindow(ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            _applicationDbContext = applicationDbContext;
            GetCurrencies();
            AddNewProductGrid.DataContext = newCurrency;
            DataContext = this;
        }

        private void GetCurrencies()
        {
            CurrencyDG.ItemsSource = _applicationDbContext.Currencies.ToList();
           
        }
        private void AddCurrency(object s, RoutedEventArgs e)
        {
            _applicationDbContext.Currencies.Add(newCurrency);
            _applicationDbContext.SaveChanges();
            GetCurrencies();
            newCurrency = new Currency();
            AddNewProductGrid.DataContext = newCurrency;
        }
        private void ChangeLimit(object s, RoutedEventArgs e)
        {
            currencyLimit = double.Parse(NewLimit.Text.ToString());
            NewLimit.Text = String.Empty;
        }
        Currency selectedCurrency = new Currency();
        private void UpdateCurrencyForEdit(object s, RoutedEventArgs e)
        {
            selectedCurrency = (s as FrameworkElement).DataContext as Currency;
            UpdateProductGrid.DataContext = selectedCurrency;
        }

        private void UpdateCurrency(object s, RoutedEventArgs e)
        {
            _applicationDbContext.Update(selectedCurrency);
            _applicationDbContext.SaveChanges();
            GetCurrencies();
        }

        private void DeleteCurrency(object s, RoutedEventArgs e)
        {
            var currencyToBeDeleted = (s as FrameworkElement).DataContext as Currency;
            _applicationDbContext.Currencies.Remove(currencyToBeDeleted);
            _applicationDbContext.SaveChanges();
            GetCurrencies();
        }
        private void BrowseOpHistory(object s, RoutedEventArgs e)
        {
            ReceiptsWindow receiptsWindow = new ReceiptsWindow();
            receiptsWindow.ShowDialog();
        }
    }
}

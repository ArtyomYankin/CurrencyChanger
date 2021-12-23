using CurrencyChanger.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;

namespace CurrencyChanger
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {

        private readonly string filesPath = "C:/Users/Artyom Yankin/Desktop/WPF/CurrencyChanger/Receipts/";
        private readonly ApplicationDbContext _applicationDbContext;
        private Customer _currentCustomer;
        public CustomerWindow(Customer customer, ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            _currentCustomer = customer;
            InitializeComponent();
            _applicationDbContext = applicationDbContext;

            ClearControls();

            BindCurrency();
        }

        #region Bind Currency From and To Combobox
        private void BindCurrency()

        {
            ToCurrency.ItemsSource = _applicationDbContext.Currencies.ToList();
            CurrencyFrom.ItemsSource = _applicationDbContext.Currencies.ToList();
            ToCurrency.DisplayMemberPath = "CurrencyName";
            CurrencyFrom.DisplayMemberPath = "CurrencyName";
            ToCurrency.SelectedValuePath = "SellRate";
            CurrencyFrom.SelectedValuePath = "BuyRate";
            ToCurrency.SelectedIndex = 0;
            CurrencyFrom.SelectedIndex = 0;
        }
        #endregion

        #region Button Click Event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            CurrencyRate currencyRate = new CurrencyRate(_applicationDbContext);
            currencyRate.Show();
        }
        private void GetCurrency_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Succesfully Done");
            string gotenCurrency = ToCurrency.Text;
            double moneyGet = double.Parse(CurrencyToGet.Content.ToString());
            var changeTime = DateTime.Now;
            var fileName =  filesPath + changeTime.Millisecond.ToString().Replace("/", ".") + _currentCustomer.Name;
            using (FileStream fileStream = new FileStream($"{fileName}.txt", FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"Operation: Buy {gotenCurrency} in amount of {moneyGet}");
                    sw.WriteLine($"Rubles inserted: {InsertedRubles.Text}");
                    sw.WriteLine($"Date: {changeTime}");
                    sw.WriteLine($"Customer:{_currentCustomer.Name}");
                } ;
            }
            fileName = fileName + ".txt";
            new Process { StartInfo = new ProcessStartInfo(fileName) { UseShellExecute  = true } }.Start();
            CurrencyToGet.Content = "";
            ClearControls();
        }
        private void GetRubles_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Succesfully Done");
            string gotenCurrency = CurrencyFrom.Text;
            double moneyGet = double.Parse(RublesToGet.Content.ToString());
            var changeTime = DateTime.Now;
            var fileName = filesPath + changeTime.Millisecond.ToString().Replace("/", ".") + _currentCustomer.Name;
            using (FileStream fileStream = new FileStream($"{fileName}.txt", FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"Operation: Buy rubles in amount of {moneyGet}");
                    sw.WriteLine($"Inserted {CurrencyFrom.Text} in amount of {InsertedCurrency.Text}");
                    sw.WriteLine($"Date: {changeTime}");
                    sw.WriteLine($"Customer:{_currentCustomer.Name}");
                };
            }
            fileName = fileName + ".txt";
            new Process { StartInfo = new ProcessStartInfo(fileName) { UseShellExecute = true } }.Start();
            RublesToGet.Content = "";
            ClearControls();
        }
        private void ConvertToCurrency_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;

            if (InsertedRubles.Text == null || InsertedRubles.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                InsertedRubles.Focus();
                return;
            }

            else
            {
                ConvertedValue = double.Parse(InsertedRubles.Text) / double.Parse(ToCurrency.SelectedValue.ToString());

                CurrencyToGet.Content =  ConvertedValue.ToString("N3");
            }
        }
        private void ConvertFromCurrency_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;

            if (InsertedCurrency.Text == null || InsertedCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                InsertedRubles.Focus();
                return;
            }

            else
            {
                ConvertedValue = double.Parse(InsertedCurrency.Text) * double.Parse(CurrencyFrom.SelectedValue.ToString());

                RublesToGet.Content = ConvertedValue.ToString("N3");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }
        #endregion

        #region Extra Events

        private void ClearControls()
        {
            InsertedRubles.Text = string.Empty;
            InsertedCurrency.Text = string.Empty;
            if (ToCurrency.Items.Count > 0)
                ToCurrency.SelectedIndex = 0;
            if (CurrencyFrom.Items.Count > 0)
                CurrencyFrom.SelectedIndex = 0;
            InsertedRubles.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}

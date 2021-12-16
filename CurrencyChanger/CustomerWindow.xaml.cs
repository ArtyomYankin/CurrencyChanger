using CurrencyChanger.Data;
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
using System.Windows.Shapes;

namespace CurrencyChanger
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public Customer Customer { get; set; }
        public CustomerWindow(Customer customer, ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            Customer = null;
            this.DataContext = Customer;
            InitializeComponent();
            _applicationDbContext = applicationDbContext;

            ClearControls();

            BindCurrency();
        }

        #region Bind Currency From and To Combobox
        private void BindCurrency()

        {
            cmbToCurrency.ItemsSource = _applicationDbContext.Currencies.ToList();
            cmbToCurrency.DisplayMemberPath = "CurrencyName";
            cmbToCurrency.SelectedValuePath = "BuyRate";
            cmbToCurrency.SelectedIndex = 0;
        }
        #endregion

        #region Button Click Event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrencyRate taskWindow = new CurrencyRate();
            taskWindow.Show();
        }
        private void GetMoney_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Succesfully Done");
            string gotenCurrency = cmbToCurrency.Text;
            double moneyGet = double.Parse(lblCurrency.Content.ToString());
            var changeTime = DateTime.UtcNow;
            lblCurrency.Content = "";
            ClearControls();
        }
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;

            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }

            if (txtCurrency.Text == cmbToCurrency.Text)
            {
                ConvertedValue = double.Parse(txtCurrency.Text);

                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                ConvertedValue = (double.Parse(txtCurrency.Text)) / double.Parse(cmbToCurrency.SelectedValue.ToString());

                lblCurrency.Content =  ConvertedValue.ToString("N3");
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
            txtCurrency.Text = string.Empty;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;
            lblCurrency.Content = "";
            txtCurrency.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}

using CurrencyChanger.Data;
using CurrencyChanger.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    //    /// <summary>
    //    /// Interaction logic for MainWindow.xaml
    //    /// </summary>
    //    public partial class MainWindow : Window
    //    {
    //       // ApplicationDbContext db;
    //        public MainWindow()
    //        {
    //            InitializeComponent();
    //            List<Currency> currencies = new List<Currency>();
    //            currencies.Add(new Currency() { CurrencyId = 1, CurrencyName = "Dollar", BuyRate = 2.533, SellRate = 2.542 });
    //            currencies.Add(new Currency() { CurrencyId = 2, CurrencyName = "Euro", BuyRate = 2.533, SellRate = 2.542 });
    //            currencies.Add(new Currency() { CurrencyId = 3, CurrencyName = "Zloty", BuyRate = 2.533, SellRate = 2.542 });

    //            dgSimple.ItemsSource = currencies;
    //            //db = new ApplicationDbContext();
    //            //db.Customers.Load();
    //            //this.DataContext = db.Customers.Local.ToBindingList();
    //        }
    //        // добавление
    //        private void Add_Click(object sender, RoutedEventArgs e)
    //        {
    //            //CustomerWindow customerWindow = new CustomerWindow(new Customer());
    //            //if (customerWindow.ShowDialog() == true)
    //            //{
    //            //    Customer customer= customerWindow.Customer;
    //            //    db.Customers.Add(customer);
    //            //    db.SaveChanges();
    //            //}
    //        }

    //        private void Button_Click(object sender, RoutedEventArgs e)
    //        {

    //        }
    //        // редактирование
    //        //private void Edit_Click(object sender, RoutedEventArgs e)
    //        //{
    //        //    // если ни одного объекта не выделено, выходим
    //        //    if (phonesList.SelectedItem == null) return;
    //        //    // получаем выделенный объект
    //        //    Customer customer= phonesList.SelectedItem as Customer;

    //        //    CustomerWindow phoneWindow = new CustomerWindow(new Customer
    //        //    {
    //        //        Id = customer.Id,
    //        //        Name = customer.Name,
    //        //        CurrencyLimit = customer.CurrencyLimit,
    //        //    });

    //        //    if (phoneWindow.ShowDialog() == true)
    //        //    {
    //        //        // получаем измененный объект
    //        //        customer = db.Customers.Find(phoneWindow.Customer.Id);
    //        //        if (customer != null)
    //        //        {
    //        //            customer.Name = phoneWindow.Customer.Name;
    //        //            customer.CurrencyLimit = phoneWindow.Customer.CurrencyLimit;
    //        //            db.Entry(customer).State = EntityState.Modified;
    //        //            db.SaveChanges();
    //        //        }
    //        //    }
    //        //}
    //        // удаление
    //        //private void Delete_Click(object sender, RoutedEventArgs e)
    //        //{
    //        //    // если ни одного объекта не выделено, выходим
    //        //    if (phonesList.SelectedItem == null) return;
    //        //    // получаем выделенный объект
    //        //    Customer phone = phonesList.SelectedItem as Customer;
    //        //    db.Customers.Remove(phone);
    //        //    db.SaveChanges();
    //        //}
    //    }
    //}

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ClearControls();

            BindCurrency();
        }

        #region Bind Currency From and To Combobox
        private void BindCurrency()

        {
            //Create a Datatable Object
            DataTable dtCurrency = new DataTable();

            dtCurrency.Columns.Add("Text");

            dtCurrency.Columns.Add("Value");

            dtCurrency.Rows.Add("Choose currency", 0);
            dtCurrency.Rows.Add("USD", 2.54);
            dtCurrency.Rows.Add("EUR", 2.89);
            dtCurrency.Rows.Add("ZLT", 0.5);

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
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
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                cmbToCurrency.Focus();
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

                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
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
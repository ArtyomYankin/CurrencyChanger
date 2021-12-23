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
    /// Interaction logic for CustomerLoginWindow.xaml
    /// </summary>
    public partial class CustomerLoginWindow : Window
    {
        private readonly ApplicationDbContext _applicationDbContext;
        Customer newCustomer = new Customer();
        public CustomerLoginWindow(ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            _applicationDbContext = applicationDbContext;
            LoginCustomerGrid.DataContext = newCustomer;
        }
        private void LoginButton(object s, RoutedEventArgs e)
        {
            var customer = _applicationDbContext.Customers.Where(x => x.Name == newCustomer.Name).FirstOrDefault();
            CustomerWindow customerWindow = new CustomerWindow(newCustomer, _applicationDbContext);
            if (customer != null)
            {
                customerWindow.Show();
                MessageBox.Show("You're in");
                Close();
            }
            else
            {
                _applicationDbContext.Add(newCustomer);
                _applicationDbContext.SaveChanges();
                MessageBox.Show("You are in base");
                customerWindow.Show();
                Close();
            }
        }
    }
}

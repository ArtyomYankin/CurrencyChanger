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
        public MainWindow(ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            _applicationDbContext = applicationDbContext;
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
    }
}
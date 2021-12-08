using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChanger.Data.Repository
{
    interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
    }
}

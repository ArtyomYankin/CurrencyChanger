using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChanger.Data.Repository
{
    class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Customer> _entity;
        public CustomerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _entity = _applicationDbContext.Set<Customer>();
        }
        public void AddCustomer(Customer customer)
        {
            _entity.Add(customer);
            _applicationDbContext.SaveChanges();
        }
    }
}

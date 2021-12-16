using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChanger.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(GetCurrency());
        }

        private Currency[] GetCurrency()
        {
            return new Currency[]
            {
                new Currency{ CurrencyId = 1, CurrencyName = "Dollar", BuyRate = 2.45, SellRate = 2.38},
                new Currency{ CurrencyId = 2, CurrencyName = "Euro", BuyRate = 2.78, SellRate = 2.67},
                new Currency{ CurrencyId = 3, CurrencyName = "Zloty", BuyRate = 0.5, SellRate = 0.65}
            };
        }
    }
}

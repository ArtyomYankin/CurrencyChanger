using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChanger.Data
{
    class Receipt
    {
        public int ReceiptId { get; set; }
        public string Customer { get; set; }
        public double Bought { get; set; }
        public double Sold { get; set; }
        public int CustomerId { get; set; }
    }
}

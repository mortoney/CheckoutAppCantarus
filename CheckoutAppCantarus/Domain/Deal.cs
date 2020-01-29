using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutAppCantarus.Domain
{
    public class Deal
    {
        public string DealName { get; set; }
        public int RequiredQuantity { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}

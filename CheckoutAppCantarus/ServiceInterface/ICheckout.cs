using CheckoutAppCantarus.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutAppCantarus.ServiceInterface
{
    public interface ICheckout
    {
        void Scan(Item item);
        decimal Total();
    }
}

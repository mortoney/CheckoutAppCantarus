using CheckoutAppCantarus.Caching;
using CheckoutAppCantarus.Domain;
using CheckoutAppCantarus.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutAppCantarus.Service
{
    public class Checkout : ICheckout
    {
        public void Scan(Item item)
        {
            BasketCache.AddToBasketCahce(item);
        }

        public decimal Total()
        {
            var basketTotal = 0.0M;
            var basketItems = BasketCache.GetBasketContent();

            foreach (KeyValuePair<int, int> item in basketItems)
            {
                var inventoryItem = InventoryCache.GetInventoryItemById(item.Key);
                var itemQuantityInBasket = item.Value;
                if (inventoryItem != null)
                {
                    if (inventoryItem.AvailableDeal != null)
                    {
                        if(inventoryItem.AvailableDeal.RequiredQuantity <= itemQuantityInBasket)
                        {
                            // Find out how many times this deal could be applied.
                            int timesApplyDiscount = itemQuantityInBasket / inventoryItem.AvailableDeal.RequiredQuantity;
                            // How many single items left.
                            int remainingSingleItems = itemQuantityInBasket - (inventoryItem.AvailableDeal.RequiredQuantity * timesApplyDiscount);

                            basketTotal += (timesApplyDiscount * inventoryItem.AvailableDeal.DiscountedPrice) + (remainingSingleItems * inventoryItem.Price);
                        }
                        else
                        {
                            basketTotal += itemQuantityInBasket * inventoryItem.Price;
                        }
                    }
                    else
                    {
                        basketTotal += itemQuantityInBasket * inventoryItem.Price;
                    }
                }
            }
            return basketTotal;
        }

    }
}

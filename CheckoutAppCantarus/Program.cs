using CheckoutAppCantarus.Caching;
using CheckoutAppCantarus.Domain;
using CheckoutAppCantarus.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutAppCantarus
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get data from database and populate inventory cache
            InventoryCache.AddToPricingCahce(FakeData.InventoryDataFakingDatabase());
            var checkoutComplete = false;
            var checkoutService = new Checkout();

            Console.WriteLine("Welcome to Store");
            Console.WriteLine("Available Items:");
            foreach(var item in InventoryCache.GetInventoryContent())
            {
                var inventoryItem = item.Value;
                var dealText = string.Empty;
                if (inventoryItem.AvailableDeal != null)
                {
                    dealText = " OR " + inventoryItem.AvailableDeal.RequiredQuantity + " for £" + inventoryItem.AvailableDeal.DiscountedPrice;
                }
                Console.WriteLine(inventoryItem.Name + " for £" + inventoryItem.Price + dealText);
                Console.WriteLine("################");
            }
            Console.WriteLine("");
            while (!checkoutComplete)
            {
                Console.WriteLine("Enter Item name (letter) which you like to purchase, or type 'ex' to finish");
                var input = Console.ReadLine().ToLower();
                if (input == "ex")
                {
                    checkoutComplete = true;
                    continue;
                }
                var itemFromInventory = InventoryCache.GetInventoryContent().Where(i => i.Value.Name.ToLower() == input).FirstOrDefault();
                if(itemFromInventory.Value != null)
                {
                    checkoutService.Scan(itemFromInventory.Value);
                    Console.WriteLine("Current Total:" + checkoutService.Total());
                }
                else
                {
                    Console.WriteLine("Invalid item, type just one letter.");
                }
            }


            Console.WriteLine("Final Total:" + checkoutService.Total());
            Console.ReadKey();
        }

    }
}

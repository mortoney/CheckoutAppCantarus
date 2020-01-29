using CheckoutAppCantarus.Domain;
using System.Collections.Generic;

namespace CheckoutAppCantarus
{
    public static class FakeData
    {

        public static List<Item> InventoryDataFakingDatabase()
        {
            var itemData = new List<Item>();

            itemData.Add(new Item
            {
                Id = 1,
                Name = "A",
                Price = 50,
                AvailableDeal = new Deal
                {
                    DealName = "3 For only 130",
                    DiscountedPrice = 130.00M,
                    RequiredQuantity = 3
                }
            });

            itemData.Add(new Item
            {
                Id = 2,
                Name = "B",
                Price = 30,
                AvailableDeal = new Deal
                {
                    DealName = "2 For only 45",
                    DiscountedPrice = 45.00M,
                    RequiredQuantity = 2
                }
            });

            itemData.Add(new Item
            {
                Id = 3,
                Name = "C",
                Price = 20
            });

            itemData.Add(new Item
            {
                Id = 4,
                Name = "D",
                Price = 15
            });

            return itemData;
        }
    }
}

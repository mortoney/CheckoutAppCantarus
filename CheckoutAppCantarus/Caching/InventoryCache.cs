using CheckoutAppCantarus.Domain;
using System.Collections.Generic;

namespace CheckoutAppCantarus.Caching
{
    public static class InventoryCache
    {
        // chache containing ItemId and ItemObject
        private static Dictionary<int, Item> _pricing;

        static InventoryCache()
        {
            _pricing = new Dictionary<int, Item>();
        }

        public static void AddToPricingCahce(List<Item> items)
        {
            _pricing.Clear();

            foreach(var item in items)
            {
                if (!_pricing.ContainsKey(item.Id))
                {
                    _pricing.Add(item.Id, item);
                }
                
            }
        }

        public static Item GetInventoryItemById(int itemId)
        {
            if (_pricing.ContainsKey(itemId))
            {
                return _pricing[itemId];
            }
            return null;
        }

        public static Dictionary<int, Item> GetInventoryContent()
        {
            return _pricing;
        }
    }
}

using System.Collections.Generic;

namespace CheckoutAppCantarus.Domain
{
    public static class BasketCache
    {
        // chache containing ItemId and countity
        private static Dictionary<int, int> _basketItems;

        static BasketCache()
        {
            _basketItems = new Dictionary<int, int>();
        }

        public static void AddToBasketCahce(Item item) 
        {
                //If already exists item in basket, increase countity
                if (_basketItems.ContainsKey(item.Id))
                {
                    _basketItems[item.Id] += 1;
                }
                else
                {
                    _basketItems.Add(item.Id, 1);
                }    
        }

        public static void ClearAddToBasketCahce()
        {
            _basketItems.Clear();
        }

        public static Dictionary<int, int> GetBasketContent()
        {
            return _basketItems;
        }
    }
}

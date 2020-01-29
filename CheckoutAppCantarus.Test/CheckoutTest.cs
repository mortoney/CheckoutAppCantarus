using CheckoutAppCantarus.Caching;
using CheckoutAppCantarus.Domain;
using CheckoutAppCantarus.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutAppCantarus.Test
{
    [TestFixture]
    public class CheckoutTest
    {
        private Checkout _checkoutService;


        [SetUp]
        public void SetupFakeData()
        {
            // this data should be hard coded. No connection to any source. This is only to save time.
            InventoryCache.AddToPricingCahce(FakeData.InventoryDataFakingDatabase());
            _checkoutService = new Checkout();
        }

        [Test]
        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void CheckoutSingle_Success(string itemName, decimal expectedResult)
        {
            BasketCache.ClearAddToBasketCahce();
            var itemFromInventory = InventoryCache.GetInventoryContent().Where(i => i.Value.Name.ToLower() == itemName.ToLower()).FirstOrDefault();
            Assert.IsNotNull(itemFromInventory, "Item does not exist in inventory");

            _checkoutService.Scan(itemFromInventory.Value);

            Assert.IsNotNull(BasketCache.GetBasketContent()[itemFromInventory.Value.Id], "Item does not exist in inventory");

            var total = _checkoutService.Total();

            Assert.AreEqual(expectedResult, total);
        }


        [Test]
        [TestCase("A,A,A", 130)]
        [TestCase("B,B", 45)]
        [TestCase("A,A,A,B", 160)]
        [TestCase("A,B,A,B,A,C", 195)]
        [TestCase("B,B,D", 60)]
        [TestCase("A,A,A,A,A,A,A", 310)]
        public void CheckoutSingle_WithDeals_Success(string itemsNames, decimal expectedResult)
        {
            BasketCache.ClearAddToBasketCahce();
            var basketItemsToAdd = itemsNames.Split(',');
            foreach(var itemToAdd in basketItemsToAdd)
            {
                var itemFromInventory = InventoryCache.GetInventoryContent().Where(i => i.Value.Name.ToLower() == itemToAdd.ToLower()).FirstOrDefault();
                Assert.IsNotNull(itemFromInventory, "Item does not exist in inventory");

                _checkoutService.Scan(itemFromInventory.Value);
                Assert.IsNotNull(BasketCache.GetBasketContent()[itemFromInventory.Value.Id], "Item does not exist in inventory");
            }

            var total = _checkoutService.Total();

            Assert.AreEqual(expectedResult, total);
        }
    }
}

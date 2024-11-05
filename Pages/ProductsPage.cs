using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PlaywrightTests
{
    public class ProductsPage
    {
        private readonly IPage _page;
        private List<string> _addedPrductNames;

        public ProductsPage(IPage page)
        {
            _page = page;
            _addedPrductNames = new List<string>();
        }
        private const string FilterDropdownSelector = "[data-test='product-sort-container']";
        private const string ProductNameSelector = "[data-test='inventory-item-name']";
        private const string ProductPriceSelector = "[data-test='inventory-item-price']";

        private const string btn_addToCart = "[data-test='add-to-cart-{0}']";

        private const string btn_shopping_cart = "//div[@id='shopping_cart_container']/a";



        public async Task ProductSorting(SortOption sortOption)
        {
            string optionValue = sortOption switch
            {
                SortOption.NameAToZ => "az",
                SortOption.NameZToA => "za",
                SortOption.PriceLowToHigh => "lohi",
                SortOption.PriceHighToLow => "hilo",
                _ => throw new ArgumentOutOfRangeException(nameof(sortOption), sortOption, null)
            };

            await _page.Locator(FilterDropdownSelector).SelectOptionAsync(optionValue);
            await Task.Delay(2000);

            if (sortOption == SortOption.NameAToZ || sortOption == SortOption.NameZToA)
            {
                List<string> uiProductNames = await GetProductNames();
                var expectedNames = new List<string>(uiProductNames);
                if (sortOption == SortOption.NameAToZ)
                    expectedNames.Sort();
                else
                    expectedNames.Sort((a, b) => string.Compare(b, a));

                Console.WriteLine($"Expected Product Names for {sortOption}: {string.Join(", ", expectedNames)}");


                Console.WriteLine($"Actual Product Names for {sortOption}: {string.Join(", ", uiProductNames)}");
                Assert.That(uiProductNames, Is.EqualTo(expectedNames), $"Product names are not sorted correctly for {sortOption}");
            }
            else if (sortOption == SortOption.PriceHighToLow || sortOption == SortOption.PriceLowToHigh)
            {
                List<decimal> uiProductPrices = await GetProductPrices();
                var expectedPrices = new List<decimal>(uiProductPrices);
                if (sortOption == SortOption.PriceLowToHigh)
                    expectedPrices.Sort();
                else
                    expectedPrices.Sort((a, b) => b.CompareTo(a));

                Console.WriteLine($"Expected Product List for {sortOption}: {string.Join(", ", expectedPrices)}");

                Console.WriteLine($"Actual Product List for {sortOption}: {string.Join(", ", uiProductPrices)}");

                Assert.That(expectedPrices, Is.EqualTo(uiProductPrices), $"Product names are not sorted correctly for {sortOption}");
            }

        }

        private async Task<List<string>> GetProductNames()
        {
            var productElements = await _page.Locator(ProductNameSelector).AllAsync();
            var productNames = new List<string>();

            foreach (var element in productElements)
            {
                productNames.Add(await element.InnerTextAsync());

            }
            return productNames;
        }

        private async Task<List<decimal>> GetProductPrices()
        {
            var priceElements = await _page.Locator(ProductPriceSelector).AllAsync();
            var productPrices = new List<decimal>();

            foreach (var element in priceElements)
            {
                string priceText = await element.InnerTextAsync();
                productPrices.Add(decimal.Parse(priceText.Trim('$')));
            }
            return productPrices;
        }

        public async Task addItemsToCart(string products)
        {
            foreach (var product in products.Split(","))
            {
                var selectorName = product.Trim().ToLower().Replace(" ", "-");
                var addToCartButtonSelector = string.Format(btn_addToCart, selectorName);
                await _page.Locator(addToCartButtonSelector).ClickAsync();
                _addedPrductNames.Add(product.Trim());
            }
        
        }

        public List<string> GetAddedProductNames() => _addedPrductNames;



        public async Task ClickShoppingCartButton()
        {
            await _page.Locator(btn_shopping_cart).ClickAsync();
        }



    }
}

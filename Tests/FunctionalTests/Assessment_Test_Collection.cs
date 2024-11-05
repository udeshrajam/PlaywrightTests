using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests;


namespace PlaywrightTests
{

    public class Assessment_Test_Collection : TestBase
    {


        [TestCase(SortOption.NameZToA)]
        [Description("Verify the sorting order displayed for Z-A on the “All Items” page.")]
        public async Task ValidateProductSortingforZtoA(SortOption sortOption)
        {
            var LoginPage = new LoginPage(_page!);
            await LoginPage.UserLogin();

            var productsPage = new ProductsPage(_page!);
            await productsPage.ProductSorting(sortOption);
        }


        [TestCase(SortOption.PriceHighToLow)]
        [Description("Verify the price order (high-low) displayed on the “All Items” page.")]
        public async Task ValidateProductSortingHightoLow(SortOption sortOption)
        {
            var LoginPage = new LoginPage(_page!);
            await LoginPage.UserLogin();

            var productsPage = new ProductsPage(_page!);
            await productsPage.ProductSorting(sortOption);
        }




        [TestCase("Add multiple items to the card and validate the checkout journey.")]
        public async Task AddMultipleItemsToCart(string productName)
        {
            var LoginPage = new LoginPage(_page!);
            var productsPage = new ProductsPage(_page!);
            var CartPage = new CartPage(_page!);

            await LoginPage.UserLogin();

            await productsPage.addItemsToCart("Sauce Labs Backpack,Sauce Labs Fleece Jacket");
            var addedProducts = productsPage.GetAddedProductNames();
            await productsPage.ClickShoppingCartButton();

            await CartPage.ValidateShoppingCart(addedProducts);
            await CartPage.AddCheckOutInformation();
            await CartPage.CompleteOrder();

        }

    }


}

using Microsoft.Playwright;
using PlaywrightTests.Utilities;


namespace PlaywrightTests
{
    class CartPage
    {
        private readonly IPage _page;
        public CartPage(IPage page) => _page = page;

        private const string ProductNameSelector = "[data-test='inventory-item-name']";
        private const string btn_checkout = "#checkout";

        private const string txt_firstName = "#first-name";
        private const string txt_LastName = "#last-name";
        private const string txt_PostCode = "#postal-code";
        private const string btn_continue = "#continue";
        private const string btn_finish = "#finish";
        private const string lbl_header = "//div[@id='checkout_complete_container']/h2";

        private const string btn_back_to_products = "#back-to-products";

        Random random = new Random(3);


        public async Task ValidateShoppingCart(List<string> addedProducts)
        {
            var nameElements = await _page.Locator(ProductNameSelector).AllAsync();
            var cartProductNames = new List<string>();

            foreach (var element in nameElements)
            {
                string name = await element.InnerTextAsync();
                cartProductNames.Add(name.Trim());
            }
              CollectionAssert.AreEquivalent(addedProducts, cartProductNames, "The items in the cart does not match with added items");
        }


        public async Task AddCheckOutInformation()
        {
            int randomNumber = random.Next();
            await _page.Locator(btn_checkout).ClickAsync();
            await _page.Locator(txt_firstName).FillAsync($"First Name {randomNumber}");
            await _page.Locator(txt_LastName).FillAsync($"Last Name {randomNumber}");
            await _page.Locator(txt_PostCode).FillAsync(randomNumber.ToString());
            await _page.Locator(btn_continue).ClickAsync();
        }


        public async Task CompleteOrder()
        {
            await _page.Locator(btn_finish).ClickAsync();
            string header = await _page.Locator(lbl_header).InnerTextAsync();
            Assert.AreEqual(header, Constants.shopping_success_header, "Header Text not Equal");
            await _page.Locator(btn_back_to_products).ClickAsync();
        }
    }

}
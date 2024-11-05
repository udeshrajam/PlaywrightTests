using Microsoft.Playwright;
using PlaywrightTests.Utilities;


namespace PlaywrightTests
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page) => _page = page;

        private const string txt_username = "#user-name";
        private const string txt_password = "#password";
        private const string btn_loogin = "#login-button";


        public async Task UserLogin()
        {
            await _page.Locator(txt_username).FillAsync(Config.Username);
            Thread.Sleep(5000);
            await _page.Locator(txt_password).FillAsync(Config.Password);
            Thread.Sleep(5000);
            await _page.Locator(btn_loogin).ClickAsync();
            Thread.Sleep(5000);
        }
       
    }

}
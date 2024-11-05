using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests;


namespace PlaywrightTests
{

    public class Accessibility_Test_Collection : TestBase
    {
        [Test]
        [Description("Accessibility Test Execution in Login Page")]
        public void AccessbilityInLoginPage()
        {
            var accessibility = new Accessibility(_page!);
            accessibility?.AnalyzeAccessibilityAsyncTest();
            var login = new LoginPage(_page!);
            login?.UserLogin();

        }

        [Test]
        [Description("Accessibility Test Execution in Product Page")]
        public void AccessbilityInProductsPage()
        {
            var accessibility = new Accessibility(_page!);
            accessibility?.AnalyzeAccessibilityAsync(); 

        }

    }
}
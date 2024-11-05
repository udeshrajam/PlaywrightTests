using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Utilities;
using System.Threading.Tasks;


namespace PlaywrightTests
{

    public class TestBase
    {
        protected IPlaywright? _playwright;
        protected IBrowser? _browser;
        protected IBrowserContext? _context;
        protected IPage? _page;

        protected IBrowserContext? _accessibilityContext;
        protected IPage? _accessibilityPage;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await LaunchBrowser();
            _context = await _browser!.NewContextAsync();
            _page = await _context.NewPageAsync();

            await _page.GotoAsync(Config.BaseUrl);
            await _page.SetViewportSizeAsync(1920, 1080);
            await _page.WaitForLoadStateAsync(LoadState.Load);
        }

        private async Task<IBrowser> LaunchBrowser()
        {
            return Config.browser switch
            {
                BrowserType.Chromium => await _playwright!.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = Config.Headless }),
                BrowserType.Firefox => await _playwright!.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = Config.Headless }),
                BrowserType.Webkit => await _playwright!.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = Config.Headless }),
                _ => throw new ArgumentException("Unsupported browser type")
            };
        }


        [OneTimeTearDown]
        public async Task Teardown()
        {
            await Task.WhenAll(
                _page?.CloseAsync() ?? Task.CompletedTask,
                _context?.CloseAsync() ?? Task.CompletedTask,
                _browser?.CloseAsync() ?? Task.CompletedTask
            );

            _playwright?.Dispose();
        }
    }
}
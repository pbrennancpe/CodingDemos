using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TicketingTest.Playwright
{
    public class PlaywrightFETestBase
    {

        protected IPlaywright Playwright = null!;
        protected IBrowser Browser = null!;
        private IBrowserContext Context;
        protected IPage Page = null!;

        [SetUp]
        public async Task SetUp()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            Context = await Browser.NewContextAsync(new BrowserNewContextOptions { BaseURL = "http://localhost:5212" });
            Page = await Context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright?.Dispose();

        }
    }
}
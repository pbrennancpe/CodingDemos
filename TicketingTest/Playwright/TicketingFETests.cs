using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TicketingTest.Playwright
{
    [TestFixture, Category("Integration")]
    public class TicketingFETests : PlaywrightFETestBase
    {

        [Test]
        public async Task TableHasEntries()
        {
            await Page.GotoAsync("/");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var table = Page.Locator("#TicketTable table tbody tr");



            Assert.That(table.CountAsync(), Is.GreaterThan(0));

        }

        [Test]
        public async Task ClickFirstRowNavigatesToItem1()
        {
            await Page.GotoAsync("/");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var firstRow = Page.Locator("#TicketTable table tbody tr").First;
            var ticketNo = (await firstRow.Locator("td:nth-child(1)").TextContentAsync())!.Trim();

            await firstRow.ClickAsync();

            StringAssert.Contains($"/ticket/{ticketNo}", Page.Url);
        }
        [Test]
        public async Task CreateButtonNavigates()
        {
            await Page.GotoAsync("/");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var button = Page.Locator("#CreateTicket");
            Assert.That(await button.IsVisibleAsync(), Is.True);

            await button.ClickAsync();

            // Wait for navigation to complete
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            StringAssert.Contains("/ticket", Page.Url);
        }

        [Test]
        public async Task UsersDropdownFilterTest()
        {
            //This test requires that the first User in this case "Paul Brennan"
            //Has at least one assigned ticket
            await Page.GotoAsync("/");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var userSelect = Page.Locator("#SelectUser");
            await userSelect.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var options = Page.Locator("div.mud-list-item");

            int optionCount = await options.CountAsync();

            Assert.That(optionCount, Is.GreaterThan(0), "Users Drop down should have at least one user");

                
            var firstUserOption = Page.Locator("div.mud-list-item").First;
            await firstUserOption.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var tableRows = Page.Locator("#TicketTable");

             int rowCount = await tableRows.CountAsync();
            Assert.That(rowCount, Is.GreaterThan(0), "Table should display at least one ticket for selected user.");


        }

    }
}
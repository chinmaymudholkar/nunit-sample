using Microsoft.Playwright;

namespace nunit_sample.Pages;

public class CartPage : BasePage
{
    private ILocator CheckoutButton => _page.Locator("#checkout");

    public CartPage(IPage page) : base(page)
    {
    }

    public async Task CheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}

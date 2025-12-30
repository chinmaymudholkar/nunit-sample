using Microsoft.Playwright;

namespace nunit_sample.Pages;

public class InventoryPage : BasePage
{
    private ILocator BurgerMenuButton => _page.Locator("#react-burger-menu-btn");
    private ILocator LogoutLink => _page.Locator("#logout_sidebar_link");

    public InventoryPage(IPage page) : base(page)
    {
    }

    public async Task LogoutAsync()
    {
        await BurgerMenuButton.ClickAsync();
        await LogoutLink.ClickAsync();
    }

    public async Task AddToCartAsync(string productName)
    {
        // Locator strategy: Find the inventory item by text, then find the add to cart button within it.
        var itemContainer = _page.Locator(".inventory_item", new PageLocatorOptions { HasTextString = productName });
        var addToCartButton = itemContainer.Locator("button");
        await addToCartButton.ClickAsync();
    }

    public async Task GoToCartAsync()
    {
        await _page.Locator(".shopping_cart_link").ClickAsync();
    }

    public async Task<int> GetCartBadgeCountAsync()
    {
        var badge = _page.Locator(".shopping_cart_badge");
        if (await badge.IsVisibleAsync())
        {
            return int.Parse(await badge.InnerTextAsync());
        }
        return 0;
    }
}

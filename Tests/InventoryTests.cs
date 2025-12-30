using FluentAssertions;
using nunit_sample.Libraries;
using nunit_sample.Pages;

namespace nunit_sample.Tests;

[TestFixture]
public class InventoryTests : BaseTest
{
    private LoginPage _loginPage;
    private InventoryPage _inventoryPage;
    private CartPage _cartPage;
    private CheckoutPage _checkoutPage;

    [SetUp]
    public void PageSetup()
    {
        _loginPage = new LoginPage(Page);
        _inventoryPage = new InventoryPage(Page);
        _cartPage = new CartPage(Page);
        _checkoutPage = new CheckoutPage(Page);
    }

    [Test]
    public async Task AddToCart_ShouldUpdateBadge()
    {
        await _loginPage.LoginAsync(TestConfig.Username, TestConfig.Password);
        await _inventoryPage.AddToCartAsync("Sauce Labs Backpack");
       
        var count = await _inventoryPage.GetCartBadgeCountAsync();
        count.Should().Be(1);

        await _inventoryPage.AddToCartAsync("Sauce Labs Bike Light");
        count = await _inventoryPage.GetCartBadgeCountAsync();
        count.Should().Be(2);
    }

    [Test]
    public async Task Checkout_ShouldCompleteSuccessfully()
    {
        await _loginPage.LoginAsync(TestConfig.Username, TestConfig.Password);
        await _inventoryPage.AddToCartAsync("Sauce Labs Backpack");
        await _inventoryPage.GoToCartAsync();
        await _cartPage.CheckoutAsync();
        await _checkoutPage.FillInformationAsync("Chinmay", "M", "90210");
        await _checkoutPage.FinishCheckoutAsync();
        var header = await _checkoutPage.GetCompleteHeaderAsync();
        header.Should().Be("Thank you for your order!");
    }
}

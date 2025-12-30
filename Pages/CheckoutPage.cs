using Microsoft.Playwright;

namespace nunit_sample.Pages;

public class CheckoutPage : BasePage
{
    private ILocator FirstNameInput => _page.Locator("#first-name");
    private ILocator LastNameInput => _page.Locator("#last-name");
    private ILocator ZipCodeInput => _page.Locator("#postal-code");
    private ILocator ContinueButton => _page.Locator("#continue");
    private ILocator FinishButton => _page.Locator("#finish");
    private ILocator CompleteHeader => _page.Locator(".complete-header");

    public CheckoutPage(IPage page) : base(page)
    {
    }

    public async Task FillInformationAsync(string firstName, string lastName, string zip)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await ZipCodeInput.FillAsync(zip);
        await ContinueButton.ClickAsync();
    }

    public async Task FinishCheckoutAsync()
    {
        await FinishButton.ClickAsync();
    }

    public async Task<string> GetCompleteHeaderAsync()
    {
        return await CompleteHeader.InnerTextAsync();
    }
}

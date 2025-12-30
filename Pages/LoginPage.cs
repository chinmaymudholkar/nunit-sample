using Microsoft.Playwright;
using nunit_sample.Libraries;

namespace nunit_sample.Pages;

public class LoginPage : BasePage
{
    private ILocator UsernameInput => _page.Locator("[data-test='username']");
    private ILocator PasswordInput => _page.Locator("[data-test='password']");
    private ILocator LoginButton => _page.Locator("[data-test='login-button']");
    private ILocator ErrorMessage => _page.Locator("[data-test='error']");

    public LoginPage(IPage page) : base(page)
    {
    }

    public async Task LoginAsync(string username, string password)
    {
        await _page.GotoAsync(TestConfig.BaseUrl);
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    public async Task<string> GetErrorMessageAsync()
    {
        return await ErrorMessage.InnerTextAsync();
    }

    public string GetUrl()
    {
        return _page.Url;
    }
}

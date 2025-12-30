using Microsoft.Playwright;

namespace nunit_sample.Pages;

public abstract class BasePage
{
    protected readonly IPage _page;

    protected BasePage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToAsync(string url)
    {
        await _page.GotoAsync(url);
    }
}

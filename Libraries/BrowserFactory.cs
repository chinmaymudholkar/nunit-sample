using Microsoft.Playwright;

namespace nunit_sample.Libraries;

public static class BrowserFactory
{
    public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
    {
        var browserType = TestConfig.Browser switch
        {
            "chrome" => playwright.Chromium,
            "edge" => playwright.Chromium, // Edge is Chromium
            "firefox" => playwright.Firefox,
            "webkit" => playwright.Webkit,
            _ => playwright.Firefox
        };

        var options = new BrowserTypeLaunchOptions
        {
            Headless = TestConfig.Headless,
            SlowMo = TestConfig.SlowMo,
            Channel = TestConfig.Browser switch
            {
                "edge" => "msedge",
                "chrome" => "chrome",
                _ => null
            }
        };

        return await browserType.LaunchAsync(options);
    }
}

using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Playwright;
using nunit_sample.Libraries;
using NUnit.Framework.Interfaces;

namespace nunit_sample.Tests;

[AllureNUnit]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public class BaseTest
{
    private IPlaywright Playwright { get; set; } = null!;
    private IBrowser Browser { get; set; } = null!;
    private IBrowserContext Context { get; set; } = null!;
    protected IPage Page { get; private set; } = null!;

    [SetUp]
    public async Task Setup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await BrowserFactory.CreateBrowserAsync(Playwright);
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var testName = GetCleanScreenshotFileName(TestContext.CurrentContext.Test.Name);
            var screenshot = await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"screenshot_{testName}.png"),
                FullPage = true
            });
            AllureApi.AddAttachment("Screenshot", "image/png", screenshot);
        }

        await Context.CloseAsync();
        await Browser.CloseAsync();
        Playwright.Dispose();
    }

    private static string GetCleanScreenshotFileName(string testName)
    {
        // Clean up test name to remove characters not allowed in Windows filenames
        var invalidChars = Path.GetInvalidFileNameChars();
        foreach (var invalidChar in invalidChars)
        {
            testName = testName.Replace(invalidChar, '_');
        }
        
        // Also replace common problematic characters
        testName = testName.Replace(" ", "_");
        
        return testName;
    }
}

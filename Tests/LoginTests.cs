using FluentAssertions;
using nunit_sample.Libraries;
using nunit_sample.Pages;
using NUnit.Framework.Constraints;

namespace nunit_sample.Tests;

[TestFixture]
public class LoginTests : BaseTest
{
    private LoginPage _loginPage;

    [SetUp]
    public void PageSetup()
    {
        _loginPage = new LoginPage(Page);
    }

    public static IEnumerable<TestCaseData> ValidCredentials
    {
        get
        {
            yield return new TestCaseData(TestConfig.Username, TestConfig.Password);
        }
    }

    public static IEnumerable<TestCaseData> InvalidCredentials
    {
        get
        {
            yield return new TestCaseData("locked_out_user", "secret_sauce", "Epic sadface: Sorry, this user has been locked out.");
            yield return new TestCaseData("invalid_user", "invalid_pass", "Epic sadface: Username and password do not match any user in this service");
            yield return new TestCaseData("", "secret_sauce", "Epic sadface: Username is required");
            yield return new TestCaseData("standard_user", "", "Epic sadface: Password is required");
        }
    }

    [Test, TestCaseSource(nameof(ValidCredentials))]
    public async Task Login_ValidCredentials_ShouldRedirectToInventory(string username, string password)
    {
        await _loginPage.LoginAsync(username, password);
        var url = _loginPage.GetUrl();
        url.Should().Contain("inventory.html");
        await new InventoryPage(Page).LogoutAsync();
        _loginPage.GetUrl().Should().Be(TestConfig.BaseUrl);
    }

    [Test, TestCaseSource(nameof(InvalidCredentials))]
    public async Task Login_InvalidCredentials_ShouldShowErrorMessage(string username, string password, string expectedError)
    {
        await _loginPage.LoginAsync(username, password);
        var errorMessage = await _loginPage.GetErrorMessageAsync();
        errorMessage.Should().Be(expectedError);
    }
}

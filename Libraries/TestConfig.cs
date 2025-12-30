using DotNetEnv;

namespace nunit_sample.Libraries;

public static class TestConfig
{
    static TestConfig()
    {
        Env.Load();
    }

    public static string BaseUrl => Environment.GetEnvironmentVariable("BASE_URL") ?? throw new InvalidOperationException("BASE_URL env variable is not set");
    public static string Username => Environment.GetEnvironmentVariable("SWAG_LABS_USERNAME") ?? throw new InvalidOperationException("SWAG_LABS_USERNAME env variable is not set");
    public static string Password => Environment.GetEnvironmentVariable("SWAG_LABS_PASSWORD") ?? throw new InvalidOperationException("SWAG_LABS_PASSWORD env variable is not set");
    public static string Browser => Environment.GetEnvironmentVariable("BROWSER")?.ToLower() ?? "firefox";
    public static bool Headless => bool.Parse(Environment.GetEnvironmentVariable("HEADLESS") ?? "true");
    public static int SlowMo => int.Parse(Environment.GetEnvironmentVariable("SLOW_MO") ?? "0");
}

namespace ZaloTools.Services;

public class ChromeService : IChromeService
{
    private string binaryLocationChrome = "C:\\Program Files\\Google\\Chrome\\Application\\Chrome.exe";

    public byte[] TakesScreenshot(ChromeDriver chromeDriver, IWebElement webElement)
    {
        return (webElement as ITakesScreenshot)?.GetScreenshot()?.AsByteArray;
    }

    public Task<bool> CheckLoginZaloAsync(ChromeDriver chromeDriver)
    {
        var now = DateTime.Now;
        while (true)
        {
            var cookies = chromeDriver.Manage().Cookies.GetCookieNamed("zlogin_session");
            if (cookies == null)
            {
                return Task.FromResult(true);
            }

            if ((DateTime.Now - now).Minutes > 1)
            {
                return Task.FromResult(false);
            }
        }
    }

    public Task<ChromeDriver> GetChromeDriverAsync(string pathChromeProfile, bool hideChrome = true)
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.BinaryLocation = binaryLocationChrome;
        chromeOptions.AddExcludedArgument("enable-automation");
        if (hideChrome)
            chromeOptions.AddArgument("headless");
        chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
        chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
        chromeOptions.AddArgument(@"user-data-dir=" + pathChromeProfile);
        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
        chromeDriverService.HideCommandPromptWindow = true;
        return Task.FromResult(new ChromeDriver(chromeDriverService, chromeOptions));
    }
}
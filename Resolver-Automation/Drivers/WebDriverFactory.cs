namespace Resolver.Automation.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(TestSettings settings)
        {
            var browser = ParseBrowser(settings.Browser);
            IWebDriver driver = browser switch
            {
                BrowserType.Edge => BuildEdgeDriver(settings),
                _ => BuildChromeDriver(settings)
            };

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(settings.Timeouts.PageLoadTimeoutSeconds);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.Timeouts.ImplicitWaitSeconds);
            driver.Manage().Window.Maximize();

            return driver;
        }

        private static BrowserType ParseBrowser(string? browser) =>
            Enum.TryParse(browser, ignoreCase: true, out BrowserType parsed)
                ? parsed
                : BrowserType.Chrome;

        private static IWebDriver BuildChromeDriver(TestSettings settings)
        {
            var options = new ChromeOptions();
            if (settings.Headless)
            {
                options.AddArgument("--headless=new");
            }
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--allow-file-access-from-files");

            return new ChromeDriver(options);
        }

        private static IWebDriver BuildEdgeDriver(TestSettings settings)
        {
            var options = new EdgeOptions();
            if (settings.Headless)
            {
                options.AddArgument("--headless=new");
            }
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--allow-file-access-from-files");

            return new EdgeDriver(options);
        }
    }
}

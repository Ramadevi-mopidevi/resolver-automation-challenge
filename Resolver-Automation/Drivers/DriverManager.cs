namespace Resolver.Automation.Drivers
{
    public sealed class DriverManager : IDisposable
    {
        private readonly TestSettings _settings;
        private IWebDriver? _driver;

        public DriverManager(TestSettings? settings = null)
        {
            _settings = settings ?? ConfigReader.Settings;
        }

        public IWebDriver GetDriver()
        {
            _driver ??= WebDriverFactory.CreateWebDriver(_settings);
            return _driver;
        }

        public void Dispose()
        {
            if (_driver == null)
            {
                return;
            }

            try
            {
                _driver.Manage().Cookies.DeleteAllCookies();
            }
            catch
            {
                // ignore cleanup failures
            }

            _driver.Quit();
            _driver.Dispose();
            _driver = null;
        }
    }
}

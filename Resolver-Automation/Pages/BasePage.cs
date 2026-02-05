namespace Resolver.Automation.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }
        protected WebDriverWait Wait { get; }

        protected BasePage(IWebDriver driver, TestSettings settings)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(settings.Timeouts.ExplicitWaitSeconds));
        }

        protected IWebElement WaitUntilVisible(By locator)
        {
            return Wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            })!;
        }

        public void NavigateTo(string url) => Driver.Navigate().GoToUrl(url);

        protected IWebElement Find(By locator) => Driver.FindElement(locator);

        public void Click(By locator)
        {
            var element = WaitUntilVisible(locator);
            element.Click();
        }

        public void SendKeys(By locator, string text, bool clearFirst = true)
        {
            var element = WaitUntilVisible(locator);
            if (clearFirst)
            {
                element.Clear();
            }
            element.SendKeys(text);
        }

        public string GetText(By locator)
        {
            var element = WaitUntilVisible(locator);
            return element.Text;
        }

        public bool IsVisible(By locator)
        {
            try
            {
                var element = WaitUntilVisible(locator);
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }
    }
}

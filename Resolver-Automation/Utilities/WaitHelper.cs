namespace Resolver.Automation.Utilities
{
    public static class WaitHelper
    {
        public static IWebElement WaitForElement(IWebDriver driver, By locator, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(d => d.FindElement(locator));
        }
    }
}

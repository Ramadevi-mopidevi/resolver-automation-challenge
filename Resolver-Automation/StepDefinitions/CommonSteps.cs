namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        public CommonSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [Given("the user is on the home page")]
        public void GivenTheUserIsOnTheHomePage()
        {
            var baseUrl = ConfigReader.GetBaseUrl();
            TestLogger.Action($"Navigating to home page: {baseUrl}");
            MainPage.NavigateTo(baseUrl);
        }
    }
}

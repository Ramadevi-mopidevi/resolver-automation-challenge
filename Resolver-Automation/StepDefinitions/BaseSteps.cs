namespace Resolver.Automation.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected IWebDriver Driver { get; }
        protected TestSettings Settings { get; }
        protected ScenarioContext ScenarioContext { get; }
        private MainPage? _mainPage;

        protected MainPage MainPage => _mainPage ??= new MainPage(Driver, Settings);

        protected BaseSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
        {
            Driver = driver;
            Settings = settings;
            ScenarioContext = scenarioContext;
        }
    }
}

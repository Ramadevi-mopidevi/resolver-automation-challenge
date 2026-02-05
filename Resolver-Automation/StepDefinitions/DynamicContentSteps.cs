namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class DynamicContentSteps : BaseSteps
    {
        private MainPage? _mainPage;
        private MainPage MainPage => _mainPage ??= new MainPage(Driver, Settings);

        public DynamicContentSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [When("the dynamic button appears")]
        public void WhenTheDynamicButtonAppears()
        {
            TestLogger.Action("Waiting for dynamic button to appear");
            MainPage.WaitForDynamicButtonToAppear();
            Assert.That(MainPage.IsDynamicButtonVisible(), Is.True, "Dynamic button should be visible after wait.");
        }

        [When("the user clicks the dynamic button")]
        public void WhenTheUserClicksTheDynamicButton()
        {
            TestLogger.Action("Clicking dynamic button");
            MainPage.ClickDynamicButton();
        }

        [Then("a success alert is shown")]
        public void ThenASuccessAlertIsShown()
        {
            Assert.That(MainPage.IsSuccessAlertVisible(), Is.True, "Success alert should be visible after clicking dynamic button.");
        }

        [Then("the dynamic button becomes disabled")]
        public void ThenTheDynamicButtonBecomesDisabled()
        {
            Assert.That(MainPage.IsDynamicButtonEnabled(), Is.False, "Dynamic button should be disabled after click.");
        }
    }
}

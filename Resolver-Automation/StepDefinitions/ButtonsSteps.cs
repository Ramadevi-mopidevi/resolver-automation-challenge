namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class ButtonsSteps : BaseSteps
    {
        private MainPage? _mainPage;
        private MainPage MainPage => _mainPage ??= new MainPage(Driver, Settings);

        public ButtonsSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [Then("the first button is enabled")]
        public void ThenTheFirstButtonIsEnabled()
        {
            TestLogger.Action("Verifying primary button state");
            Assert.Multiple(() =>
            {
                Assert.That(MainPage.IsPrimaryButtonVisible(), Is.True, "Primary button should be visible.");
                Assert.That(MainPage.IsPrimaryButtonEnabled(), Is.True, "Primary button should be enabled.");
            });
        }

        [Then("the second button is disabled")]
        public void ThenTheSecondButtonIsDisabled()
        {
            TestLogger.Action("Verifying secondary button state");
            Assert.Multiple(() =>
            {
                Assert.That(MainPage.IsSecondaryButtonVisible(), Is.True, "Secondary button should be visible.");
                Assert.That(MainPage.IsSecondaryButtonEnabled(), Is.False, "Secondary button should be disabled.");
            });
        }
    }
}

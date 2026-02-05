namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class DropdownSteps : BaseSteps
    {
        public DropdownSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [Then("the dropdown defaults to \"(.*)\"")]
        public void ThenTheDropdownDefaultsTo(string expected)
        {
            TestLogger.Action($"Asserting dropdown defaults to '{expected}'");
            var selected = MainPage.GetSelectedDropdownText();
            Assert.That(selected, Is.EqualTo(expected), "Default dropdown value mismatch.");
        }

        [When("the user selects \"(.*)\" from the dropdown")]
        public void WhenTheUserSelectsFromTheDropdown(string option)
        {
            TestLogger.Action($"Selecting dropdown option '{option}'");
            MainPage.SelectDropdownOption(option);
        }

        [Then("the dropdown displays \"(.*)\"")]
        public void ThenTheDropdownDisplaysOption(string option)
        {
            var selected = MainPage.GetSelectedDropdownText();
            Assert.That(selected, Is.EqualTo(option), "Dropdown selection should match the chosen option.");
        }
    }
}

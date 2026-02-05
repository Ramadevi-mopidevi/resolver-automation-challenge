namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class ListItemsSteps : BaseSteps
    {
        public ListItemsSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [When("the user reviews the list group")]
        public void WhenTheUserReviewsTheListGroup()
        {
            TestLogger.Action("Reviewing list group content");
        }

        [Then("the list group contains (.*) items")]
        public void ThenTheListGroupContainsItems(int expectedCount)
        {
            TestLogger.Action($"Validating list contains {expectedCount} items");
            var items = MainPage.GetListItemsWithBadges();
            Assert.That(items.Count, Is.EqualTo(expectedCount), "Unexpected list item count.");
        }

        [Then("the second list item text equals \"(.*)\"")]
        public void ThenTheSecondListItemTextEquals(string expectedText)
        {
            TestLogger.Action("Validating second list item text");
            var items = MainPage.GetListItemsWithBadges();
            Assert.That(items.Count, Is.GreaterThanOrEqualTo(2), "List should have at least two items.");
            Assert.That(items[1].Text, Is.EqualTo(expectedText), "Second list item text mismatch.");
        }

        [Then("the second list item badge equals \"(.*)\"")]
        public void ThenTheSecondListItemBadgeEquals(string expectedBadge)
        {
            TestLogger.Action("Validating second list item badge value");
            var items = MainPage.GetListItemsWithBadges();
            Assert.That(items.Count, Is.GreaterThanOrEqualTo(2), "List should have at least two items.");
            Assert.That(items[1].Badge, Is.EqualTo(expectedBadge), "Second list item badge mismatch.");
        }
    }
}

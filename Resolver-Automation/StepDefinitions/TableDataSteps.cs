namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class TableDataSteps : BaseSteps
    {
        private MainPage? _mainPage;
        private MainPage MainPage => _mainPage ??= new MainPage(Driver, Settings);

        public TableDataSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [When("the user reads the table cell at row (.*) column (.*)")]
        public void WhenTheUserReadsTheTableCellAtRowAndColumn(int rowIndex, int columnIndex)
        {
            TestLogger.Action($"Reading table cell at row {rowIndex}, column {columnIndex}");
            var value = MainPage.GetTableCellValue(rowIndex, columnIndex);
            ScenarioContext["TableCellValue"] = value;
        }

        [Then("the table cell value equals \"(.*)\"")]
        public void ThenTheTableCellValueEquals(string expectedValue)
        {
            var actual = ScenarioContext.TryGetValue("TableCellValue", out string? value) ? value : string.Empty;
            Assert.That(actual, Is.EqualTo(expectedValue), "Table cell value mismatch.");
        }
    }
}

namespace Resolver.Automation.StepDefinitions
{
    [Binding]
    public class LoginSteps : BaseSteps
    {
        public LoginSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
            : base(driver, settings, scenarioContext)
        {
        }

        [Then("the login form shows the email and password inputs and the sign-in button")]
        public void ThenTheLoginFormShowsRequiredControls()
        {
            TestLogger.Action("Validating login controls are visible");
            Assert.That(MainPage.AreLoginControlsVisible(), Is.True, "Email, password, and sign-in controls should be visible.");
        }

        [When("the user enters the email \"(.*)\"")]
        public void WhenTheUserEntersTheEmail(string email)
        {
            TestLogger.Action($"Entering login email '{email}'");
            MainPage.EnterEmail(email);
            ScenarioContext["LoginEmail"] = email;
        }

        [When("the user enters the password \"(.*)\"")]
        public void WhenTheUserEntersThePassword(string password)
        {
            TestLogger.Action("Entering login password");
            MainPage.EnterPassword(password);
            ScenarioContext["LoginPassword"] = password;
        }

        [Then("the login form retains the entered email and password")]
        public void ThenTheLoginFormRetainsTheEnteredEmailAndPassword()
        {
            var expectedEmail = ScenarioContext.TryGetValue("LoginEmail", out string? email) ? email : string.Empty;
            var expectedPassword = ScenarioContext.TryGetValue("LoginPassword", out string? password) ? password : string.Empty;

            Assert.Multiple(() =>
            {
                Assert.That(MainPage.GetEmailValue(), Is.EqualTo(expectedEmail), "Email input should contain entered value.");
                Assert.That(MainPage.GetPasswordValue(), Is.EqualTo(expectedPassword), "Password input should contain entered value.");
            });
        }
    }
}

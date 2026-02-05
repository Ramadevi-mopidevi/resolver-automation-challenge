namespace Resolver.Automation.Pages
{
    public class MainPage : BasePage
    {
        private readonly By _loginEmailInput = By.Id("inputEmail");
        private readonly By _loginPasswordInput = By.Id("inputPassword");
        private readonly By _signInButton = By.CssSelector("#test-1-div button[type='submit']");

        private readonly By _listItems = By.CssSelector("#test-2-div ul.list-group li");
        private readonly By _listItemBadge = By.CssSelector("span.badge");

        private readonly By _dropdownButton = By.Id("dropdownMenuButton");
        private readonly By _dropdownItems = By.CssSelector("#test-3-div .dropdown-item");

        private readonly By _primaryButton = By.CssSelector("#test-4-div .btn-primary");
        private readonly By _secondaryButton = By.CssSelector("#test-4-div .btn-secondary");

        private readonly By _dynamicContentPlaceholder = By.Id("test5-placeholder");
        private readonly By _dynamicButton = By.Id("test5-button");
        private readonly By _successAlert = By.Id("test5-alert");

        private readonly By _tableRows = By.CssSelector("#test-6-div table tbody tr");
        private readonly By _tableCells = By.CssSelector("td");

        public MainPage(IWebDriver driver, TestSettings settings) : base(driver, settings)
        {
        }

        public void EnterEmail(string email) => SendKeys(_loginEmailInput, email);

        public void EnterPassword(string password) => SendKeys(_loginPasswordInput, password);

        public void ClickSignIn() => Click(_signInButton);

        public bool IsSignInButtonVisible() => IsVisible(_signInButton);

        public bool AreLoginControlsVisible() => IsVisible(_loginEmailInput) && IsVisible(_loginPasswordInput) && IsVisible(_signInButton);

        public string GetEmailValue() => Find(_loginEmailInput).GetAttribute("value");

        public string GetPasswordValue() => Find(_loginPasswordInput).GetAttribute("value");

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickSignIn();
        }

        public IReadOnlyCollection<IWebElement> GetListItems() => Driver.FindElements(_listItems);

        public IList<string> GetListItemTexts()
        {
            return GetListItems().Select(item => item.Text.Trim()).ToList();
        }

        public IList<(string Text, string Badge)> GetListItemsWithBadges()
        {
            return GetListItems()
                .Select(item =>
                {
                    var badgeText = item.FindElement(_listItemBadge).Text.Trim();
                    var labelText = item.Text.Replace(badgeText, string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
                    return (labelText, badgeText);
                })
                .ToList();
        }

        public void OpenDropdown() => Click(_dropdownButton);

        private bool IsDropdownOpen()
        {
            var dropdown = Find(_dropdownButton);
            var expanded = dropdown.GetAttribute("aria-expanded");
            return string.Equals(expanded, "true", StringComparison.OrdinalIgnoreCase);
        }

        public void SelectDropdownOption(string optionText)
        {
            if (!IsDropdownOpen())
            {
                OpenDropdown();
            }

            var option = Wait.Until(d =>
            {
                var items = d.FindElements(_dropdownItems);
                return items.FirstOrDefault(o => string.Equals(o.Text.Trim(), optionText, StringComparison.OrdinalIgnoreCase) && o.Displayed);
            });

            option?.Click();
        }

        public string GetSelectedDropdownText() => GetText(_dropdownButton);

        public void ClickPrimaryButton() => Click(_primaryButton);

        public bool IsSecondaryButtonEnabled() => IsVisible(_secondaryButton) && Driver.FindElement(_secondaryButton).Enabled;

        public bool IsPrimaryButtonEnabled() => IsVisible(_primaryButton) && Driver.FindElement(_primaryButton).Enabled;

        public bool IsPrimaryButtonVisible() => IsVisible(_primaryButton);

        public bool IsSecondaryButtonVisible() => IsVisible(_secondaryButton);

        public bool IsDynamicButtonVisible() => IsVisible(_dynamicButton);

        public void WaitForDynamicButtonToAppear() => Wait.Until(_ => Driver.FindElement(_dynamicButton).Displayed);

        public void ClickDynamicButton() => Click(_dynamicButton);

        public bool IsSuccessAlertVisible() => IsVisible(_successAlert);

        public bool IsDynamicPlaceholderVisible() => IsVisible(_dynamicContentPlaceholder);

        public bool IsDynamicButtonEnabled()
        {
            try
            {
                return Driver.FindElement(_dynamicButton).Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public IList<(string FirstName, string LastName, string AppName)> GetTableRows()
        {
            var rows = Driver.FindElements(_tableRows);
            var result = new List<(string FirstName, string LastName, string AppName)>();

            foreach (var row in rows)
            {
                var cells = row.FindElements(_tableCells);
                if (cells.Count >= 3)
                {
                    result.Add((cells[0].Text.Trim(), cells[1].Text.Trim(), cells[2].Text.Trim()));
                }
            }

            return result;
        }

        public string GetTableCellValue(int rowIndex, int columnIndex)
        {
            var rows = Driver.FindElements(_tableRows);
            if (rowIndex < 0 || rowIndex >= rows.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Row index {rowIndex} is out of range.");
            }

            var cells = rows[rowIndex].FindElements(_tableCells);
            if (columnIndex < 0 || columnIndex >= cells.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), $"Column index {columnIndex} is out of range.");
            }

            return cells[columnIndex].Text.Trim();
        }
    }
}

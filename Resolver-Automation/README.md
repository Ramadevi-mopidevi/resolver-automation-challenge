# Resolver Automation

SpecFlow + NUnit UI automation that validates the Resolver QE sample page (login form, list data, dropdown, button states, dynamic content, and table values) using Selenium with a lean Page Object Model.

## Design choices
- **BDD with SpecFlow** keeps coverage readable and ties Gherkin scenarios to focused step definitions.
- **Page Object Model** centralizes selectors and user actions in `Pages/MainPage.cs`, keeping steps declarative.
- **Shared browser session per feature** avoids driver churn; navigation isolates scenarios. Parallelization is disabled to protect the shared session.
- **Hooks**: `BeforeFeature` starts the shared driver, `AfterFeature` disposes it, `BeforeScenario` registers it into DI, and `AfterStep` captures screenshots on failure.

## Tech stack
- .NET 8, C#
- SpecFlow + NUnit
- Selenium WebDriver (Chrome, Edge)
- Microsoft.Extensions.Configuration for settings binding

## Folder structure (high level)
- `Features/` – Gherkin coverage (`Index.feature`)
- `StepDefinitions/` – Step bindings delegating to page objects
- `Pages/` – Page Object Model (`MainPage`)
- `Drivers/` – Driver creation and lifecycle management
- `Hooks/` – SpecFlow hooks for driver sharing, logging, and screenshots
- `Config/` – Strongly typed settings model
- `Utilities/` – Config loader, waits, and logging helpers
- `TestData/` – Local HTML assets under test

## Configuration (`appsettings.json`)
`TestSettings` drives execution:
- `Browser`: `Chrome` or `Edge`
- `Headless`: toggle headless mode
- `BaseUrl`: absolute URL or relative path to the HTML under test (relative paths are resolved from the test assembly location)
- `Timeouts`: `ImplicitWaitSeconds`, `ExplicitWaitSeconds`, `PageLoadTimeoutSeconds`

## Running the tests
```
dotnet test Resolver-Automation/Resolver-Automation.csproj -s Resolver-Automation.runsettings
```
Optionally adjust `appsettings.json` before running to point at a different HTML file or browser mode.

## Assumptions and trade-offs
- A single browser instance is shared per feature to reduce driver start-up time; scenarios rely on navigation to reset state.
- Parallel execution is disabled to avoid contention on the shared driver.
- Local HTML is loaded via `BaseUrl`; remote hosting would only require updating this setting.

## Future scaling
- Add CI wiring with artifacts for screenshots and test results.
- Extend the Page Object Model into smaller fragments if coverage grows.
- Introduce per-scenario drivers if tests need to run in parallel or against mutable environments.
- Add structured logging sinks for richer diagnostics when needed.

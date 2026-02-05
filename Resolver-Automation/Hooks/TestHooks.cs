using BoDi;
using NUnit.Framework;
using System.Diagnostics;

[assembly: Parallelizable(ParallelScope.None)]

namespace Resolver.Automation.Hooks
{
    // WebDriver is shared at the feature level to limit browser start-up overhead while keeping scenarios isolated via navigation.
    [Binding]
    public class TestHooks
    {
        private static readonly object DriverLock = new();
        private static DriverManager? _driverManager;
        private static IWebDriver? _driver;

        private readonly IObjectContainer _container;
        private readonly TestSettings _settings;
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
            _settings = ConfigReader.Settings;
            _container.RegisterInstanceAs(_settings);
        }

        [BeforeFeature(Order = 0)]
        public static void InitializeWebDriverForFeature()
        {
            lock (DriverLock)
            {
                KillDriverProcesses();

                if (_driverManager != null)
                {
                    return;
                }

                var settings = ConfigReader.Settings;
                TestLogger.Info($"Initializing WebDriver for feature with browser {settings.Browser} (Headless: {settings.Headless})");
                _driverManager = new DriverManager(settings);
                _driver = _driverManager.GetDriver();
            }
        }

        [AfterFeature(Order = 1000)]
        public static void CleanupWebDriverForFeature()
        {
            lock (DriverLock)
            {
                TestLogger.Info("Tearing down shared WebDriver instance");
                _driverManager?.Dispose();
                _driverManager = null;
                _driver = null;

                KillDriverProcesses();
            }
        }

        [BeforeScenario(Order = 0)]
        public void RegisterSharedWebDriver()
        {
            if (_driver == null || _driverManager == null)
            {
                throw new InvalidOperationException("WebDriver was not initialized for the feature.");
            }

            TestLogger.Info("Registering shared WebDriver for scenario");
            _container.RegisterInstanceAs(_driverManager);
            _container.RegisterInstanceAs(_driver);
        }

        [BeforeScenario(Order = 1)]
        public void LogScenarioStart()
        {
            var scenario = _scenarioContext.ScenarioInfo;
            TestLogger.Info($"Starting scenario: {scenario.Title}");
            TestLogger.Info($"BaseUrl: {_settings.BaseUrl}");
        }

        [AfterScenario(Order = 200)]
        public void LogScenarioEnd()
        {
            var status = _scenarioContext.TestError == null ? "Passed" : "Failed";
            TestLogger.Info($"Finished scenario: {_scenarioContext.ScenarioInfo.Title} | Status: {status}");
        }

        [AfterStep]
        public void CaptureFailureScreenshot()
        {
            if (_scenarioContext.TestError == null || _driver == null)
            {
                return;
            }

            try
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                var screenshotsDir = Path.Combine(AppContext.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var fileName = $"{SanitizeFileName(_scenarioContext.ScenarioInfo.Title)}_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}.png";
                var fullPath = Path.Combine(screenshotsDir, fileName);
                screenshot.SaveAsFile(fullPath);

                TestLogger.Warn($"Captured failure screenshot at {fullPath}");
            }
            catch (Exception ex)
            {
                TestLogger.Error("Failed to capture screenshot", ex);
            }
        }

        private static string SanitizeFileName(string name)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Concat(name.Select(ch => invalidChars.Contains(ch) ? '_' : ch));
        }

        private static void KillDriverProcesses()
        {
            foreach (var processName in new[] { "chromedriver", "msedgedriver" })
            {
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    try
                    {
                        process.Kill(entireProcessTree: true);
                    }
                    catch
                    {
                        // Ignore failures in cleanup
                    }
                }
            }
        }
    }
}

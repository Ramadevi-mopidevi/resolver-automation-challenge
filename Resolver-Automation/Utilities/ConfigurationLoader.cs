using Microsoft.Extensions.Configuration;

namespace Resolver.Automation.Utilities
{
    public static class ConfigReader
    {
        private const string SettingsSection = "TestSettings";

        private static readonly Lazy<TestSettings> _settings = new(LoadSettings, isThreadSafe: true);

        public static TestSettings Settings => _settings.Value;

        private static TestSettings LoadSettings()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var settings = new TestSettings();
            configuration.GetSection(SettingsSection).Bind(settings);

            return settings;
        }

        public static string GetBaseUrl()
        {
            if (string.IsNullOrWhiteSpace(Settings.BaseUrl))
            {
                throw new InvalidOperationException("BaseUrl is not configured.");
            }

            if (Uri.TryCreate(Settings.BaseUrl, UriKind.Absolute, out var absoluteUri))
            {
                return absoluteUri.AbsoluteUri;
            }

            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory();
            var fullPath = Path.GetFullPath(Path.Combine(basePath, Settings.BaseUrl));
            return new Uri(fullPath).AbsoluteUri;
        }
    }
}

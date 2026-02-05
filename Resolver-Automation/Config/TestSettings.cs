namespace Resolver.Automation.Config
{
    public class TestSettings
    {
        public string Browser { get; set; } = "Chrome";

        public string BaseUrl { get; set; } = string.Empty;

        public TimeoutSettings Timeouts { get; set; } = new();

        public bool Headless { get; set; }
    }

    public class TimeoutSettings
    {
        public int ImplicitWaitSeconds { get; set; } = 5;

        public int ExplicitWaitSeconds { get; set; } = 10;

        public int PageLoadTimeoutSeconds { get; set; } = 30;
    }
}

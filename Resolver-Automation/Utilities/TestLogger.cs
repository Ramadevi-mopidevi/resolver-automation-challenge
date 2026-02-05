namespace Resolver.Automation.Utilities
{
    public static class TestLogger
    {
        private static string Timestamp => DateTime.UtcNow.ToString("O");

        public static void Info(string message) => Console.WriteLine($"[{Timestamp}] INFO  {message}");

        public static void Action(string message) => Console.WriteLine($"[{Timestamp}] STEP  {message}");

        public static void Warn(string message) => Console.WriteLine($"[{Timestamp}] WARN  {message}");

        public static void Error(string message, Exception? exception = null)
        {
            var detail = exception == null
                ? message
                : $"{message} | {exception.GetType().Name}: {exception.Message}";

            Console.WriteLine($"[{Timestamp}] ERROR {detail}");
        }
    }
}

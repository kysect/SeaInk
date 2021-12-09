using System;

namespace SeaInk.Utility
{
    public static class Logger
    {
        public static Action<string> LogMethod { get; set; } = ConsoleLog;

        public static void Log(object message)
            => LogMethod(message.ToString() ?? string.Empty);

        private static void ConsoleLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
using System;

namespace SeaInk.Core.Utils
{
    public static class Logger
    {
        public static Action<string> LogMethod { get; set; } = Console.WriteLine;

        public static void Log(object message)
        {
            LogMethod(message.ToString());
        }
    }
}
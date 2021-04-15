using System;

namespace SeaInk.Core.Utils
{
    public class Logger
    {
        private Logger() { }

        private static Logger Instance { get; set; }
        private static readonly object Lock = new object();

        public static LoggingDestination Destination { get; set; } = LoggingDestination.Console;
        
        public static Logger GetInstance()
        {
            if (Instance != null) 
                return Instance;
            
            lock (Lock)
            {
                Instance ??= new Logger();
            }

            return Instance;
        }

        public void Log(object message)
        {
            switch (Destination)
            {
                case LoggingDestination.Console:
                    ConsoleLog(message.ToString());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private static void ConsoleLog(string message)
        {
            Console.WriteLine(message);
        }
        
        public enum LoggingDestination
        {
            Console
        }
    }
}
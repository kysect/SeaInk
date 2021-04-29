using System;

namespace SeaInk.Core.TableIntegrations.Google.Exceptions
{
    public class DriveException : Exception
    {
        public DriveException(string message)
            : base(message) { }

        public DriveException()
            : base("Unspecified drive exception") { }
    }
}
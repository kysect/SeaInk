namespace SeaInk.Core.TableIntegrations.Google.Exceptions
{
    public class InsufficientPermissionException : DriveException
    {
        public InsufficientPermissionException(string message)
            : base($"Insufficient permission for accessing google drive\n{message}") { }

        public InsufficientPermissionException()
            : this("") { }
    }
}
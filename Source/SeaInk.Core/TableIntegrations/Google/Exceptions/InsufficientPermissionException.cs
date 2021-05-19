namespace SeaInk.Core.TableIntegrations.Google.Exceptions
{
    public class InsufficientPermissionException : DriveException
    {
        private const string BaseMessage = "Insufficient permission for accessing google drive";

        public InsufficientPermissionException(string message)
            : base($"{BaseMessage}\n{message}") { }

        public InsufficientPermissionException()
            : base(BaseMessage) { }
    }
}
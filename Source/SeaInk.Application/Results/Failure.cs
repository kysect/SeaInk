using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Results
{
    public class Failure : IResult
    {
        public Failure(string message)
        {
            Message = message.ThrowIfNull(nameof(message));
        }

        public string Message { get; }
    }
}
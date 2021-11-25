namespace SeaInk.Application.Results
{
    public class Success<T> : IResult
    {
        public Success(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
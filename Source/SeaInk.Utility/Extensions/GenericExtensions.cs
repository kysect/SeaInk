using System;

namespace SeaInk.Utility.Extensions
{
    public static class GenericExtension
    {
        public static TValue ThrowIfNull<TValue, TException>(this TValue? value, TException exception)
            where TException: Exception
        {
            if (value is null)
                throw exception;

            return value;
        }

        public static TValue ThrowIfNull<TValue>(this TValue? value, string argumentName)
            => value.ThrowIfNull(new ArgumentNullException(argumentName));

        public static TValue ThrowIfNull<TValue, TException>(this TValue? value, TException exception)
            where TException: Exception
            where TValue: struct
        {
            if (value is null)
                throw exception;

            return value.Value;
        }

        public static TValue ThrowIfNull<TValue>(this TValue? value, string argumentName) where TValue: struct
            => value.ThrowIfNull(new ArgumentException(argumentName));
    }
}
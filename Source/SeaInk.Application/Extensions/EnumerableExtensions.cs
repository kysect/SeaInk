using System;
using System.Collections.Generic;

namespace SeaInk.Application.Extensions;

internal static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T value in enumerable)
        {
            action.Invoke(value);
        }
    }
}
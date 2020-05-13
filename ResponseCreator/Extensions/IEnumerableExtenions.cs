﻿using System.Collections.Generic;
using System.Linq;

namespace ResponseCreator.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Any();
        }
    }
}

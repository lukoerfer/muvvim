using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmUtil.Util
{
    internal static class IEnumerableUtil
    {

        public static IEnumerable<T> Assert<T>(this IEnumerable<T> collection, Func<T, bool> assertion)
        {
            if (!collection.All(assertion))
            {
                throw new Exception();
            }
            return collection;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

    }
}

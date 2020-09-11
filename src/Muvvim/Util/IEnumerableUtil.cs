using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Muvvim.Util
{
    internal static class IEnumerableUtil
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="assertion"></param>
        /// <returns></returns>
        public static IEnumerable<T> Assert<T>(this IEnumerable<T> collection, Func<T, bool> assertion)
        {
            if (!collection.All(assertion))
            {
                throw new Exception();
            }
            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

    }
}

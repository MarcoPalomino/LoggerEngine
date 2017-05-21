using System;
using System.Collections.Generic;

namespace LoggerEngine.Util
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Performs an action over each item of a collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs an action over each item of a collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in collection)
            {
                action(item, i++);
            }
        }
    }
}

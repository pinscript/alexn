using System;
using System.Collections.Generic;
using System.Linq;

namespace alexn.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> instance)
        {
            return instance == null || instance.Count() == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> instance)
        {
            return instance.Count() == 0;
        }

        /// <summary>
        /// Run action over all items in a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (var item in instance)
            {
                action(item);
            }
        }

        /// <summary>
        /// Distinct a list by element property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="instance"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> instance, Func<T, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (T element in instance)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Prepend item if it's missing from the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<T> PrependIfMissing<T>(this IEnumerable<T> instance, T item)
        {
            if (instance.Contains(item))
                return instance;

            instance.ToList().Insert(0, item);
            return instance;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> instance)
        {
            var rng = new Random();
            var items = instance.ToArray();

            for(var i = 0; i < items.Length; i++)
            {
                var idx = rng.Next(i, items.Length);
                yield return items[idx];

                items[idx] = items[i];
            }
        }
    }
}
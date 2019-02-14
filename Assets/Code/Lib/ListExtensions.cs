using System;
using System.Collections;
using System.Collections.Generic;

namespace Zeyro.Extensions
{
    public static class ListExtensions
    {
        private static Random Random = new Random();

        public static void SetSeed(int seed)
        {
            Random = new Random(seed);
        }

        public static T Choice<T>(this IList<T> list)
        {
            if(list == null || list.Count < 1)
            {
                return default;
            }

            return list[Random.Next(list.Count)];
        }

        public static T Choice<T>(this T[] array)
        {
            if(array == null || array.Length < 1)
            {
                return default;
            }

            return array[Random.Next(array.Length)];
        }
    }
}
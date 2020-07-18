using System;
using System.Collections;
using System.Collections.Generic;

namespace MinimalShooting
{
    /// <summary>
    /// Utility
    /// This class implements many utility methods.
    /// </summary>
    public static class Utility
    {
        private static Random rng = new Random();

        /// <summary>
        /// https://stackoverflow.com/questions/273313/randomize-a-listt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

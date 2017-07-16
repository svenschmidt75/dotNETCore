using System;
using System.Linq;
using System.Net;

namespace Set
{
    public static class SetMethods
    {
        public static Set<T> Union<T>(this Set<T> @this, Set<T> other)
        {
            var newSet = new Set<T>();
            // O(2N) == O(N)
            @this.ForEach(newSet.Add);
            other.ForEach(newSet.Add);
            return newSet;
        }

        public static Set<T> Difference<T>(this Set<T> @this, Set<T> other)
        {
            var newSet = new Set<T>();

            // O(N^2)
            @this.ForEach(item =>
            {
                if (other.Contains(item) == false)
                {
                    newSet.Add(item);
                }
            });
            return newSet;
        }

        public static Set<T> Intersection<T>(this Set<T> @this, Set<T> other)
        {
            var newSet = new Set<T>();

            // O(N^2)
            @this.ForEach(item =>
            {
                if (other.Contains(item))
                {
                    newSet.Add(item);
                }
            });
            return newSet;
        }

        public static bool Subset<T>(this Set<T> a, Set<T> b)
        {
            // a subset b?
            return a.All(b.Contains);
        }

    }
}
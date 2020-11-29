#region

using System;
using System.Collections;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("SuffixArray.Test")]

namespace SuffixArray
{
    public class SuffixArray
    {
        private readonly string _input;

        public SuffixArray(string input)
        {
            _input = input;
            SA = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                SA[i] = i;
            }

            // SS: sorting is O(N^2 log N), where N is the number of characters
            // N^2, because comparing two strings takes O(N)
            // There is an O(N log N) algorithm available...
            Array.Sort(SA, new SuffixComparer(input));
        }

        internal int[] SA { get; }

        public int LowerBound(string text)
        {
            // SS: O(N log N), O(log N) from BS, O(N) from string compare
            var min = 0;
            var max = SA.Length;

            while (min < max)
            {
                var mid = min + (max - min) / 2;

                var saValue = SA[mid];
                var saStr = _input.Substring(saValue);

                var c = text.CompareTo(saStr);

                if (c < 0)
                {
                    max = mid;
                }
                else if (c > 0)
                {
                    min = mid + 1;
                }
                else
                {
                    // SS: strings are equal, check whether there are
                    // more to the left of mid...
                    if (mid > 0)
                    {
                        saValue = SA[mid - 1];
                        saStr = _input.Substring(saValue);
                        c = text.CompareTo(saStr);
                        if (c == 0)
                        {
                            max = mid;
                            continue;
                        }
                    }

                    return mid;
                }
            }

            return -1;
        }

        public int UpperBound(string text)
        {
            // SS: O(N log N), O(log N) from BS, O(N) from string compare
            var min = 0;
            var max = SA.Length;

            while (min < max)
            {
                var mid = min + (max - min) / 2;

                var saValue = SA[mid];
                var saStr = _input.Substring(saValue);

                var c = text.CompareTo(saStr);

                if (c < 0)
                {
                    max = mid;
                }
                else if (c > 0)
                {
                    min = mid + 1;
                }
                else
                {
                    // SS: strings are equal, check whether there are
                    // more to the left of mid...
                    if (mid <= SA.Length - 2)
                    {
                        saValue = SA[mid + 1];
                        saStr = _input.Substring(saValue);
                        c = text.CompareTo(saStr);
                        if (c == 0)
                        {
                            min = mid + 1;
                            continue;
                        }
                    }

                    return mid;
                }
            }

            return -1;
        }

        public int[] CreateLCPArray()
        {
            /* SS: The LCP array is an array in which every index tracks how many
             *     characters two adjacent sorted suffixes have in common.
             *
             * Example input: ABABBAB
             *
             *  --------------------------------------------------------
             *  | Suffix Array Value | LCP Array Value | Suffix        |
             *  ---------------------|-----------------|----------------
             *  |        5           |        0        | A B           |
             *  ---------------------|-----------------|----------------
             *  |        0           |        2        | A B A B B A B |
             *  ---------------------|-----------------|----------------
             *  |        2           |        2        | A B B A B     |
             *  ---------------------|-----------------|----------------
             *  |        6           |        0        | B             |
             *  ---------------------|-----------------|----------------
             *  |        4           |        1        | B A B         |
             *  ---------------------|-----------------|----------------
             *  |        1           |        3        | B A B B A B   |
             *  ---------------------|-----------------|----------------
             *  |        3           |        1        | B B A B       |
             *  ---------------------|-----------------|----------------
             * 
            */

            var lcp = new int[SA.Length];

            // SS: runtime complexity: O(N^2)
            // where N = _input.Length
            // There is an O(N) algorithm available...

            for (var i = 1; i < lcp.Length; i++)
            {
                var sa1 = SA[i - 1];
                var s1 = _input.Substring(sa1);

                var sa2 = SA[i];
                var s2 = _input.Substring(sa2);

                // SS: determine how many characters are in common
                var cnt = 0;
                while (cnt < Math.Min(s1.Length, s2.Length) && s1[cnt] == s2[cnt])
                {
                    cnt++;
                }

                lcp[i] = cnt;
            }

            return lcp;
        }

        public string LongestCommonSubstring()
        {
            // SS: find the LCS for the given strings
            // We first create the suffix array, because the SA will group suffixes
            // with the same prefix together (sorting).
            
            
        }
        
        private class SuffixComparer : IComparer
        {
            private readonly string _input;

            public SuffixComparer(string input)
            {
                _input = input;
            }

            public int Compare(object x, object y)
            {
                var saIdx1 = (int) x;
                var saIdx2 = (int) y;
                var s1 = _input.Substring(saIdx1);
                var s2 = _input.Substring(saIdx2);
                return s1.CompareTo(s2);
            }
        }
    }
}
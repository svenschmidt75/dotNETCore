#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 844. Backspace String Compare
// URL: https://leetcode.com/problems/backspace-string-compare/

namespace LeetCode
{
    public class Solution
    {
        public bool BackspaceCompare(string s, string t)
        {
            // return BackspaceCompare1(s, t);
            return BackspaceCompare2(s, t);
        }

        private bool BackspaceCompare2(string s, string t)
        {
            // SS: in C#, string is immutable, so copy to array and consider this
            // in-place?
            // SS: runtime complexity: O(n)
            // space complexity: O(1) if we could modify strings, otherwise O(n)
            
            var sArr = s.ToArray();
            var tArr = t.ToArray();

            var sIdx = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var c = sArr[i];

                if (c == '#')
                {
                    sIdx = Math.Max(0, sIdx - 1);
                }
                else
                {
                    sArr[sIdx++] = sArr[i];
                }
            }

            var tIdx = 0;
            for (var i = 0; i < t.Length; i++)
            {
                var c = tArr[i];

                if (c == '#')
                {
                    tIdx = Math.Max(0, tIdx - 1);
                }
                else
                {
                    tArr[tIdx++] = tArr[i];
                }
            }

            if (sIdx != tIdx)
            {
                return false;
            }

            for (var i = 0; i < tIdx; i++)
            {
                if (sArr[i] != tArr[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool BackspaceCompare1(string s, string t)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)
            var stack1 = new Stack<char>();
            var stack2 = new Stack<char>();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '#')
                {
                    if (stack1.Any())
                    {
                        stack1.Pop();
                    }
                }
                else
                {
                    stack1.Push(c);
                }
            }

            for (var i = 0; i < t.Length; i++)
            {
                var c = t[i];

                if (c == '#')
                {
                    if (stack2.Any())
                    {
                        stack2.Pop();
                    }
                }
                else
                {
                    stack2.Push(c);
                }
            }

            // SS: compare both stacks
            if (stack1.Count != stack2.Count)
            {
                return false;
            }

            while (stack1.Any() && stack2.Any())
            {
                if (stack1.Pop() != stack2.Pop())
                {
                    return false;
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("ab#c", "ad#c", true)]
            [TestCase("ab##", "c#d#", true)]
            [TestCase("a##c", "#a#c", true)]
            [TestCase("a#c", "b", false)]
            public void Test(string s, string t, bool expectedAreEqual)
            {
                // Arrange

                // Act
                var areEqual = new Solution().BackspaceCompare(s, t);

                // Assert
                Assert.AreEqual(expectedAreEqual, areEqual);
            }
        }
    }
}
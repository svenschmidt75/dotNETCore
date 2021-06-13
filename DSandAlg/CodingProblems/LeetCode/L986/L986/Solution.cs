using System;
using System.Collections.Generic;
using NUnit.Framework;

// Problem: 986. Interval List Intersections
// URL: https://leetcode.com/problems/interval-list-intersections/

namespace LeetCode
{
    public class Solution
    {
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
//            return IntervalIntersection1(firstList, secondList);
            return IntervalIntersection2(firstList, secondList);
        }

        private int[][] IntervalIntersection2(int[][] firstList, int[][] secondList)
        {
            var stack = new Stack<(int, int)>();

            List<int[]> intersection = new List<int[]>();

            int i = 0;
            int i2 = 0;
            int j = 0;
            int j2 = 0;

            while (i < firstList.Length && j < secondList.Length)
            {
                int[] first = firstList[i];
                int[] second = secondList[j];

                if (first[i2] < second[j2])
                {
                    if (i2 == 0)
                    {
                        // SS: if number of elements on the stack is even
                        if (stack.Count % 2 == 0 && stack.Peek().Item1 == 1)
                        {
                            intersection.Add(new[]{stack.Peek().Item2, first[i2]});
                        }
                        stack.Push((0, first[i2]));
                        i2++;
                    }
                    else
                    {
                        // SS: if number of elements on the stack is even
                        if (stack.Count % 2 == 0 && stack.Peek().Item1 == 0)
                        {
                            intersection.Add(new[]{stack.Peek().Item2, first[i2]});
                        }
                        stack.Push((1, first[i2]));
                        i2++;
                    }
                }
                else if (first[i2] > second[j2])
                {
                    if (j2 == 0)
                    {
                        // SS: if number of elements on the stack is even
                        if (stack.Count % 2 == 0 && stack.Peek().Item1 == 1)
                        {
                            intersection.Add(new[]{stack.Peek().Item2, second[j2]});
                        }
                        stack.Push((0, second[j2]));
                        j2++;
                    }
                    else
                    {
                        // SS: if number of elements on the stack is even
                        if (stack.Count % 2 == 0 && stack.Peek().Item1 == 0)
                        {
                            intersection.Add(new[]{stack.Peek().Item2, second[j2]});
                        }
                        stack.Push((1, second[j2]));
                        j2++;
                    }
                }
                else
                {
                    if (i2 == 0)
                    {
                        stack.Push((0, first[i2]));
                        i2++;
                    }
                    if (j2 == 0)
                    {
                        stack.Push((0, second[j2]));
                        j2++;
                    }
                    
                    
                }

            }
            
            return intersection.ToArray();
        }

        private int[][] IntervalIntersection1(int[][] firstList, int[][] secondList)
        {
            // SS: Intersect intervals

            List<int[]> intersection = new List<int[]>();

            int idx11 = 0;
            int idx12 = 0;

            int idx21 = 0;
            int idx22 = 0;

            int int1 = -1;
            int int2 = -1;

            while (idx11 < firstList.Length && idx22 < secondList.Length)
            {
                int v1 = firstList[idx11][idx12];
                int v2 = secondList[idx11][idx22];

                if (int1 == -1)
                {
                    if (v1 <= v2)
                    {
                        
                        
                        
                        
                    }
                    
                    
                    
                }
                
                
                
                
                
                
                
                if (idx12 == 0 && idx22 == 0)
                {
                    // SS: both point to the start of the interval
                    if (v1 == v2)
                    {
                        int1 = Math.Max(v1, v2);
                    }
                    else
                    {
                        int v12 = firstList[idx11][1];
                        int v22 = secondList[idx11][1];
                        
                        
                        
                        
                        
                    }
                }
                else if (idx12 == 1 && idx21 == 0 && int1 != -1)
                {
                    int2 = Math.Min(v1, v2);
                    intersection.Add(new[] { int1, int2 });
                    int1 = -1;
                    int2 = -1;
                }

                int tmpIdx11 = 0;
                int tmpIdx12 = 0;

                int tmpIdx21 = 0;
                int tmpIdx22 = 0;

                if (idx12 == 0)
                {
                    // SS: move to end point of interval
                    tmpIdx11 = idx11;
                    tmpIdx12 = 1;
                }
                else
                {
                    // SS: move to next interval
                    tmpIdx11 = idx11 + 1;
                    tmpIdx12 = 0;
                }

                if (idx22 == 0)
                {
                    // SS: move to end point of interval
                    tmpIdx21 = idx21;
                    tmpIdx22 = 1;
                }
                else
                {
                    // SS: move to next interval
                    tmpIdx21 = idx21 + 1;
                    tmpIdx22 = 0;
                }

                v1 = firstList[idx11][idx12];
                v2 = secondList[idx11][idx22];

                if (v1 <= v2)
                {
                    idx11 = tmpIdx11;
                    idx12 = tmpIdx12;
                }
                else
                {
                    idx21 = tmpIdx21;
                    idx22 = tmpIdx22;
                }
            }

            return intersection.ToArray();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] firstList = { new[] { 0, 2 }, new[] { 5, 10 }, new[] { 13, 23 }, new[] { 24, 25 } };
                int[][] secondList = { new[] { 1, 5 }, new[] { 8, 12 }, new[] { 15, 24 }, new[] { 25, 26 } };

                // Act
                int[][] result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 1, 2 }, new[] { 5, 5 }, new[] { 8, 10 }, new[] { 15, 23 }, new[] { 24, 24 }, new[] { 25, 25 } }, result);
            }
        }
    }
}
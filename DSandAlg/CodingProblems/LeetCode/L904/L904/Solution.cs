#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 904. Fruit Into Baskets
// URL: https://leetcode.com/problems/fruit-into-baskets/

namespace LeetCode
{
    public class Solution
    {
        public int TotalFruit(int[] fruits)
        {
            // return TotalFruit1(fruits);
            return TotalFruit2(fruits);
        }

        private int TotalFruit2(int[] fruits)
        {
            // SS: greedy, sliding window.
            // grow as much as possible, then shrink until
            // we can add a new fruit type.
            // runtime complexity: O(n)
            // space complexity: O(1)
            // => accepted

            int i = 0;
            int j = 0;

            var map = new Dictionary<int, int>();
            int fruitType1 = -1;
            int fruitType2 = -1;

            int globalMaxFruits = 0;
            int maxFruits = 0;

            while (j < fruits.Length)
            {
                int fruitType = fruits[j];

                if (map.ContainsKey(fruitType))
                {
                    map[fruitType]++;
                    maxFruits++;

                    j++;
                }
                else
                {
                    if (map.Count == 2)
                    {
                        // SS: we need to evict the fruit type that is not right before
                        // j
                        int evictFruitType;
                        if (fruits[j - 1] == fruitType1)
                        {
                            evictFruitType = fruitType2;
                            fruitType2 = -1;
                        }
                        else
                        {
                            evictFruitType = fruitType1;
                            fruitType1 = -1;
                        }

                        while (map.ContainsKey(evictFruitType))
                        {
                            int ft = fruits[i];
                            map[ft]--;

                            maxFruits--;

                            if (map[ft] == 0)
                            {
                                map.Remove(ft);
                            }

                            i++;
                        }
                    }
                    else
                    {
                        map[fruitType] = 1;
                        maxFruits++;

                        if (fruitType1 == -1)
                        {
                            fruitType1 = fruitType;
                        }
                        else
                        {
                            fruitType2 = fruitType;
                        }

                        j++;
                    }
                }

                globalMaxFruits = Math.Max(globalMaxFruits, maxFruits);
            }

            return globalMaxFruits;
        }

        private int TotalFruit1(int[] fruits)
        {
            // SS: runtime complexity: O(n^2)
            // space complexity: O(1)
            // => time-limit exceeded...

            int Solve(int startIdx, HashSet<int> set)
            {
                int nFruit = 0;
                int i = startIdx;

                while (i < fruits.Length)
                {
                    int fruitType = fruits[i];

                    if (set.Contains(fruitType) == false)
                    {
                        // SS: is there an empty basket to add the fruit to?
                        if (set.Count == 2)
                        {
                            // SS: no, all baskets are exhausted
                            return nFruit;
                        }

                        // SS: yes, we can add the fruit
                        set.Add(fruitType);
                    }

                    nFruit++;
                    i++;
                }

                return nFruit;
            }

            int globalMaxFruits = 0;
            
            int idx = 0;
            while (idx < fruits.Length)
            {
                int maxFruit = Solve(idx, new HashSet<int>());
                globalMaxFruits = Math.Max(globalMaxFruits, maxFruit);
                idx++;
            }

            return globalMaxFruits;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[]{1, 2, 1}, 3)]
            [TestCase(new[] { 0, 1, 2, 2 }, 3)]
            [TestCase(new[] { 1, 2, 3, 2, 2 }, 4)]
            [TestCase(new[] { 3, 3, 3, 1, 2, 1, 1, 2, 3, 3, 4 }, 5)]
            [TestCase(new[] { 1, 2, 2, 2, 3, 3 }, 5)]
            [TestCase(new[] { 1 }, 1)]
            [TestCase(new[] { 1, 2, 1, 2, 2, 3, 3, 3, 3 }, 6)]
            public void Test(int[] fruits, int expected)
            {
                // Arrange

                // Act
                int nFruits = new Solution().TotalFruit(fruits);

                // Assert
                Assert.AreEqual(expected, nFruits);
            }
        }
    }
}
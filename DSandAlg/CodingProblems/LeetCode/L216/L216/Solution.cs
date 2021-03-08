#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 216. Combination Sum III
// URL: https://leetcode.com/problems/combination-sum-iii/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            return CombinationSum31(k, n);
        }

        private IList<IList<int>> CombinationSum31(int k, int n)
        {
            // SS: DFS with backtracking
            // runtime complexity: O(9 * 8 * 7 * 6 * 5 * 4 * 3 * 2), worst-cast, pessimistic
            // space complexity: O(9)
            
            var result = new List<IList<int>>();

            if (n > 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9)
            {
                return result;
            }
            
            void Backtracking(int startValue, int localK, int localN, List<int> localResult)
            {
                // SS: base cases
                if (localK == k && localN == n)
                {
                    result.Add(new List<int>(localResult));
                    return;
                }

                if (localK >= k || localN >= n || startValue > 9)
                {
                    return;
                }

                // SS: transition
                for (var i = startValue; i <= 9; i++)
                {
                    localResult.Add(i);
                    Backtracking(i + 1, localK + 1, localN + i, localResult);
                    localResult.RemoveAt(localResult.Count - 1);
                }
            }

            Backtracking(1, 0, 0, new List<int>());
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum3(3, 7);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 2, 4}}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum3(3, 9);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 2, 6}, new[] {1, 3, 5}, new[] {2, 3, 4}}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum3(4, 1);

                // Assert
                Assert.IsEmpty(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum3(3, 2);

                // Assert
                Assert.IsEmpty(result);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum3(9, 45);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 2, 3, 4, 5, 6, 7, 8, 9}}, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum3(4, 21);

                // Assert
                CollectionAssert.AreEquivalent(new[] { new[]{1,3,8,9}, new[]{1,4,7,9}, new[]{1,5,6,9}, new[]{1,5,7,8}, new []{2,3,7,9}, new[]{2,4,6,9}, new[]{2,4,7,8}, new[]{2,5,6,8}, new[]{3,4,5,9}, new[]{3,4,6,8}, new[]{3,5,6,7}}, result);
            }
            
        }
    }
}
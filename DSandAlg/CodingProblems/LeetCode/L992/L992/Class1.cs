using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

// 992. Subarrays with K Different Integers
// https://leetcode.com/problems/subarrays-with-k-different-integers/ 

namespace L992
{
    public class Solution
    {
        private static void AddElement(int value, IDictionary<int, int> map)
        {
            if (map.ContainsKey(value))
            {
                map[value]++;
            }
            else
            {
                map[value] = 1;
            }
        }

        private static void RemoveElement(int value, IDictionary<int, int> map)
        {
            if (map.TryGetValue(value, out int freq))
            {
                if (freq == 1)
                {
                    map.Remove(value);
                }
                else
                {
                    map[value]--;
                }
            }
        }

        public int SubarraysWithKDistinct(int[] A, int K)
        {
            if (A.Length < K || K == 0)
            {
                return 0;
            }
            
            var ijkMap = new Dictionary<int, int>();
            var jkMap = new Dictionary<int, int>();

            int i = 0;
            int j = 0;
            int k = 0;

            int count = 0;
            
            while (k <= A.Length)
            {
                if (ijkMap.Count < K)
                {
                    int v = A[k];
                    AddElement(v, ijkMap);
                    AddElement(v, jkMap);

                    k++;
                }
                else if (ijkMap.Count > K)
                {
                    if (i < j)
                    {
                        int v = A[i];
                        RemoveElement(v, ijkMap);
                        i++;
                    }
                    else
                    {
                        int v = A[i];
                        RemoveElement(v, ijkMap);
                        RemoveElement(v, jkMap);
                        i++;
                        j++;
                    }
                }
                else
                {
                    Debug.Assert(ijkMap.Count == K);

                    int n = j - i;
                    count += n;
                    
                    if (jkMap.Count == K)
                    {
                        count++;
                    }

                    // move sliding window
                    // slide window of width K by one to the right, if we haven't reached
                    // the end of the array yet
                    if (k == A.Length)
                    {
                        break;
                    }

                    // extend ijk window to right
                    int v = A[k];

                    // grow window
                    k++;

                    AddElement(v, ijkMap);
                    AddElement(v, jkMap);
                    if (ijkMap.Count > K)
                    {
                        // window contains more than K elements, need to shrink
                        continue;
                    }

                    // remove element at position j
                    v = A[j];
                    RemoveElement(v, jkMap);
                    j++;
                        
                    Debug.Assert(k == j + K);
                }
            }
            
            return count;
        }
        
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            int[] A = new[] {2, 1, 2, 1, 3};
            int k = 2;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(7, result);
        }
        
        [Test]
        public void Test2()
        {
            // Arrange
            int[] A = new[] {2, 1, 1, 2, 3};
            int k = 2;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            int[] A = new[] {2, 1, 1, 2, 3};
            int k = 3;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            int[] A = new[] {1, 2, 1, 3, 4};
            int k = 3;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(3, result);
        }

    }
}
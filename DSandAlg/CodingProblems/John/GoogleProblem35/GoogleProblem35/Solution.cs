#region

using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

#endregion

namespace GoogleProblem35
{
    public class Solution
    {
        public IList<((int u1, int v1), (int u2, int v2))> FindIntersection(IList<int> l1, IList<int> l2)
        {
            // SS: Given two lists of sorted integers, find their intersection.
            var results = new List<((int u1, int v1), (int u2, int v2))>();

            var u1 = 0;
            var u2 = 0;

            var max1 = l1.Count;
            var max2 = l2.Count;
            while (u1 < max1 && u2 < max2)
            {
                if (l1[u1] < l2[u2])
                {
                    u1++;
                    continue;
                }

                if (l1[u1] > l2[u2])
                {
                    u2++;
                    continue;
                }

                Debug.Assert(l1[u1] == l2[u2]);

                var v1 = u1 + 1;
                var v2 = u2 + 1;

                while (v1 < max1 && v2 < max2 && l1[v1] == l2[v2])
                {
                    v1++;
                    v2++;
                }

                results.Add(((u1, v1), (u2, v2)));

                u1 = v1;
                u2 = v2;
            }

            return results;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var l1 = new List<int> {1, 2, 5, 9, 13, 19, 21, 24, 27, 30};
                var l2 = new List<int> {5, 9, 13, 17, 21, 24, 27, 31};

                // Act
                var ranges = new Solution().FindIntersection(l1, l2);

                // Assert
                Assert.AreEqual(2, ranges.Count);
                Assert.AreEqual(((2, 5), (0, 3)), ranges[0]);
                Assert.AreEqual(((6, 9), (4, 7)), ranges[1]);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var l1 = new List<int>();
                var l2 = new List<int> {5, 9, 13, 17, 21, 24, 27, 31};

                // Act
                var ranges = new Solution().FindIntersection(l1, l2);

                // Assert
                Assert.AreEqual(0, ranges.Count);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var l1 = new List<int> {1, 2, 5, 9, 13, 19, 21, 24, 27, 30};
                var l2 = new List<int>();

                // Act
                var ranges = new Solution().FindIntersection(l1, l2);

                // Assert
                Assert.AreEqual(0, ranges.Count);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var l1 = new List<int> {1, 2, 5, 9, 13, 19, 21, 24, 27, 30};
                var l2 = new List<int> {5, 9, 13, 17, 21, 24, 27, 30};

                // Act
                var ranges = new Solution().FindIntersection(l1, l2);

                // Assert
                Assert.AreEqual(2, ranges.Count);
                Assert.AreEqual(((2, 5), (0, 3)), ranges[0]);
                Assert.AreEqual(((6, l1.Count), (4, l2.Count)), ranges[1]);
            }
        }
    }
}
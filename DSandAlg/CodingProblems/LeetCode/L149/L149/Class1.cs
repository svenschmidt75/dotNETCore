#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 149. Max Points on a Line
// https://leetcode.com/problems/max-points-on-a-line/

namespace L149
{
    public class Solution
    {
        public int MaxPoints(int[][] points)
        {
            if (points.Length == 1)
            {
                return 1;
            }

            // SS: map pairs to (slope, intercept)
            var regularPoints = new Dictionary<(int, int), List<Line>>();
            var duplicatePoints = new Dictionary<(int x, int y), HashSet<int>>();
            var verticalLines = new Dictionary<double, HashSet<int>>();

            var maxPoints = 0;

            for (var i = 0; i < points.Length; i++)
            {
                var p1 = points[i];

                for (var j = i + 1; j < points.Length; j++)
                {
                    var p2 = points[j];

                    var a1 = p2[1] - p1[1];
                    var a2 = p2[0] - p1[0];

                    // duplicate points?
                    if (a1 == 0 && a2 == 0)
                    {
                        var key = (p1[0], p1[1]);
                        if (duplicatePoints.ContainsKey(key))
                        {
                            duplicatePoints[key].Add(i);
                            duplicatePoints[key].Add(j);
                            maxPoints = Math.Max(maxPoints, duplicatePoints[key].Count);
                        }
                        else
                        {
                            duplicatePoints[key] = new HashSet<int> {i, j};
                            maxPoints = Math.Max(maxPoints, 2);
                        }

                        continue;
                    }

                    // inf slope, points on vertical line?
                    if (a2 == 0)
                    {
                        if (verticalLines.ContainsKey(p1[0]))
                        {
                            verticalLines[p1[0]].Add(i);
                            verticalLines[p1[0]].Add(j);
                            maxPoints = Math.Max(maxPoints, verticalLines[p1[0]].Count);
                        }
                        else
                        {
                            verticalLines[p1[0]] = new HashSet<int> {i, j};
                            maxPoints = Math.Max(maxPoints, 2);
                        }

                        continue;
                    }

                    // non-degenerate points
                    var gcd = Gcd(a1, a2);
                    a1 /= gcd;
                    a2 /= gcd;
                    if (regularPoints.TryGetValue((a1, a2), out var lines))
                    {
                        // check all lines with the same slope
                        var k = 0;
                        while (k < lines.Count)
                        {
                            var line = lines[k];
                            var startP = points[line.Points.First()];
                            var d = Math.Abs(a2 * (startP[1] - p2[1]) - a1 * (startP[0] - p2[0]));
                            if (d < 1E-10)
                            {
                                // points on same line
                                line.Points.Add(i);
                                line.Points.Add(j);

                                maxPoints = Math.Max(maxPoints, line.Points.Count);

                                break;
                            }

                            k++;
                        }

                        // found line?
                        if (k == lines.Count)
                        {
                            // line not found
                            var line = new Line
                            {
                                a1 = a1
                                , a2 = a2
                                , Points = new HashSet<int> {i, j}
                            };
                            lines.Add(line);

                            maxPoints = Math.Max(maxPoints, 2);
                        }
                    }
                    else
                    {
                        lines = new List<Line>
                        {
                            new Line
                            {
                                a1 = a1
                                , a2 = a2
                                , Points = new HashSet<int>
                                {
                                    i, j
                                }
                            }
                        };
                        regularPoints[(a1, a2)] = lines;

                        maxPoints = Math.Max(maxPoints, 2);
                    }
                }
            }

            return maxPoints;
        }

        private static int Gcd(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return a == 0 ? b : a;
            }

            var r1 = Math.Min(a, b);
            var r2 = Math.Max(a, b);

            while (r1 != 0)
            {
                var r3 = r2 % r1;
                r2 = r1;
                r1 = r3;
            }

            return r2;
        }

        private struct Line
        {
            public int a1 { get; set; }
            public int a2 { get; set; }
            public HashSet<int> Points { get; set; }
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var points = new[] {new[] {1, 1}, new[] {2, 2}, new[] {3, 3}};

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(3, n);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var points = new[] {new[] {1, 1}, new[] {3, 2}, new[] {5, 3}, new[] {4, 1}, new[] {2, 3}, new[] {1, 4}};

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(4, n);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var points = new[] {new[] {0, 0}, new[] {0, 0}};

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(2, n);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var points = new[]
            {
                new[] {40, -23}, new[] {9, 138}, new[] {429, 115}, new[] {50, -17}, new[] {-3, 80}, new[] {-10, 33}
                , new[] {5, -21}, new[] {-3, 80}, new[] {-6, -65}, new[] {-18, 26}, new[] {-6, -65}, new[] {5, 72}
                , new[] {0, 77}, new[] {-9, 86}, new[] {10, -2}, new[] {-8, 85}, new[] {21, 130}, new[] {18, -6}
                , new[] {-18, 26}, new[] {-1, -15}, new[] {10, -2}, new[] {8, 69}, new[] {-4, 63}, new[] {0, 3}
                , new[] {-4, 40}, new[] {-7, 84}, new[] {-8, 7}, new[] {30, 154}, new[] {16, -5}, new[] {6, 90}
                , new[] {18, -6}, new[] {5, 77}, new[] {-4, 77}, new[] {7, -13}, new[] {-1, -45}, new[] {16, -5}
                , new[] {-9, 86}, new[] {-16, 11}, new[] {-7, 84}, new[] {1, 76}, new[] {3, 77}, new[] {10, 67}
                , new[] {1, -37}, new[] {-10, -81}, new[] {4, -11}, new[] {-20, 13}, new[] {-10, 77}, new[] {6, -17}
                , new[] {-27, 2}, new[] {-10, -81}, new[] {10, -1}, new[] {-9, 1}, new[] {-8, 43}, new[] {2, 2}
                , new[] {2, -21}, new[] {3, 82}, new[] {8, -1}, new[] {10, -1}, new[] {-9, 1}, new[] {-12, 42}
                , new[] {16, -5}, new[] {-5, -61}, new[] {20, -7}, new[] {9, -35}, new[] {10, 6}, new[] {12, 106}
                , new[] {5, -21}, new[] {-5, 82}, new[] {6, 71}, new[] {-15, 34}, new[] {-10, 87}, new[] {-14, -12}
                , new[] {12, 106}, new[] {-5, 82}, new[] {-46, -45}, new[] {-4, 63}, new[] {16, -5}, new[] {4, 1}
                , new[] {-3, -53}, new[] {0, -17}, new[] {9, 98}, new[] {-18, 26}, new[] {-9, 86}, new[] {2, 77}
                , new[] {-2, -49}, new[] {1, 76}, new[] {-3, -38}, new[] {-8, 7}, new[] {-17, -37}, new[] {5, 72}
                , new[] {10, -37}, new[] {-4, -57}, new[] {-3, -53}, new[] {3, 74}, new[] {-3, -11}, new[] {-8, 7}
                , new[] {1, 88}, new[] {-12, 42}, new[] {1, -37}, new[] {2, 77}, new[] {-6, 77}, new[] {5, 72}
                , new[] {-4, -57}, new[] {-18, -33}, new[] {-12, 42}, new[] {-9, 86}, new[] {2, 77}, new[] {-8, 77}
                , new[] {-3, 77}, new[] {9, -42}, new[] {16, 41}, new[] {-29, -37}, new[] {0, -41}, new[] {-21, 18}
                , new[] {-27, -34}, new[] {0, 77}, new[] {3, 74}, new[] {-7, -69}, new[] {-21, 18}, new[] {27, 146}
                , new[] {-20, 13}, new[] {21, 130}, new[] {-6, -65}, new[] {14, -4}, new[] {0, 3}, new[] {9, -5}
                , new[] {6, -29}, new[] {-2, 73}, new[] {-1, -15}, new[] {1, 76}, new[] {-4, 77}, new[] {6, -29}
            };

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(25, n);
        }

        [Test]
        public void Test5()
        {
            // Arrange
            var points = new[] {new[] {0, 0}, new[] {0, 0}, new[] {1, 1}};

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(3, n);
        }

        [Test]
        public void Test6()
        {
            // Arrange
            var points = new[]
            {
                new[] {560, 248}, new[] {0, 16}, new[] {30, 250}, new[] {950, 187}, new[] {630, 277}, new[] {950, 187}
                , new[] {-212, -268}, new[] {-287, -222}, new[] {53, 37}, new[] {-280, -100}, new[] {-1, -14}
                , new[] {-5, 4}, new[] {-35, -387}, new[] {-95, 11}, new[] {-70, -13}, new[] {-700, -274}
                , new[] {-95, 11}, new[] {-2, -33}, new[] {3, 62}, new[] {-4, -47}, new[] {106, 98}, new[] {-7, -65}
                , new[] {-8, -71}, new[] {-8, -147}, new[] {5, 5}, new[] {-5, -90}, new[] {-420, -158}
                , new[] {-420, -158}, new[] {-350, -129}, new[] {-475, -53}, new[] {-4, -47}, new[] {-380, -37}
                , new[] {0, -24}, new[] {35, 299}, new[] {-8, -71}, new[] {-2, -6}, new[] {8, 25}, new[] {6, 13}
                , new[] {-106, -146}, new[] {53, 37}, new[] {-7, -128}, new[] {-5, -1}, new[] {-318, -390}
                , new[] {-15, -191}, new[] {-665, -85}, new[] {318, 342}, new[] {7, 138}, new[] {-570, -69}
                , new[] {-9, -4}, new[] {0, -9}, new[] {1, -7}, new[] {-51, 23}, new[] {4, 1}, new[] {-7, 5}
                , new[] {-280, -100}, new[] {700, 306}, new[] {0, -23}, new[] {-7, -4}, new[] {-246, -184}
                , new[] {350, 161}, new[] {-424, -512}, new[] {35, 299}, new[] {0, -24}, new[] {-140, -42}
                , new[] {-760, -101}, new[] {-9, -9}, new[] {140, 74}, new[] {-285, -21}, new[] {-350, -129}
                , new[] {-6, 9}, new[] {-630, -245}, new[] {700, 306}, new[] {1, -17}, new[] {0, 16}, new[] {-70, -13}
                , new[] {1, 24}, new[] {-328, -260}, new[] {-34, 26}, new[] {7, -5}, new[] {-371, -451}
                , new[] {-570, -69}, new[] {0, 27}, new[] {-7, -65}, new[] {-9, -166}, new[] {-475, -53}
                , new[] {-68, 20}, new[] {210, 103}, new[] {700, 306}, new[] {7, -6}, new[] {-3, -52}
                , new[] {-106, -146}, new[] {560, 248}, new[] {10, 6}, new[] {6, 119}, new[] {0, 2}, new[] {-41, 6}
                , new[] {7, 19}, new[] {30, 250}
            };

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(22, n);
        }

        [Test]
        public void Test7()
        {
            // Arrange
            var points = new[]
            {
                new[] {0, 0}, new[] {94911151, 94911150}, new[] {94911152, 94911151}
            };

            // Act
            var n = new Solution().MaxPoints(points);

            // Assert
            Assert.AreEqual(2, n);
        }
    }
}
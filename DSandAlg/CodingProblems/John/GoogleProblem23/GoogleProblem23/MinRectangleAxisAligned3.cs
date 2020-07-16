#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem23
{
    public class MinRectangleAxisAligned3
    {
        public int MinAreaRect(int[][] points)
        {
            var graph = new Graph();
            for (var i = 0; i < points.Length; i++)
            {
                graph.AddVertex(i);
            }

            // SS: add connections
            var nConnections = 0;
            for (var i = 0; i < points.Length; i++)
            {
                var p1 = points[i];

                for (var j = i + 1; j < points.Length; j++)
                {
                    var p2 = points[j];

                    if (OnSameDiagonal(p1, p2))
                    {
                        graph.AddUndirectedEdge(i, j);
                        nConnections++;
                    }
                }
            }

            var minArea = int.MaxValue;
            var visited = new HashSet<int>();
            for (var vertex = 0; vertex < points.Length; vertex++)
            {
                visited.Add(vertex);
                var area = DFS(0, vertex, graph, visited, new int[4], points);
                visited.Remove(vertex);
                minArea = Math.Min(minArea, area);
            }

            if (minArea == int.MaxValue)
            {
                // SS: no rectangle found
                minArea = 0;
            }

            return minArea;
        }

        private static bool OnSameDiagonal(int[] p1, int[] p2)
        {
            var dx = p2[0] - p1[0];
            var dy = p2[1] - p1[1];
            return dx == dy || dx == 0 || dy == 0;
        }

        private static int DFS(int n, int vertex, Graph graph, HashSet<int> visited, int[] rect, int[][] points)
        {
            rect[n] = vertex;

            if (n > 1)
            {
                // SS: check that points form right angles, to filter out invalid connections
                var p0 = points[rect[n - 2]];
                var p1 = points[rect[n - 1]];
                var p2 = points[rect[n]];

                var p01x = -p0[0] + p1[0];
                var p01y = -p0[1] + p1[1];

                var p12x = -p1[0] + p2[0];
                var p12y = -p1[1] + p2[1];

                var dot = p01x * p12x + p01y * p12y;
                if (dot != 0)
                {
                    return int.MaxValue;
                }
            }

            if (n == 3)
            {
                return RectangleArea(rect, points);
            }

            var minArea = int.MaxValue;

            var neighbors = graph.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                visited.Add(neighbor);

                var area = DFS(n + 1, neighbor, graph, visited, rect, points);
                minArea = Math.Min(minArea, area);

                visited.Remove(neighbor);
            }

            return minArea;
        }

        private static int RectangleArea(int[] rect, int[][] points)
        {
            var p0 = points[rect[0]];
            var p1 = points[rect[1]];
            var p2 = points[rect[2]];
            var p3 = points[rect[3]];

            var p01x = -p0[0] + p1[0];
            var p01y = -p0[1] + p1[1];

            var p23x = -p2[0] + p3[0];
            var p23y = -p2[1] + p3[1];

            var p30x = -p3[0] + p0[0];
            var p30y = -p3[1] + p0[1];

            // SS: check whether edges (0, 1) and (3, 0) form a right angle... 
            var dot = p30x * p01x + p30y * p01y;
            if (dot != 0)
            {
                return int.MaxValue;
            }

            var area = p23x * p30y - p23y * p30x;
            if (area <= 0)
            {
                return int.MaxValue;
            }

            return area;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var points = new[]
                {
                    new[] {1, 1}, new[] {1, 4}, new[] {2, 2}, new[] {3, 1}, new[] {3, 3}, new[] {3, 4}, new[] {4, 1}
                    , new[] {4, 3}, new[] {4, 4}
                };

                // Act
                var minArea = new MinRectangleAxisAligned3().MinAreaRect(points);

                // Assert
                Assert.AreEqual(1, minArea);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var points = new[]
                {
                    new[] {1, 1}, new[] {1, 3}, new[] {2, 2}, new[] {3, 1}, new[] {3, 3}
                };

                // Act
                var minArea = new MinRectangleAxisAligned3().MinAreaRect(points);

                // Assert
                Assert.AreEqual(4, minArea);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var points = new[]
                {
                    new[] {1, 1}, new[] {1, 3}, new[] {3, 1}, new[] {3, 3}, new[] {4, 1}, new[] {4, 3}
                };

                // Act
                var minArea = new MinRectangleAxisAligned3().MinAreaRect(points);

                // Assert
                Assert.AreEqual(2, minArea);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var points = new[]
                {
                    new[] {3, 2}, new[] {3, 1}, new[] {4, 4}, new[] {1, 1}, new[] {4, 3}, new[] {0, 3}, new[] {0, 2}
                    , new[] {4, 0}
                };

                // Act
                var minArea = new MinRectangleAxisAligned3().MinAreaRect(points);

                // Assert
                Assert.AreEqual(0, minArea);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var points = new[]
                {
                    new[] {0, 0}, new[] {3, 3}, new[] {5, 2}, new[] {5, 5}, new[] {1, 4}, new[] {1, 1}, new[] {2, 0}
                    , new[] {0, 5}, new[] {4, 3}, new[] {2, 4}, new[] {1, 0}, new[] {5, 3}, new[] {4, 1}, new[] {0, 2}
                    , new[] {4, 0}
                };

                // Act
                var minArea = new MinRectangleAxisAligned3().MinAreaRect(points);

                // Assert
                Assert.AreEqual(3, minArea);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var points = new[]
                {
                    new[] {33489, 35488}, new[] {35453, 25333}, new[] {33489, 13087}, new[] {35453, 28054}
                    , new[] {33489, 3192}, new[] {35453, 32718}, new[] {33489, 34158}, new[] {33489, 28804}
                    , new[] {33489, 31324}, new[] {33489, 10279}, new[] {35453, 24627}, new[] {33489, 16560}
                    , new[] {33489, 15995}, new[] {33489, 32718}, new[] {35453, 9172}, new[] {35453, 11474}
                    , new[] {35453, 24648}, new[] {33489, 8839}, new[] {33489, 6218}, new[] {35453, 32181}
                    , new[] {35453, 21472}, new[] {33489, 19329}, new[] {33489, 11187}, new[] {33489, 14444}
                    , new[] {35453, 16560}, new[] {35453, 5883}, new[] {35453, 3442}, new[] {35453, 30089}
                    , new[] {35453, 18602}, new[] {33489, 33912}, new[] {33489, 6673}, new[] {35453, 14113}
                    , new[] {35453, 38039}, new[] {35453, 24035}, new[] {35453, 28804}, new[] {35453, 36166}
                    , new[] {33489, 26912}, new[] {33489, 3819}, new[] {33489, 25556}, new[] {33489, 21240}
                    , new[] {35453, 11106}, new[] {33489, 33396}, new[] {33489, 17271}, new[] {35453, 21497}
                    , new[] {35453, 20923}, new[] {35453, 8839}, new[] {33489, 36166}, new[] {33489, 6717}
                    , new[] {35453, 20212}, new[] {35453, 5231}, new[] {35453, 14741}, new[] {35453, 12687}
                    , new[] {35453, 35547}, new[] {33489, 3442}, new[] {35453, 26312}, new[] {35453, 1812}
                    , new[] {33489, 22809}, new[] {35453, 9112}, new[] {35453, 22405}, new[] {35453, 7035}
                    , new[] {33489, 22482}, new[] {33489, 37334}, new[] {35453, 23418}, new[] {33489, 1688}
                    , new[] {33489, 21497}, new[] {33489, 8877}, new[] {33489, 13883}, new[] {35453, 38389}
                    , new[] {35453, 363}, new[] {35453, 39062}, new[] {33489, 2649}, new[] {35453, 9347}
                    , new[] {35453, 13883}, new[] {35453, 1481}, new[] {35453, 39796}, new[] {33489, 7234}
                    , new[] {33489, 39331}, new[] {35453, 39790}, new[] {33489, 7035}, new[] {35453, 22482}
                    , new[] {35453, 15645}, new[] {35453, 20073}, new[] {35453, 26805}, new[] {35453, 7211}
                    , new[] {35453, 33366}, new[] {33489, 39608}, new[] {35453, 1186}, new[] {33489, 10490}
                    , new[] {33489, 3547}, new[] {35453, 507}, new[] {33489, 7677}, new[] {33489, 15521}
                    , new[] {35453, 32512}, new[] {33489, 10303}, new[] {33489, 33154}, new[] {35453, 14444}
                    , new[] {33489, 20089}, new[] {35453, 39608}, new[] {35453, 19316}, new[] {33489, 32050}
                    , new[] {33489, 24648}, new[] {35453, 15207}, new[] {33489, 20679}, new[] {33489, 11914}
                    , new[] {33489, 29908}, new[] {33489, 2265}, new[] {35453, 18691}, new[] {35453, 22864}
                    , new[] {35453, 15396}, new[] {33489, 33104}, new[] {33489, 19932}, new[] {35453, 39497}
                    , new[] {33489, 25529}, new[] {35453, 3192}, new[] {33489, 507}, new[] {35453, 3547}
                    , new[] {35453, 8966}, new[] {33489, 413}, new[] {35453, 4779}, new[] {35453, 2439}
                    , new[] {35453, 1688}
                    , new[] {35453, 15995}, new[] {35453, 8158}, new[] {33489, 3979}, new[] {33489, 15137}
                    , new[] {33489, 33012}, new[] {35453, 9163}, new[] {33489, 21506}, new[] {33489, 9112}
                    , new[] {33489, 33539}, new[] {35453, 12866}, new[] {35453, 8589}, new[] {35453, 33104}
                    , new[] {33489, 26312}, new[] {33489, 22688}, new[] {35453, 13087}, new[] {33489, 35668}
                    , new[] {33489, 39199}, new[] {35453, 9678}, new[] {33489, 39790}, new[] {35453, 5000}
                    , new[] {35453, 6753}, new[] {33489, 34008}, new[] {33489, 9785}, new[] {35453, 21240}
                    , new[] {33489, 9678}, new[] {35453, 603}, new[] {35453, 33912}, new[] {35453, 14816}
                    , new[] {33489, 4597}, new[] {35453, 39879}, new[] {33489, 25302}, new[] {35453, 1312}
                    , new[] {35453, 9785}, new[] {35453, 38139}, new[] {33489, 28851}, new[] {33489, 15207}
                    , new[] {33489, 30580}, new[] {35453, 6134}, new[] {35453, 21143}, new[] {33489, 14589}
                    , new[] {35453, 4398}, new[] {33489, 20923}, new[] {33489, 22405}, new[] {35453, 27613}
                    , new[] {35453, 28851}, new[] {33489, 4564}, new[] {35453, 7745}, new[] {35453, 7564}
                    , new[] {35453, 34229}, new[] {33489, 29391}, new[] {33489, 12579}, new[] {33489, 14749}
                    , new[] {35453, 1338}, new[] {35453, 20125}, new[] {33489, 2492}, new[] {35453, 23178}
                    , new[] {33489, 9452}, new[] {35453, 29391}, new[] {33489, 39497}, new[] {35453, 24267}
                    , new[] {33489, 17560}, new[] {35453, 20815}, new[] {35453, 10490}, new[] {35453, 24252}
                    , new[] {35453, 14429}, new[] {35453, 26912}, new[] {35453, 7694}, new[] {33489, 2253}
                    , new[] {35453, 20089}, new[] {33489, 12496}, new[] {33489, 20815}, new[] {33489, 24691}
                    , new[] {35453, 32959}, new[] {33489, 12866}, new[] {33489, 1312}, new[] {33489, 24627}
                    , new[] {33489, 27912}, new[] {33489, 20073}, new[] {33489, 37268}, new[] {35453, 6230}
                    , new[] {33489, 25333}, new[] {35453, 17271}, new[] {33489, 9172}, new[] {35453, 35129}
                    , new[] {35453, 20679}, new[] {33489, 8966}, new[] {35453, 2649}, new[] {35453, 8877}
                    , new[] {33489, 30089}, new[] {35453, 39859}, new[] {33489, 11528}, new[] {35453, 9707}
                    , new[] {33489, 8589}, new[] {33489, 5000}, new[] {35453, 2590}, new[] {35453, 1267}
                    , new[] {33489, 36760}, new[] {33489, 7604}, new[] {35453, 11187}, new[] {35453, 6563}
                    , new[] {35453, 21785}, new[] {35453, 6406}, new[] {33489, 13192}, new[] {35453, 413}
                    , new[] {33489, 31531}, new[] {33489, 7745}, new[] {35453, 20832}, new[] {33489, 33141}
                    , new[] {35453, 25079}, new[] {33489, 33366}, new[] {35453, 17560}, new[] {35453, 29379}
                    , new[] {33489, 11986}, new[] {33489, 22579}, new[] {35453, 6717}, new[] {35453, 2152}
                    , new[] {33489, 38645}, new[] {33489, 18602}, new[] {33489, 20212}, new[] {35453, 9889}
                    , new[] {35453, 9452}, new[] {33489, 15396}, new[] {35453, 2265}, new[] {35453, 2492}
                    , new[] {33489, 23187}, new[] {33489, 34918}, new[] {35453, 22579}, new[] {35453, 25556}
                    , new[] {35453, 39199}, new[] {35453, 38645}, new[] {33489, 31060}, new[] {35453, 4033}
                    , new[] {35453, 36786}, new[] {35453, 7677}, new[] {33489, 4352}, new[] {33489, 24267}
                    , new[] {33489, 6753}, new[] {33489, 7564}, new[] {33489, 21472}, new[] {33489, 2819}
                    , new[] {35453, 3333}, new[] {33489, 9347}, new[] {33489, 38139}, new[] {33489, 1481}
                    , new[] {35453, 12008}, new[] {35453, 15137}, new[] {33489, 24035}, new[] {33489, 38039}
                    , new[] {33489, 10159}, new[] {33489, 39879}, new[] {33489, 25888}, new[] {35453, 35488}
                    , new[] {33489, 11106}, new[] {33489, 33077}, new[] {33489, 5880}, new[] {35453, 1709}
                    , new[] {35453, 34008}, new[] {35453, 37379}, new[] {33489, 1338}, new[] {33489, 6224}
                    , new[] {33489, 32521}, new[] {35453, 10303}, new[] {33489, 37208}, new[] {35453, 33154}
                    , new[] {35453, 14285}, new[] {35453, 22311}, new[] {35453, 24691}, new[] {33489, 7211}
                    , new[] {35453, 14474}, new[] {33489, 1186}, new[] {33489, 21785}, new[] {35453, 16280}
                    , new[] {33489, 14285}, new[] {33489, 4398}, new[] {35453, 37334}, new[] {35453, 23187}
                    , new[] {35453, 7604}, new[] {33489, 13079}, new[] {33489, 1709}, new[] {35453, 33396}
                    , new[] {35453, 3441}, new[] {35453, 36760}, new[] {33489, 363}, new[] {35453, 4597}
                    , new[] {33489, 29379}, new[] {33489, 39062}, new[] {35453, 37208}, new[] {35453, 932}
                    , new[] {33489, 22864}, new[] {35453, 34918}, new[] {33489, 35557}, new[] {35453, 19932}
                    , new[] {35453, 35069}, new[] {33489, 7957}, new[] {33489, 35328}, new[] {33489, 9163}
                    , new[] {35453, 27882}, new[] {35453, 3979}, new[] {33489, 13731}, new[] {33489, 35129}
                    , new[] {35453, 12579}, new[] {35453, 20334}, new[] {35453, 33210}, new[] {35453, 35328}
                    , new[] {33489, 28883}, new[] {35453, 6761}, new[] {35453, 37268}, new[] {33489, 4779}
                    , new[] {33489, 14352}, new[] {33489, 3441}, new[] {33489, 39859}, new[] {33489, 6406}
                    , new[] {33489, 10074}, new[] {35453, 12164}, new[] {35453, 31323}, new[] {33489, 7694}
                    , new[] {35453, 12496}, new[] {35453, 22375}, new[] {33489, 23178}, new[] {33489, 14429}
                    , new[] {35453, 35668}, new[] {35453, 33539}, new[] {35453, 32935}, new[] {35453, 16813}
                    , new[] {35453, 17230}, new[] {35453, 31324}, new[] {33489, 1267}, new[] {33489, 32922}
                    , new[] {33489, 17230}, new[] {35453, 37393}, new[] {35453, 33077}, new[] {33489, 6230}
                    , new[] {35453, 7021}, new[] {33489, 23418}, new[] {35453, 17849}, new[] {35453, 10074}
                    , new[] {33489, 11474}, new[] {33489, 2152}, new[] {33489, 13084}, new[] {33489, 14113}
                    , new[] {35453, 15521}, new[] {33489, 16280}, new[] {33489, 39734}, new[] {33489, 36786}
                    , new[] {33489, 3333}, new[] {35453, 4564}, new[] {33489, 26805}, new[] {33489, 28054}
                    , new[] {33489, 35069}, new[] {35453, 14201}, new[] {33489, 20334}, new[] {35453, 22512}
                    , new[] {35453, 10159}, new[] {33489, 32512}, new[] {35453, 5880}, new[] {33489, 6832}
                    , new[] {33489, 12687}, new[] {35453, 35557}, new[] {35453, 14352}, new[] {35453, 2253}
                    , new[] {33489, 32211}, new[] {35453, 11986}, new[] {33489, 14816}, new[] {35453, 39331}
                    , new[] {35453, 31531}, new[] {33489, 38389}, new[] {33489, 18691}, new[] {35453, 5912}
                    , new[] {33489, 35547}, new[] {35453, 6832}, new[] {33489, 12008}, new[] {33489, 22375}
                    , new[] {33489, 8158}, new[] {33489, 39796}, new[] {35453, 18071}, new[] {33489, 37302}
                    , new[] {33489, 14201}, new[] {33489, 21143}, new[] {33489, 22512}, new[] {33489, 23663}
                    , new[] {33489, 2957}, new[] {35453, 11528}, new[] {35453, 7957}, new[] {33489, 25079}
                    , new[] {35453, 33141}, new[] {35453, 22688}, new[] {35453, 3819}, new[] {33489, 19316}
                    , new[] {35453, 32922}, new[] {35453, 14749}, new[] {33489, 32935}, new[] {35453, 33012}
                    , new[] {33489, 14741}, new[] {33489, 7021}, new[] {33489, 5883}, new[] {33489, 37393}
                    , new[] {33489, 20125}, new[] {33489, 18071}, new[] {35453, 11914}, new[] {33489, 1812}
                    , new[] {33489, 10587}, new[] {35453, 14589}, new[] {35453, 21670}, new[] {35453, 13079}
                    , new[] {33489, 5231}, new[] {35453, 30580}, new[] {35453, 6224}, new[] {35453, 27505}
                    , new[] {35453, 13192}, new[] {33489, 27613}, new[] {35453, 29908}, new[] {35453, 6218}
                    , new[] {35453, 25888}, new[] {33489, 32181}, new[] {33489, 33210}, new[] {33489, 37531}
                    , new[] {35453, 837}, new[] {33489, 12164}, new[] {33489, 603}, new[] {35453, 27912}
                    , new[] {33489, 22311}, new[] {33489, 27882}, new[] {35453, 39734}, new[] {35453, 14508}
                    , new[] {33489, 6563}, new[] {35453, 21506}, new[] {35453, 34158}, new[] {35453, 10587}
                    , new[] {33489, 837}, new[] {33489, 21670}, new[] {33489, 6761}, new[] {33489, 2439}
                    , new[] {33489, 15645}, new[] {33489, 4033}, new[] {35453, 19329}, new[] {33489, 37379}
                    , new[] {33489, 27505}, new[] {33489, 9889}, new[] {35453, 4352}, new[] {33489, 5912}
                    , new[] {33489, 3418}, new[] {33489, 38587}, new[] {33489, 932}, new[] {33489, 14474}
                    , new[] {35453, 31060}, new[] {33489, 9707}, new[] {33489, 14508}, new[] {35453, 2819}
                    , new[] {35453, 35150}, new[] {35453, 23663}, new[] {35453, 38587}, new[] {35453, 6673}
                    , new[] {35453, 32050}, new[] {35453, 28883}, new[] {33489, 2590}, new[] {35453, 37302}
                    , new[] {33489, 6134}, new[] {33489, 17849}, new[] {35453, 13731}, new[] {35453, 22809}
                    , new[] {33489, 35150}, new[] {35453, 10279}, new[] {33489, 24252}, new[] {33489, 20832}
                    , new[] {35453, 25302}, new[] {35453, 25529}, new[] {33489, 31323}, new[] {35453, 3418}
                    , new[] {35453, 13084}, new[] {35453, 32521}, new[] {35453, 32211}, new[] {33489, 32959}
                    , new[] {35453, 7234}, new[] {35453, 2957}, new[] {33489, 34229}, new[] {33489, 16813}
                    , new[] {35453, 37531}
                };

                // Act
                var minArea = new MinRectangleAxisAligned3().MinAreaRect(points);

                // Assert
                Assert.AreEqual(1964, minArea);
            }
        }
    }
}
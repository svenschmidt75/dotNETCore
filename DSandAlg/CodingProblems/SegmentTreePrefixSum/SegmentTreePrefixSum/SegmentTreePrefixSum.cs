#region

using NUnit.Framework;

#endregion

namespace SegmentTreePrefixSum
{
    public class SegmentTreePrefixSum
    {
        public static Node Root { get; set; }

        public static Node Create(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var prefixSumInput = new int[input.Length + 1];
            for (var i = 0; i < input.Length; i++)
            {
                prefixSumInput[i + 1] = input[i];
            }

            // SS: construct tree
            var root = Construct(0, prefixSumInput.Length, prefixSumInput);
            return root;
        }

        private static Node Construct(int min, int max, int[] values)
        {
            // SS: [min, max)
            if (min == max - 1)
            {
                // SS: left node
                return new Node
                {
                    Value = values[min]
                };
            }

            var mid = (min + max + 1) / 2;

            var left = Construct(min, mid, values);
            var right = Construct(mid, max, values);
            var sum = left.Value + right.Value;

            return new Node
            {
                Left = left
                , Right = right
                , Value = sum
            };
        }

        public int QuerySum(int size, int l, int r)
        {
            return QuerySum(Root, 0, size, l, r);
        }

        private static int QuerySum(Node node, int min, int max, int l, int r)
        {
            // SS: [l, r) is the query interval

            // SS: 3 cases
            // 1. partial overlap
            // 2. total overlap
            // 3. no overlap

            // SS: no overlap
            if (r <= min || l >= max)
            {
                // SS: no overlap, return neutral element
                return 0;
            }

            // SS: total overlap?
            if (min >= l && max <= r)
            {
                return node.Value;
            }

            // SS: must be partial overlap
            var mid = (min + max + 1) / 2;
            var leftSum = QuerySum(node.Left, min, mid, l, r);
            var rightSum = QuerySum(node.Right, mid, max, l, r);

            var sum = leftSum + rightSum;
            return sum;
        }

        private static int QueryPrefixSum(Node node, int min, int max, int l, int r)
        {
            // SS: [l, r) is the query interval
            var sum1 = QuerySum(node, min, max, 0, l + 1);
            var sum2 = QuerySum(node, min, max, 0, r + 1);
            var prefixSum = sum2 - sum1;
            return prefixSum;
        }

        public class Node
        {
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] input = {-1, 3, 4, 0, 2, 1};

                // Act
                var root = Create(input);

                //Assert
                Assert.AreEqual(0, root.Left.Left.Left.Value);
                Assert.AreEqual(-1, root.Left.Left.Right.Value);
                Assert.AreEqual(-1, root.Left.Left.Value);

                Assert.AreEqual(3, root.Left.Right.Left.Value);
                Assert.AreEqual(4, root.Left.Right.Right.Value);
                Assert.AreEqual(7, root.Left.Right.Value);

                Assert.AreEqual(6, root.Left.Value);

                Assert.AreEqual(0, root.Right.Left.Left.Value);
                Assert.AreEqual(2, root.Right.Left.Right.Value);
                Assert.AreEqual(2, root.Right.Left.Value);

                Assert.AreEqual(1, root.Right.Right.Value);
                Assert.AreEqual(3, root.Right.Value);

                Assert.AreEqual(9, root.Value);
            }

            [TestCase(0, 4, 6)]
            [TestCase(0, 6, 9)]
            [TestCase(2, 3, 7)]
            [TestCase(2, 4, 7)]
            public void TestSum1(int l, int r, int expectedSum)
            {
                // Arrange
                int[] input = {-1, 3, 4, 0, 2, 1};
                var root = Create(input);

                // Act
                var sum = QuerySum(root, 0, input.Length, l, r);

                //Assert
                Assert.AreEqual(expectedSum, sum);
            }

            [TestCase(0, 5, 9)]
            [TestCase(2, 4, 2)]
            public void TestPrefixSum1(int l, int r, int expectedPrefixSum)
            {
                // Arrange
                int[] input = {-1, 3, 4, 0, 2, 1};
                var root = Create(input);

                // Act
                var sum = QueryPrefixSum(root, 0, input.Length, l, r);

                //Assert
                Assert.AreEqual(expectedPrefixSum, sum);
            }
        }
    }
}
#region

using NUnit.Framework;

#endregion

namespace L938
{
    public class Solution
    {
        public int RangeSumBST(TreeNode root, int L, int R)
        {
            if (root == null)
            {
                return 0;
            }

            var leftSum = RangeSumBST(root.left, L, R);
            var rightSum = RangeSumBST(root.right, L, R);
            var thisSum = root.val < L || root.val > R ? 0 : root.val;

            return leftSum + rightSum + thisSum;
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }


    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var node4 = new Solution.TreeNode(3);
            var node5 = new Solution.TreeNode(7);
            var node2 = new Solution.TreeNode(5, node4, node5);

            var node6 = new Solution.TreeNode(18);
            var node3 = new Solution.TreeNode(15, null, node6);

            var node1 = new Solution.TreeNode(10, node2, node3);

            // Act
            var result = new Solution().RangeSumBST(node1, 7, 15);

            // Assert
            Assert.AreEqual(32, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var node18 = new Solution.TreeNode(18);
            var node13 = new Solution.TreeNode(13);
            var node15 = new Solution.TreeNode(15, node13, node18);
            var node6 = new Solution.TreeNode(6);
            var node7 = new Solution.TreeNode(7, node6);
            var node1 = new Solution.TreeNode(1);
            var node3 = new Solution.TreeNode(3, node1);
            var node5 = new Solution.TreeNode(5, node3, node7);
            var node10 = new Solution.TreeNode(10, node5, node15);

            // Act
            var result = new Solution().RangeSumBST(node10, 6, 10);

            // Assert
            Assert.AreEqual(23, result);
        }
    }
}
#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace IterativePostorderTraversal
{
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


    public class Class1
    {
        public static List<int> RecursivePostorderTraversal(TreeNode node)
        {
            var values = new List<int>();
            RecursivePostorderTraversal(node, values);
            return values;
        }

        private static void RecursivePostorderTraversal(TreeNode node, List<int> values)
        {
            if (node == null)
            {
                return;
            }

            RecursivePostorderTraversal(node.left, values);
            RecursivePostorderTraversal(node.right, values);
            values.Add(node.val);
        }

        public static List<int> IterativePostorderTraversal(TreeNode node)
        {
            var values = new List<int>();

            var stack1 = new Stack<TreeNode>();
            var stack2 = new Stack<TreeNode>();
            var current = node;

            stack1.Push(current);

            while (stack1.Any())
            {
                current = stack1.Pop();
                stack2.Push(current);

                if (current.left != null)
                {
                    stack1.Push(current.left);
                }

                if (current.right != null)
                {
                    stack1.Push(current.right);
                }
            }

            while (stack2.Any())
            {
                current = stack2.Pop();
                values.Add(current.val);
            }

            return values;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var node1 = new TreeNode {val = 1};
            var node2 = new TreeNode {val = -1};
            var node3 = new TreeNode {val = 11};
            var node4 = new TreeNode {val = -2};
            var node5 = new TreeNode {val = -3};
            var node6 = new TreeNode {val = 21};
            var node7 = new TreeNode {val = 6};
            var node8 = new TreeNode {val = 5};

            node5.right = node8;
            node2.left = node4;
            node2.right = node5;
            node3.left = node6;
            node3.right = node7;
            node1.left = node2;
            node1.right = node3;

            // Act
            var values = Class1.IterativePostorderTraversal(node1);

            // Assert
            CollectionAssert.AreEqual(Class1.RecursivePostorderTraversal(node1), values);
        }
    }
}
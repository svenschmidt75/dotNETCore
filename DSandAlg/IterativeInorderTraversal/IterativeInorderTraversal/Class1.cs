#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Iterative Inorder Traversal of Binary Tree
// https://www.youtube.com/watch?v=nzmtCFNae9k&list=PLrmLmBdmIlpv_jNDXtJGYTPNQ2L1gdHxu&index=12

namespace IterativeInorderTraversal
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

        public static TreeNode CreateTree(int[] vals)
        {
            var root = new TreeNode {val = vals[0]};

            var q = new Queue<TreeNode>();
            q.Enqueue(root);

            var i = 1;

            while (q.Any())
            {
                var p = q.Dequeue();

                if (i < vals.Length)
                {
                    var leftVal = vals[i];
                    if (leftVal != int.MaxValue)
                    {
                        var leftChild = new TreeNode {val = leftVal};
                        p.left = leftChild;
                        q.Enqueue(leftChild);
                    }

                    i++;
                }
                else
                {
                    break;
                }

                if (i < vals.Length)
                {
                    var rightVal = vals[i];
                    if (rightVal != int.MaxValue)
                    {
                        var rightChild = new TreeNode {val = rightVal};
                        p.right = rightChild;
                        q.Enqueue(rightChild);
                    }

                    i++;
                }
                else
                {
                    break;
                }
            }

            return root;
        }
    }


    public class Class1
    {
        public static List<int> RecursiveInorderTraversal(TreeNode node)
        {
            var values = new List<int>();
            RecursiveInorderTraversal(node, values);
            return values;
        }

        private static void RecursiveInorderTraversal(TreeNode node, List<int> values)
        {
            if (node == null)
            {
                return;
            }

            RecursiveInorderTraversal(node.left, values);
            values.Add(node.val);
            RecursiveInorderTraversal(node.right, values);
        }

        public static List<int> IterativeInorderTraversal(TreeNode node)
        {
            var values = new List<int>();

            var stack = new Stack<TreeNode>();
            var current = node;

            do
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = current.left;
                }
                else
                {
                    current = stack.Pop();
                    values.Add(current.val);
                    current = current.right;
                }
            } while (stack.Any() || current != null);

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
            var root = TreeNode.CreateTree(new[]
            {
                45, 30, 46, 10, 36, int.MaxValue, 49, 8, 24, 34, 42, 48, int.MaxValue, 4, 9, 14, 25, 31, 35, 41, 43, 47
                , int.MaxValue, 0, 6
                , int.MaxValue, int.MaxValue, 11, 20, int.MaxValue, 28, int.MaxValue, 33, int.MaxValue, int.MaxValue, 37
                , int.MaxValue, int.MaxValue, 44, int.MaxValue, int.MaxValue, int.MaxValue, 1, 5, 7
                , int.MaxValue, 12, 19, 21, 26, 29, 32, int.MaxValue, int.MaxValue, 38, int.MaxValue, int.MaxValue
                , int.MaxValue, 3, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 13
                , 18, int.MaxValue, int.MaxValue, 22, int.MaxValue, 27, int.MaxValue, int.MaxValue, int.MaxValue
                , int.MaxValue, int.MaxValue, 39, 2, int.MaxValue, int.MaxValue, int.MaxValue, 15, int.MaxValue
                , int.MaxValue
                , 23, int.MaxValue, int.MaxValue, int.MaxValue, 40, int.MaxValue, int.MaxValue, int.MaxValue, 16
                , int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 17
            });

            // Act
            var values = Class1.IterativeInorderTraversal(root);

            // Assert
            CollectionAssert.AreEqual(Class1.RecursiveInorderTraversal(root), values);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var root = TreeNode.CreateTree(new[] {5, 3, 6, 2, 4, int.MaxValue, int.MaxValue, 1});

            // Act
            var values = Class1.IterativeInorderTraversal(root);

            // Assert
            CollectionAssert.AreEqual(Class1.RecursiveInorderTraversal(root), values);
        }
    }
}
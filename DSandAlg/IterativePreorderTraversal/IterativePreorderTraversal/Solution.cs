﻿#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace IterativePreorderTraversal
{
    public class Solution
    {
        public IEnumerable<int> IterativePreorderTraversal(TreeNode root)
        {
            var result = new List<int>();

            var stack = new Stack<TreeNode>();
            var node = root;
            while (true)
            {
                while (node != null)
                {
                    result.Add(node.Value);
                    stack.Push(node);
                    node = node.Left;
                }

                if (stack.Any() == false)
                {
                    return result;
                }

                node = stack.Pop();
                node = node.Right;
            }
        }

        public IEnumerable<int> RecursivePreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            RecursivePreorderTraversal(root, result);
            return result;
        }

        private void RecursivePreorderTraversal(TreeNode node, List<int> values)
        {
            if (node == null)
            {
                return;
            }

            values.Add(node.Value);
            RecursivePreorderTraversal(node.Left, values);
            RecursivePreorderTraversal(node.Right, values);
        }

        public class TreeNode
        {
            public int Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var root = new TreeNode
                {
                    Value = 5, Left = new TreeNode
                    {
                        Value = 3
                        , Left = new TreeNode
                        {
                            Value = 2
                            , Left = new TreeNode
                            {
                                Value = 1
                            }
                        }
                        , Right = new TreeNode
                        {
                            Value = 4
                        }
                    }
                    , Right = new TreeNode
                    {
                        Value = 7
                        , Left = new TreeNode
                        {
                            Value = 6
                        }
                        , Right = new TreeNode
                        {
                            Value = 8
                        }
                    }
                };

                // Act
                var result = new Solution().IterativePreorderTraversal(root);
                var expectedResult = new Solution().RecursivePreorderTraversal(root);

                // Assert
                CollectionAssert.AreEqual(expectedResult, result);
            }
        }
    }
}
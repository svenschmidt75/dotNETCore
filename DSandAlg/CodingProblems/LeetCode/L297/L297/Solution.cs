#region

using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

#endregion

// Problem: 297. Serialize and Deserialize Binary Tree
// URL: https://leetcode.com/problems/serialize-and-deserialize-binary-tree/

namespace LeetCode
{
    public class Codec
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            // SS: We traverse the tree in postorder traversal when serializing.
            // This was, when deserializing, we know we have reconstructed the
            // children before their parents.
            // runtime complexity: O(n)
            // space complexity: O(n), for hash table and recursive stack

            var dict = new Dictionary<TreeNode, int>();
            var nodeCounter = 0;

            var builder = new StringBuilder();

            void Deserialize(TreeNode node)
            {
                // SS: base case
                if (node == null)
                {
                    return;
                }

                Deserialize(node.left);
                Deserialize(node.right);

                // SS: write to string
                dict[node] = nodeCounter++;

                var leftChildNodeIdx = node.left != null ? dict[node.left] : -1;
                var rightChildNodeIdx = node.right != null ? dict[node.right] : -1;

                var value = $"{{{nodeCounter - 1} {node.val} {leftChildNodeIdx} {rightChildNodeIdx}}}";
                builder.Append(value);
            }

            Deserialize(root);

            var result = builder.ToString();
            return result;
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            // SS: deserialize
            // runtime complexity: O(n), where n is ~ data.Length
            // space complexity: O(n)

            var dict = new Dictionary<int, TreeNode>();

            TreeNode rootNode = null;

            var pos = 0;
            while (pos < data.Length)
            {
                // SS: expected format {'node index' 'node value' 'left child node index' 'right child node index'}

                // SS: skip {
                pos++;

                // SS: read node index
                var pos2 = pos;
                while (char.IsDigit(data[pos2]))
                {
                    pos2++;
                }

                var nodeIndex = int.Parse(data[pos..pos2]);
                pos = pos2 + 1;

                // SS: read node value
                pos2 = pos;
                while (char.IsDigit(data[pos2]) || data[pos2] == '-')
                {
                    pos2++;
                }

                var nodeValue = int.Parse(data[pos..pos2]);
                pos = pos2 + 1;

                // SS: read left child node index
                pos2 = pos;
                while (char.IsDigit(data[pos2]) || data[pos2] == '-')
                {
                    pos2++;
                }

                var leftIndex = int.Parse(data[pos..pos2]);
                pos = pos2 + 1;

                // SS: read right child node index
                pos2 = pos;
                while (char.IsDigit(data[pos2]) || data[pos2] == '-')
                {
                    pos2++;
                }

                var rightIndex = int.Parse(data[pos..pos2]);
                pos = pos2;

                // SS: skip }
                pos++;

                var node = new TreeNode(nodeValue);
                dict[nodeIndex] = node;

                if (leftIndex > -1)
                {
                    node.left = dict[leftIndex];
                }

                if (rightIndex > -1)
                {
                    node.right = dict[rightIndex];
                }

                // SS: note: In postorder traversal, the root always is the last
                // node to visit, so just assign here.
                rootNode = node;
            }

            return rootNode;
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var root = new TreeNode(1)
                {
                    left = new TreeNode(2)
                    , right = new TreeNode(3)
                    {
                        left = new TreeNode(4)
                        , right = new TreeNode(5)
                    }
                };

                // Act
                var serialized = new Codec().serialize(root);
                var deserialized = new Codec().deserialize(serialized);

                // Assert
                Assert.AreEqual(root.val, deserialized.val);

                Assert.AreEqual(root.left.val, deserialized.left.val);
                Assert.Null(root.left.left);
                Assert.Null(root.left.right);

                Assert.AreEqual(root.right.val, deserialized.right.val);

                Assert.AreEqual(root.right.left.val, deserialized.right.left.val);
                Assert.Null(root.right.left.left);
                Assert.Null(root.right.left.right);

                Assert.AreEqual(root.right.right.val, deserialized.right.right.val);
                Assert.Null(root.right.right.left);
                Assert.Null(root.right.right.right);
            }
        }
    }
}
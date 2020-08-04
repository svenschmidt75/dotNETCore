#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion

namespace GoogleProblem27
{
    public class Solution
    {
        public byte[] SerializeTree(TreeNode root)
        {
            // SS: use default-endianness

            // SS: we do level-order traversal and serialize nodes as such
            // runtime complexity: O(N)
            // space complexity: O(N)

            var byteStream = new List<byte>();

            if (root == null)
            {
                return byteStream.ToArray();
            }

            var map = new Dictionary<string, int>();
            var values = new List<TreeNode>();
            short stringIndex = 0;

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            /* Format:
             *   16 bit: string index for node
             *   16 bit: number of children
             *   16 bit: string index of 1st child
             * ....
             *   16 bit: string index of last child
             */
            while (queue.Any())
            {
                var node = queue.Dequeue();

                if (map.TryGetValue(node.Value, out var idx) == false)
                {
                    map[node.Value] = stringIndex;
                    values.Add(node);
                    idx = stringIndex++;
                }

                var intBytes = BitConverter.GetBytes((short) idx);
                byteStream.AddRange(intBytes);

                if (node.Children != null)
                {
                    intBytes = BitConverter.GetBytes((short) node.Children.Length);
                    byteStream.AddRange(intBytes);

                    for (var i = 0; i < node.Children.Length; i++)
                    {
                        queue.Enqueue(node.Children[i]);
                    }
                }
                else
                {
                    intBytes = BitConverter.GetBytes((short) 0);
                    byteStream.AddRange(intBytes);
                }
            }

            // serialize strings
            for (var i = 0; i < stringIndex; i++)
            {
                var str = values[i].Value;
                var stringBytes = ToBytes(str);
                byteStream.AddRange(stringBytes);
            }

            return byteStream.ToArray();
        }

        public TreeNode DeserializeTree(byte[] bytes)
        {
            // SS: use default-endianness
            if (bytes.Length == 0)
            {
                return null;
            }

            var map = new Dictionary<TreeNode, int>();
            var values = new List<TreeNode>();

            var queue = new Queue<TreeNode>();
            var root = new TreeNode();
            queue.Enqueue(root);

            var streamPosition = 0;
            var maxStringIdx = 0;

            while (queue.Any())
            {
                var node = queue.Dequeue();
                values.Add(node);

                // SS: position of the string payload for this node
                int stringIdx = BitConverter.ToInt16(bytes[streamPosition..(streamPosition + 2)]);
                streamPosition += 2;

                map[node] = stringIdx;

                maxStringIdx = Math.Max(maxStringIdx, stringIdx);

                // SS: number of children this node has
                int nChildren = BitConverter.ToInt16(bytes[streamPosition..(streamPosition + 2)]);
                streamPosition += 2;

                node.Children = new TreeNode[nChildren];
                for (var i = 0; i < nChildren; i++)
                {
                    var child = new TreeNode();
                    node.Children[i] = child;
                    queue.Enqueue(child);
                }
            }

            // SS: deserialize payloads
            var stringMap = new Dictionary<int, string>();
            for (var i = 0; i <= maxStringIdx; i++)
            {
                var payload = FromBytes(bytes, streamPosition);
                streamPosition += payload.Length + 1;
                stringMap[i] = payload;
            }

            for (var i = 0; i < values.Count; i++)
            {
                var node = values[i];
                var payloadIdx = map[node];
                var payload = stringMap[payloadIdx];
                node.Value = payload;
            }

            if (streamPosition != bytes.Length)
            {
                throw new InvalidOperationException("Stream deserialization error");
            }

            return root;
        }

        private static byte[] ToBytes(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var result = new byte[bytes.Length + 1];
            Array.Copy(bytes, result, bytes.Length);
            return result;
        }

        private static string FromBytes(byte[] bytes, int offset)
        {
            var pos = offset;
            while (bytes[pos++] != 0) { }

            var str = Encoding.UTF8.GetString(bytes, offset, pos - offset - 1);
            return str;
        }

        public class TreeNode
        {
            public string Value { get; set; }
            public TreeNode[] Children { get; set; }
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
                    Value = "abc1"
                    , Children = new[]
                    {
                        new TreeNode
                        {
                            Value = "abc2"
                            , Children = new[]
                            {
                                new TreeNode
                                {
                                    Value = "abc5"
                                    , Children = new TreeNode[0]
                                }
                            }
                        }
                        , new TreeNode
                        {
                            Value = "abc3"
                            , Children = new[]
                            {
                                new TreeNode
                                {
                                    Value = "abc6"
                                    , Children = new TreeNode[0]
                                }
                                , new TreeNode
                                {
                                    Value = "abc7"
                                    , Children = new TreeNode[0]
                                }
                            }
                        }
                        , new TreeNode
                        {
                            Value = "abc4"
                            , Children = new TreeNode[0]
                        }
                    }
                };

                // Act
                var stream = new Solution().SerializeTree(root);

                // Assert
                Assert.AreEqual(63, stream.Length);
                var deserializedTree = new Solution().DeserializeTree(stream);
                Assert.True(TreeEqual(root, deserializedTree));
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode
                {
                    Value = "abc1"
                    , Children = new[]
                    {
                        new TreeNode
                        {
                            Value = "abc2"
                            , Children = new[]
                            {
                                new TreeNode
                                {
                                    Value = "abc1"
                                    , Children = new TreeNode[0]
                                }
                            }
                        }
                        , new TreeNode
                        {
                            Value = "abc3"
                            , Children = new[]
                            {
                                new TreeNode
                                {
                                    Value = "abc6"
                                    , Children = new TreeNode[0]
                                }
                                , new TreeNode
                                {
                                    Value = "abc1"
                                    , Children = new TreeNode[0]
                                }
                            }
                        }
                        , new TreeNode
                        {
                            Value = "abc4"
                            , Children = new TreeNode[0]
                        }
                    }
                };

                // Act
                var stream = new Solution().SerializeTree(root);

                // Assert
                Assert.AreEqual(63 - 2 * 5, stream.Length);
                var deserializedTree = new Solution().DeserializeTree(stream);
                Assert.True(TreeEqual(root, deserializedTree));
            }

            private bool TreeEqual(TreeNode node1, TreeNode node2)
            {
                if (node1 == null)
                {
                    return node2 == null;
                }

                if (node1.Value != node2.Value)
                {
                    return false;
                }

                if (node1.Children.Length != node2.Children.Length)
                {
                    return false;
                }

                for (var i = 0; i < node1.Children.Length; i++)
                {
                    if (TreeEqual(node1.Children[i], node2.Children[i]) == false)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GoogleProblem27
{
    public class Solution
    {
        public byte[] SerializeTree(TreeNode root)
        {
            // SS: use default-endianness
            
            var byteStream = new List<byte>();

            if (root == null)
            {
                return byteStream.ToArray();
            }

            var map = new Dictionary<string, int>();
            var values = new List<TreeNode>();
            Int16 stringIndex = 0;
            
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

                if (map.TryGetValue(node.Value, out int idx) == false)
                {
                    map[node.Value] = stringIndex;
                    values.Add(node);
                    idx = stringIndex++;
                }
                
                byte[] intBytes = BitConverter.GetBytes((Int16)idx);
                byteStream.AddRange(intBytes);

                if (node.Children != null)
                {
                    intBytes = BitConverter.GetBytes((Int16)node.Children.Length);
                    byteStream.AddRange(intBytes);

                    for (int i = 0; i < node.Children.Length; i++)
                    {
                        queue.Enqueue(node.Children[i]);
                    }
                }
                else
                {
                    intBytes = BitConverter.GetBytes((Int16)0);
                    byteStream.AddRange(intBytes);
                }
            }

            // serialize strings
            for (int i = 0; i < stringIndex; i++)
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
            }


            return null;
        }

        private byte[] ToBytes(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var result = new byte[bytes.Length + 1];
            Array.Copy(bytes, result, bytes.Length);
            return result;
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
                    , Children = new []
                    {
                        new TreeNode
                        {
                            Value = "abc2"
                            , Children = new []
                            {
                                new TreeNode
                                {
                                    Value = "abc5"
                                }, 
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
                                }
                                , new TreeNode
                                {
                                    Value = "abc7"
                                }
                            }
                        }
                        , new TreeNode
                        {
                            Value = "abc4"
                        }
                    }
                };
                
                // Act
                var stream = new Solution().SerializeTree(root);
                
                // Assert
            }
        }
    }
}
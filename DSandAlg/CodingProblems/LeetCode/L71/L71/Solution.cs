#region

using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion

// Problem: 71. Simplify Path
// URL: https://leetcode.com/problems/simplify-path/

namespace LeetCode
{
    public class Solution
    {
        public string SimplifyPath(string path)
        {
            // SS: runtime complexity: O(path)
            // space complexity: O(path), overestimate

            var stack = new Stack<(int min, int max)>();

            var i = 0;
            while (i < path.Length)
            {
                if (path[i] == '/')
                {
                    i++;
                }
                else if (path[i] == '.')
                {
                    // A folder that starts with . is hidden, so "..hidden"
                    // is a hidden folder with name ".folder"..

                    var k = i;
                    while (k < path.Length && path[k] == '.')
                    {
                        k++;
                    }

                    var pCnt = k - i;

                    var k2 = k;
                    while (k < path.Length && path[k] != '/')
                    {
                        k++;
                    }

                    var cnt = k - k2;

                    if (pCnt == 2 && cnt == 0 && stack.Any())
                    {
                        // SS: go up on level
                        stack.Pop();
                    }
                    else if (pCnt > 2 || cnt > 0)
                    {
                        stack.Push((i, k));
                    }

                    i = k;
                }
                else
                {
                    // SS: must be an folder name
                    var k = i;
                    while (k < path.Length && path[k] != '/')
                    {
                        k++;
                    }

                    stack.Push((i, k));

                    i = k;
                }
            }

            var result = ConstructCanonicalPath(path, stack);
            return result;
        }

        private static string ConstructCanonicalPath(string path, Stack<(int min, int max)> stack)
        {
            var builder = new StringBuilder();

            if (stack.Any() == false)
            {
                builder.Append('/');
            }
            else
            {
                // SS: reverse elements on stack (deque would be good to have)
                var s2 = new Stack<(int min, int max)>();
                while (stack.Any())
                {
                    s2.Push(stack.Pop());
                }

                stack = s2;

                while (stack.Any())
                {
                    builder.Append('/');
                    var (min, max) = stack.Pop();
                    for (var i = min; i < max; i++)
                    {
                        var c = path[i];
                        builder.Append(c);
                    }
                }
            }

            return builder.ToString();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var path = "/home/";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/home", simplified);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var path = "/../";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/", simplified);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var path = "/home//foo/";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/home/foo", simplified);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var path = "/a/./b/../../c/";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/c", simplified);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var path = "/...";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/...", simplified);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                
                var path = "/..hidden";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/..hidden", simplified);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                
                var path = "/f1/f2.f3/../";

                // Act
                var simplified = new Solution().SimplifyPath(path);

                // Assert
                Assert.AreEqual("/f1", simplified);
            }
        }
    }
}
#region

using NUnit.Framework;

#endregion

namespace L109
{
    public class Solution
    {
        public TreeNode SortedListToBST(ListNode head)
        {
            // SS: length of linked list
            var n = 0;
            var current = head;
            while (current != null)
            {
                n++;
                current = current.next;
            }

            if (n == 0)
            {
                return null;
            }

            // SS: linked list to array
            var array = new int[n];
            n = 0;
            current = head;
            while (current != null)
            {
                array[n++] = current.val;
                current = current.next;
            }

            // SS: traverse array and construct from inorder traversal
            var root = Travserse(array, 0, n);
            return root;
        }

        private TreeNode Travserse(int[] array, int min, int max)
        {
            if (min == max)
            {
                return null;
            }

            var mid = (min + max) / 2;
            var node = new TreeNode {val = array[mid]};
            node.left = Travserse(array, min, mid);
            node.right = Travserse(array, mid + 1, max);
            return node;
        }
    }

    public class ListNode
    {
        public ListNode next;
        public int val;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
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

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var node1 = new ListNode
            {
                val = -10
                , next = new ListNode
                {
                    val = -3
                    , next = new ListNode
                    {
                        val = 0
                        , next = new ListNode
                        {
                            val = 5
                            , next = new ListNode
                            {
                                val = 9
                            }
                        }
                    }
                }
            };

            // Act
            var treeRoot = new Solution().SortedListToBST(node1);

            // Assert
            Assert.AreEqual(0, treeRoot.val);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var array = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var root = new ListNode
            {
                val = 1
            };
            var list = root;
            var i = 1;
            while (i < array.Length && list != null)
            {
                list.next = new ListNode
                {
                    val = array[i++]
                };
                list = list.next;
            }

            // Act
            var treeRoot = new Solution().SortedListToBST(root);

            // Assert
            Assert.AreEqual(6, treeRoot.val);
        }
    }
}
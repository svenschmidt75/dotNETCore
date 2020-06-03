using NUnit.Framework;

namespace FindMiddleNodeInLinkedList
{
    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
    }

    public class LinkedList
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }

        public Node FindMiddleNode1()
        {
            // SS: single-pass though list to count number of nodes
            int nNodes = 0;
            var node = Head;
            while (node != null)
            {
                node = node.Next;
                nNodes++;
            }

            if (nNodes == 0)
            {
                return Head;
            }

            var idx = (nNodes + 1) / 2;
            node = Head;
            Node prevNode = null;
            while (idx > 0)
            {
                prevNode = node;
                node = node.Next;
                idx--;
            }

            return prevNode;
        }

        public Node FindMiddleNode2()
        {
            // SS: use two pointers, one stays at the start, the next one points to the current node,
            // and a third one points to the current middle node.
            if (Head == null)
            {
                return null;
            }

            var middleNode = Head;
            int middleIdx = 0;

            var currentNode = Head;
            int currentIdx = 0;

            while (currentNode != null)
            {
                currentNode = currentNode.Next;

                var newMiddleIdx = (currentIdx + 1) / 2;
                if (newMiddleIdx > middleIdx)
                {
                    middleIdx = newMiddleIdx;
                    middleNode = middleNode.Next;
                }

                currentIdx++;
            }

            return middleNode;
        }

        public Node FindMiddleNode3()
        {
            // SS: use two pointers, one advanced by 1, the other every two steps.
            // When we get to the end, the 2nd pointer points to the middle node.
            if (Head == null)
            {
                return null;
            }

            var pointer1 = Head;
            var pointer2 = Head;

            int cnt = 0;
            
            while (pointer1 != null)
            {
                pointer1 = pointer1.Next;
                cnt++;
                if (cnt % 2 == 0)
                {
                    pointer2 = pointer2.Next;
                }
            }

            return pointer2;
        }

    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestOneNode11()
        {
            // Arrange
            var linkedList = new LinkedList();
            linkedList.Head = new Node{Value = 1};
            linkedList.Tail = linkedList.Head;
            
            // Act
            var middleNode = linkedList.FindMiddleNode1();
            
            // Assert
            Assert.AreEqual(1, middleNode.Value);
        }

        [Test]
        public void TestOneNode12()
        {
            // Arrange
            var linkedList = new LinkedList();
            linkedList.Head = new Node{Value = 1};
            linkedList.Tail = linkedList.Head;
            
            // Act
            var middleNode = linkedList.FindMiddleNode2();
            
            // Assert
            Assert.AreEqual(1, middleNode.Value);
        }

        [Test]
        public void TestOneNode13()
        {
            // Arrange
            var linkedList = new LinkedList();
            linkedList.Head = new Node{Value = 1};
            linkedList.Tail = linkedList.Head;
            
            // Act
            var middleNode = linkedList.FindMiddleNode3();
            
            // Assert
            Assert.AreEqual(1, middleNode.Value);
        }

        [Test]
        public void Test21()
        {
            // Arrange
            var linkedList = new LinkedList();

            var node5 = new Node{Value = 5};
            var node4 = new Node{Value = 4, Next = node5};
            var node3 = new Node{Value = 3, Next = node4};
            var node2 = new Node{Value = 2, Next = node3};
            var node1 = new Node{Value = 1, Next = node2};
            
            linkedList.Head = node1;
            linkedList.Tail = node5;
            
            // Act
            var middleNode = linkedList.FindMiddleNode1();
            
            // Assert
            Assert.AreEqual(3, middleNode.Value);
        }
        
        [Test]
        public void Test22()
        {
            // Arrange
            var linkedList = new LinkedList();

            var node5 = new Node{Value = 5};
            var node4 = new Node{Value = 4, Next = node5};
            var node3 = new Node{Value = 3, Next = node4};
            var node2 = new Node{Value = 2, Next = node3};
            var node1 = new Node{Value = 1, Next = node2};
            
            linkedList.Head = node1;
            linkedList.Tail = node5;
            
            // Act
            var middleNode = linkedList.FindMiddleNode2();
            
            // Assert
            Assert.AreEqual(3, middleNode.Value);
        }

        [Test]
        public void Test23()
        {
            // Arrange
            var linkedList = new LinkedList();

            var node5 = new Node{Value = 5};
            var node4 = new Node{Value = 4, Next = node5};
            var node3 = new Node{Value = 3, Next = node4};
            var node2 = new Node{Value = 2, Next = node3};
            var node1 = new Node{Value = 1, Next = node2};
            
            linkedList.Head = node1;
            linkedList.Tail = node5;
            
            // Act
            var middleNode = linkedList.FindMiddleNode3();
            
            // Assert
            Assert.AreEqual(3, middleNode.Value);
        }
        
    }
    
}
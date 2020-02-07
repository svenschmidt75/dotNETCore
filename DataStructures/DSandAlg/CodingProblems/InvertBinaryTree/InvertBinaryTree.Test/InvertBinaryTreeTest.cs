using NUnit.Framework;

namespace InvertBinaryTree.Test
{
    public class InvertBinaryTreeTest
    {
        [Test]
        public void Test()
        {
            // https://leetcode.com/problems/invert-binary-tree/
            
            // Arrange
            var bt = new BinarySearchTree<int>();
            var root = new Node<int> {Value = 4};
            bt.Root = root;

            var l = new Node<int> {Value = 2};
            var r = new Node<int> {Value = 7};
            root.Left = l;
            root.Right = r;

            var ll = new Node<int> {Value = 1};
            var lr = new Node<int> {Value = 3};
            l.Left = ll;
            l.Right = lr;

            var rl = new Node<int> {Value = 6};
            var rr = new Node<int> {Value = 9};
            r.Left = rl;
            r.Right = rr;

            // Act
            var invertedBt = BinarySearchTree<int>.Invert(bt);

            // Assert
            Assert.AreEqual(invertedBt.Root.Value, 4);
            Assert.AreEqual(invertedBt.Root.Left.Value, 7);
            Assert.AreEqual(invertedBt.Root.Right.Value, 2);
            Assert.AreEqual(invertedBt.Root.Left.Left.Value, 9);
            Assert.AreEqual(invertedBt.Root.Left.Right.Value, 6);
            Assert.AreEqual(invertedBt.Root.Right.Left.Value, 3);
            Assert.AreEqual(invertedBt.Root.Right.Right.Value, 1);
        }
    }
}
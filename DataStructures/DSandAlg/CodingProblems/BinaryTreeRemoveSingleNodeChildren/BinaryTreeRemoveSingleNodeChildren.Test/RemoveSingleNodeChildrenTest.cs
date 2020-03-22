using BinaryTree;
using NUnit.Framework;

namespace BinaryTreeRemoveSingleNodeChildren.Test
{
    public class RemoveSingleNodeChildrenTest
    {
        [Test]
        public void Test()
        {
            // https://www.reddit.com/r/CodingProblems/comments/fbw3b2/day_62020031_problem_of_the_day_asked_by_google/
            
            // Arrange
            var bt = new BinaryTree<int>();
            var root = new Node<int> {Value = 1};
            bt.Root = root;

            var l = new Node<int> {Value = 2};
            var r = new Node<int> {Value = 3};
            root.Left = l;
            root.Right = r;

            var ll = new Node<int> {Value = 0};
            l.Left = ll;

            var rl = new Node<int> {Value = 9};
            var rr = new Node<int> {Value = 4};
            r.Left = rl;
            r.Right = rr;

            // Act
            bt.RemoveSingleNodeChildren();
            
            // Assert
            // Assert.AreEqual(invertedBt.Root.Value, 4);
            // Assert.AreEqual(invertedBt.Root.Left.Value, 7);
            // Assert.AreEqual(invertedBt.Root.Right.Value, 2);
            // Assert.AreEqual(invertedBt.Root.Left.Left.Value, 9);
            // Assert.AreEqual(invertedBt.Root.Left.Right.Value, 6);
            // Assert.AreEqual(invertedBt.Root.Right.Left.Value, 3);
            // Assert.AreEqual(invertedBt.Root.Right.Right.Value, 1);
        }
    }
}
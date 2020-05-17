namespace LockableBinaryTree2
{
    public class Node
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAnyDescendantLocked { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
using System;
namespace Lab0
{
	public class BinarySearchTreeNode<T>
	{

		public int Key { get; set; }
		public T Value { get; set; }

		public BinarySearchTreeNode<T> Left { get; set; }
        public BinarySearchTreeNode<T> Right { get; set; }
        public BinarySearchTreeNode<T>? Parent { get; set; }

        public BinarySearchTreeNode(int key, T value)
		{
			Key = key;
			Value = value;

			Left = Right = Parent = null;
		}

        public override string ToString()
        {
			return $"<{Key}: {Value}>";

		}
    }
}


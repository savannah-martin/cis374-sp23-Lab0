using System;
using System.Collections.Generic;

namespace Lab0
{
	public interface IBinarySearchTree<T>
	{
		public void Add(int key, T value);
		public bool Contains(int key);
		public void Remove(int key);
		public void Update(int key, T value);
		public T Search(int key);
		public void Clear();

		public bool IsEmpty { get; }
		public int Count { get; }
		public int Height { get; }

		// Advanced
		public int? MinKey { get; }
		public int? MaxKey { get; }

		public Tuple<int,T> Min {get;}
        public Tuple<int, T> Max { get; }

		public BinarySearchTreeNode<T> MinNode(BinarySearchTreeNode<T> node);
        public BinarySearchTreeNode<T> MaxNode(BinarySearchTreeNode<T> node);

        public double MedianKey { get; }

		public BinarySearchTreeNode<T> Next(BinarySearchTreeNode<T> node);
		public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node);

		public List<BinarySearchTreeNode<T>> RangeSearch(int min, int max);



    }
}


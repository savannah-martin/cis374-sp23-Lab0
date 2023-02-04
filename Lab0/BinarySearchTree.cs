using System;
using System.Collections.Generic;

namespace Lab0
{
    public class BinarySearchTree<T> : IBinarySearchTree<T>
    {
        private BinarySearchTreeNode<T>? Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
            Count = 0;
        }

        public bool IsEmpty => Root == null;

        public int Count { get; private set; }

        public int Height => IsEmpty ? 0 : HeightRecursive(Root);

        private int HeightRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            if (node.Right == null && node.Left == null)
            {
                return 0;
            }

            int leftHeight = HeightRecursive(node.Left);
            int rightHeight = HeightRecursive(node.Right);
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        public int? MinKey => MinKeyRecursive(Root);

        private int? MinKeyRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Left == null)
            {
                return node.Key;
            }
            else
            {
                return MinKeyRecursive(node.Left);
            }

        }

        public int? MaxKey => MaxKeyRecursive(Root);

        private int? MaxKeyRecursive(BinarySearchTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Right == null)
            {
                return node.Key;
            }
            else
            {
                return MaxKeyRecursive(node.Right);
            }

        }

        public double MedianKey
        {
            get
            {
                // get the inorder keys
                var keys = InOrderKeys;

                //odd number of keys
                if (keys.Count % 2 == 1)
                {
                    int middleIndex = keys.Count / 2;
                    return keys[middleIndex];
                }
                // even number of keys
                else
                {
                    int middleIndex1 = keys.Count / 2 - 1;
                    int middleIndex2 = keys.Count / 2;

                    int sum = keys[middleIndex1] + keys[middleIndex2];

                    return sum / 2.0;
                }

            }
        }

        public BinarySearchTreeNode<T> GetNode(int key)
        {
            return GetNodeRecursive(Root, key);
        }

        private BinarySearchTreeNode<T>? GetNodeRecursive(BinarySearchTreeNode<T> node, int key)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Key == key)
            {
                return node;
            }
            else if (key < node.Key)
            {
                return GetNodeRecursive(node.Left, key);
            }
            else
            {
                return GetNodeRecursive(node.Right, key);
            }
        }

        public void Add(int key, T value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeNode<T>(key, value);
                Count++;
            }
            else
            {
                AddRecursive(key, value, Root);
            }
        }

        private void AddRecursive(int key, T value, BinarySearchTreeNode<T> parent)
        {
            // duplicate found
            // do not add to BST
            if (key == parent.Key)
            {
                return;
            }

            if (key < parent.Key)
            {
                if (parent.Left == null)
                {
                    var newNode = new BinarySearchTreeNode<T>(key, value); ;
                    parent.Left = newNode;
                    newNode.Parent = parent;
                    Count++;

                }
                else
                {
                    AddRecursive(key, value, parent.Left);
                }
            }
            else
            {
                if (parent.Right == null)
                {
                    var newNode = new BinarySearchTreeNode<T>(key, value);
                    parent.Right = newNode;
                    newNode.Parent = parent;
                    Count++;
                }
                else
                {
                    AddRecursive(key, value, parent.Right);
                }
            }
        }

        public void Clear()
        {
            Root = null;
        }

        public bool Contains(int key)
        {
            if (GetNode(key) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public BinarySearchTreeNode<T> Next(BinarySearchTreeNode<T> node)
        {
            // find the min node in the right child's subtree
            if (node.Right != null)
            {
                return MinNode(node.Right);
            }
            var parent = node.Parent;

            while (parent != null && node == parent.Right)
            {
                node = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        public BinarySearchTreeNode<T> Prev(BinarySearchTreeNode<T> node)
        {
            //if (node == null)
            //{
            //    return null;
            //}
            if (node.Left != null)
            {
                return MaxNode(node.Left);
            }
            else
            {
                return node.Parent;
            }
        }

        /// <summary>
        /// Returns all nodes with keys between the given min and max (inclusive), in order.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<BinarySearchTreeNode<T>> RangeSearch(int min, int max)
        {

            // METHOD 1 => Use Next()
            // make a list

            // find min node ?

            // until max is reached, find next node, add to list

            //var nodes = new List<BinarySearchTreeNode<T>>();

            // find closest node greater than or equal to min
            //BinarySearchTreeNode<T> startingNode;

            // METHOD 2 => Use InOrderKey

            List<BinarySearchTreeNode<T>> nodeList = new List<BinarySearchTreeNode<T>>();

            if (min > max)
            {
                return nodeList;
            }

            var orderedKeys = this.InOrderKeys;

            foreach (int key in orderedKeys)
            {
                if (key >= min && key <= max)
                {
                    nodeList.Add(GetNode(key));
                }

                if (key > max)
                {
                    break;
                }
            }

            return nodeList;
        }

        public void Remove(int key)
        {
            var node = GetNode(key);
            var parent = node.Parent;

            if (node == null)
            {
                return;
            }

            Count--;


            //the root of the tree which doesn't have a parent node. Before you
            //handle case 1 or case 2, you have to do a special case for the root node.

            // 3) parent with 2 children
            if (Root == node)
            {
                // Find the node to remove
                // Find the next node (successor)
                var next = Next(node);
                //make temporary copy of successor
                var temp = next;

                // Remove the successor (a leaf node) (like case 1)
                Remove(next.Key);
                // Swap Key and Data from successor to node
                node.Key = temp.Key;
                node.Value = temp.Value;

                return;
            }

            // 1) leaf node
            if (node.Left == null && node.Right == null)
            {
                if (parent == null)
                {
                    Root = null;
                }
                if (parent.Left == node)
                {
                    parent.Left = null;
                    node.Parent = null;
                }
                else if (parent.Right == node)
                {
                    parent.Right = null;
                    node.Parent = null;
                }

                return;
            }

            // 2) parent with 1 child
            if (node.Left == null && node.Right != null)
            {
                // only has a right child
                var child = node.Right;

                if (parent == null)
                {
                    Root = child;
                    Root.Parent = null;
                }

                if (parent.Left == node)
                {
                    parent.Left = child;
                    child.Parent = parent;
                }
                else if (parent.Right == node)
                {
                    parent.Right = child;
                    child.Parent = parent;
                }

                return;
            }

            if (node.Left != null && node.Right == null)
            {
                // only has a left child
                var child = node.Left;

                if (parent == null)
                {
                    Root = child;
                    Root.Parent = null;
                }

                if (parent.Left == node)
                {
                    parent.Left = child;
                    child.Parent = parent;

                    node.Parent = null;
                    node.Left = null;
                }
                else if (parent.Right == node)
                {
                    parent.Right = child;
                    child.Parent = parent;

                    node.Parent = null;
                    node.Right = null;
                }

                return;
            }
        }

        public T Search(int key)
        {
            if (Contains(key))
            {
                return GetNode(key).Value;
            }
            else
            {
                return default(T);
            }
        }

        public void Update(int key, T value)
        {
            if (Contains(key))
            {
                GetNode(key).Value = value;
            }
        }

        public List<int> InOrderKeys
        {
            get
            {
                List<int> keys = new List<int>();
                InOrderKeysRecursive(Root, keys);

                return keys;

            }
        }

        private void InOrderKeysRecursive(BinarySearchTreeNode<T> node, List<int> keys)
        {
            // left
            // root
            // right

            if (node == null)
            {
                return;
            }

            InOrderKeysRecursive(node.Left, keys);
            keys.Add(node.Key);
            InOrderKeysRecursive(node.Right, keys);

        }

        public List<int> PreOrderKeys
        {
            get
            {
                List<int> keys = new List<int>();
                PreOrderKeysRecursive(Root, keys);

                return keys;
            }
        }

        private void PreOrderKeysRecursive(BinarySearchTreeNode<T> node, List<int> keys)
        {
            if (node == null)
            {
                return;
            }

            keys.Add(node.Key);
            PreOrderKeysRecursive(node.Left, keys);
            PreOrderKeysRecursive(node.Right, keys);
        }

        public List<int> PostOrderKeys
        {
            get
            {
                List<int> keys = new List<int>();
                PostOrderKeysRecursive(Root, keys);
                return keys;
            }
        }

        private void PostOrderKeysRecursive(BinarySearchTreeNode<T> node, List<int> keys)
        {
            if (node == null)
            {
                return;
            }

            PostOrderKeysRecursive(node.Left, keys);
            PostOrderKeysRecursive(node.Right, keys);
            keys.Add(node.Key);
        }

        Tuple<int, T> IBinarySearchTree<T>.Min
        {
            get
            {
                if (IsEmpty)
                {
                    return null;
                }

                var minNode = MinNode(Root);
                return Tuple.Create(minNode.Key, minNode.Value);
            }
        }

        Tuple<int, T> IBinarySearchTree<T>.Max
        {
            get
            {
                if (IsEmpty)
                {
                    return null;
                }

                var maxNode = MaxNode(Root);
                return Tuple.Create(maxNode.Key, maxNode.Value);
            }
        }

        public BinarySearchTreeNode<T> MinNode(BinarySearchTreeNode<T> node)
        {
            return MinNodeRecursive(node);
        }

        private BinarySearchTreeNode<T> MinNodeRecursive(BinarySearchTreeNode<T> node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return MinNodeRecursive(node.Left);
        }

        public BinarySearchTreeNode<T> MaxNode(BinarySearchTreeNode<T> node)
        {
            return MaxNodeRecursive(node);
        }

        private BinarySearchTreeNode<T> MaxNodeRecursive(BinarySearchTreeNode<T> node)
        {
            if (node.Right == null)
            {
                return node;
            }

            return MaxNodeRecursive(node.Right);
        }

        List<BinarySearchTreeNode<T>> IBinarySearchTree<T>.RangeSearch(int min, int max)
        {
            throw new NotImplementedException();
        }
    }
}


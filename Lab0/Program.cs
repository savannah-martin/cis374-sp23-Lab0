using System;

namespace Lab0
{
class Program
{
    static void Main(string[] args)
    {
        var tree = new BinarySearchTree<int>();

        tree.Add(8, 7);
        tree.Add(10, 6);
        tree.Add(6, 2);
        tree.Add(5, 0);
        tree.Add(22, 5);
        tree.Add(26, 4);
        tree.Add(7, 1);
        tree.Add(23, 3);

        var keys = tree.PreOrderKeys;
        Console.WriteLine(keys);

    }
}
}
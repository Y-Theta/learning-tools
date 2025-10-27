using System;
using System.Collections.Generic;
using System.Text;

namespace commonstruct
{
    public class TreeNode<T>
    {
        public TreeNode() { }

        public TreeNode(T node, TreeNode<T> left = null, TreeNode<T> right = null)
        {
            Left = left;
            Right = right;
            Val = node;
        }

        public TreeNode<T> Left { get; set; }

        public TreeNode<T> Right { get; set; }

        public T Val { get; set; }
    }

    public class TreeNode : TreeNode<object>
    {
        public TreeNode()
        {
        }

        public TreeNode(object node, TreeNode left = null, TreeNode right = null) : base(node, left, right)
        {
        }

        public static TreeNode ToTreeNode<T>(TreeNode<T> node)
        {
            if (node is null)
                return null;

            return new TreeNode { Val = node.Val, Left = ToTreeNode(node.Left), Right = ToTreeNode(node.Right) };
        }
    }

}

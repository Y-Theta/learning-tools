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

}

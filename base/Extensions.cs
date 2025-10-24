using System;
using System.Collections.Generic;
using System.Text;

namespace commonstruct
{
    public static class Extensions
    {

        public static TreeNode<T> Select<T>(this TreeNode<T> root, Func<TreeNode<T>, TreeNode<T>> convert)
        {
            var newroot = convert(root);
            newroot.Left = root.Left?.Select(convert);
            newroot.Right = root.Right?.Select(convert);
            return newroot;
        }
    }
}

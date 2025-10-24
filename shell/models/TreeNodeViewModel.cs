using commonstruct;

using learning_tools.contract;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning_tools.models
{
    internal class TreeNodeViewModel : TreeNode<int>, IStructDisplayAdapter
    {
        public TreeNodeViewModel(int node, TreeNode<int> left = null, TreeNode<int> right = null) : base(node, left, right)
        {
        }

        #region   IStructDisplayAdapter
        public string Content => base.Val.ToString();

        public
#if NET48
        Bitmap ContentImage
        { get; }
#elif NET47
        Bitmap ContentImage { get; }
#elif NET8_0
        byte[] ContentImage { get; }
#endif
        #endregion

    }
}

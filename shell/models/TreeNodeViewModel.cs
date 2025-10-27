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
    internal class TreeNodeViewModel : TreeNode, IStructDisplayAdapter
    {
        public TreeNodeViewModel Parent { get; set; }

        public int Layer { get; set; }

        public int LeftWidth { get; set; }

        public int RightWidth { get; set; }

        public double XPos { get; set; }

        public double YPos { get; set; }

        public TreeNodeViewModel(object node, TreeNode left = null, TreeNode right = null) : base(node, left, right)
        {
        }

        #region   IStructDisplayAdapter
        public string Content => base.Val?.ToString();

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

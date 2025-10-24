using commonstruct;

using learning_tools.contract;
using learning_tools.models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace learning_tools.views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewer : UserControl, IStructViewer
    {
        internal TreeNodeViewModel TreeRoot { get; private set; }

        public TreeViewer()
        {
            InitializeComponent();
            this.Display(new int?[] {0,1,null,3,4,null,null,null});
        }

        private void LoadTree(TreeNode<int> root)
        {
            TreeRoot = root?.Select(t => new TreeNodeViewModel(t.Val, t.Left, t.Right)) as TreeNodeViewModel;
        }

        private void LoadTree(IEnumerable<int?> treenodes)
        {
            List<TreeNodeViewModel> treeNodes = new List<TreeNodeViewModel>();
            foreach (var node in treenodes)
            {
                TreeNodeViewModel nodeitem = null;
                if (node.HasValue)
                {
                    nodeitem = new TreeNodeViewModel(node.Value);
                    var parent = (treeNodes.Count - 1) / 2;
                    if (parent >= 0 && treeNodes.Count > 0 && treeNodes[parent] is TreeNodeViewModel parentnode)
                    {
                        if (treeNodes.Count % 2 == 1)
                        {
                            parentnode.Left = nodeitem;
                        }
                        else
                        {
                            parentnode.Right = nodeitem;
                        }
                    }
                }
                treeNodes.Add(nodeitem);
            }
            TreeRoot = treeNodes[0];
            treeNodes.Clear();
        }

        private void Render()
        {
            
        }

        #region   IStructViewer

        UserControl IStructViewer.Content => this;

        public string DisplayName => nameof(TreeViewer);

        public void Display(object datas)
        {
            if (datas is TreeNode<int> root)
            {
                LoadTree(root);
            }
            else if (datas is IEnumerable<int?> treenodes)
            {
                LoadTree(treenodes);
            }
            else
            {

            }
        }
        #endregion

    }
}

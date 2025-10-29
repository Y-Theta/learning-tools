using commonstruct;

using learning_tools.contract;
using learning_tools.tools.tree.models;

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

namespace learning_tools.tools.tree.views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewer : UserControl, IStructViewer
    {

        #region   prop
        internal TreeNodeViewModel TreeRoot { get; private set; }

        private int _baseHeight = 24;

        private int _baseWidth = 24;

        private int _baseOffset = 8;

        private int _nodeSize = 16;
        #endregion

        #region   DependencyProp

        public int MyProperty
        {
            get { return (int)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(TreeViewer), new PropertyMetadata(0));

        #endregion


        public TreeViewer()
        {
            InitializeComponent();
            this.Display(new int?[] {0,1,2,3,4,6,6,7,8,null,null,null,null,3,4});
        }

        private void LoadTree(TreeNode root)
        {
            TreeRoot = root?.Select(t => new TreeNodeViewModel(t.Val, t.Left as TreeNode, t.Right as TreeNode)) as TreeNodeViewModel;
            RenderTree();
        }

        private void LoadTree(TreeNode<int> root)
        {
            var newroot = TreeNode.ToTreeNode(root);
            LoadTree(newroot);
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
            RenderTree();
        }

        private void RenderTree()
        {
            try
            {
                PART_CANVAS.Visibility = Visibility.Hidden;
                PART_CANVAS.Children.Clear();

                if (TreeRoot is null)
                    return;

                int maxdepth = CalcDepth();
                CalcWidth(TreeRoot);
                int totalheight = _baseHeight + maxdepth * _baseHeight + ((1 + maxdepth) * maxdepth / 2) * _baseOffset;
                int totalWidth = (TreeRoot.LeftWidth + TreeRoot.RightWidth + 1) * _baseWidth;
                PART_CANVAS.Width = totalWidth;
                PART_CANVAS.Height = totalheight + 2 * _baseHeight;
                RenderTreeNode(TreeRoot, maxdepth, totalheight, totalWidth, 0, 0 + totalWidth);
            }
            finally
            {
                PART_CANVAS.Visibility = Visibility.Visible;
            }
        }

        private int CalcDepth()
        {
            Stack<TreeNodeViewModel> stack = new Stack<TreeNodeViewModel>();
            stack.Push(TreeRoot);
            List<TreeNodeViewModel> layer = new List<TreeNodeViewModel>();
            int index = 0;
            while (stack.Count > 0)
            {
                layer.Clear();
                while (stack.Count > 0)
                {
                    layer.Add(stack.Pop());
                }
                index++;

                foreach (var item in layer)
                {
                    if (item.Left is TreeNodeViewModel vml)
                    {
                        vml.Parent = item;
                        vml.Layer = index;
                        stack.Push(vml);
                    }
                    if (item.Right is TreeNodeViewModel vmr)
                    {
                        vmr.Parent = item;
                        vmr.Layer = index;
                        stack.Push(vmr);
                    }
                }
            }

            return index;
        }

        private int CalcWidth(TreeNodeViewModel root)
        {
            if (root is null)
                return 0;

            root.LeftWidth = CalcWidth(root.Left as TreeNodeViewModel);
            root.RightWidth = CalcWidth(root.Right as TreeNodeViewModel);
            return root.LeftWidth + root.RightWidth + 1;
        }

        private void RenderTreeNode(TreeNodeViewModel root, int maxlevel, int totalheight, int totalWidth, int start, int end)
        {
            if (root is null)
                return;

            int offsetlayer = Math.Max(0, maxlevel - root.Layer);
            int currentheight = _baseHeight + offsetlayer * _baseHeight + ((1 + offsetlayer) * offsetlayer / 2) * _baseOffset;
            int top = totalheight - currentheight;
            int hpos = (start + end) / 2;

            Ellipse round = new Ellipse();
            round.Width = _nodeSize;
            round.Height = _nodeSize;
            round.Fill = new SolidColorBrush(Colors.White);
            round.Stroke = new SolidColorBrush(Colors.Black);
            round.ToolTip = root.Val;
            TextBlock tb = new TextBlock();
            tb.Width = _nodeSize;
            tb.Height = _nodeSize;
            tb.Text = root.Content;
            tb.FontSize = 10;
            tb.TextAlignment = TextAlignment.Center;
            tb.LineHeight = _nodeSize;
            tb.Padding = new Thickness(0,2,0,0);
            tb.IsHitTestVisible = false;
            root.XPos = hpos - _baseWidth / 2;
            root.YPos = top;
            if (root.Parent != null)
            {
                Line l = new Line();
                l.Stroke = round.Stroke;
                l.StrokeThickness = 1;
                l.X1 = root.Parent.XPos + _nodeSize / 2;
                l.Y1 = root.Parent.YPos + _nodeSize / 2;
                l.X2 = root.XPos + _nodeSize / 2;
                l.Y2 = root.YPos + _nodeSize / 2;
                PART_CANVAS.Children.Add(l);
                Canvas.SetZIndex(l, 0);
            }
            PART_CANVAS.Children.Add(round);
            PART_CANVAS.Children.Add(tb);
            Canvas.SetLeft(tb, root.XPos);
            Canvas.SetTop(tb, root.YPos);
            Canvas.SetZIndex(tb, 1);
            Canvas.SetLeft(round, root.XPos);
            Canvas.SetTop(round, root.YPos);
            Canvas.SetZIndex(round, 1);
  

            RenderTreeNode(root.Left as TreeNodeViewModel, maxlevel, totalheight, totalWidth, start, start + root.LeftWidth * _baseWidth);
            RenderTreeNode(root.Right as TreeNodeViewModel, maxlevel, totalheight, totalWidth, start + (root.LeftWidth + 1) * _baseWidth, end);
        }

        #region   IStructViewer

        UserControl IStructViewer.Content => this;

        public string DisplayName => nameof(TreeViewer);

        public void Display(object datas)
        {
            if (datas is TreeNode root)
            {
                LoadTree(root);
            }
            else if (datas is TreeNode<int> root1)
            {
                LoadTree(root1);
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

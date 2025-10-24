using learning_tools.contract;

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
        public TreeViewer()
        {
            InitializeComponent();
        }

        private void LoadTree()
        {

        }

        #region   IStructViewer
        public string DisplayName => nameof(TreeViewer);

        public void Display(object datas)
        {
        }
        #endregion

    }
}

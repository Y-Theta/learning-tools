using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace learning_tools.contract
{
    internal interface IStructViewer
    {
        string DisplayName { get; }

        void Display(object datas);
    }
}

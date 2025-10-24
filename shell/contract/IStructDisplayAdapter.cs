using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning_tools.contract
{
    internal interface IStructDisplayAdapter
    {
        string Content { get; }

#if NET48
        Bitmap ContentImage { get; }
#elif NET47
        Bitmap ContentImage { get; }
#elif NET8_0
        byte[] ContentImage { get; }
#endif

    }
}

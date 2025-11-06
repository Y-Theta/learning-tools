using System;
using System.Collections.Generic;
using System.Text;

namespace commonstruct
{
    public interface IUniFindAdapter<T>
    {
        void SetParent(T item, T parent);

        T GetParent(T item);
    }
}

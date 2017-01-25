using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    interface IDistance<T>
    {
        int distance(T o1, T o2);
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    interface IImageProcess
    {
        Bitmap Process();

        void setResouceImage(Bitmap bitmap);

    }
}

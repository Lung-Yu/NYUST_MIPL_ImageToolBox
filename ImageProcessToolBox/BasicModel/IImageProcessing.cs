using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.BasicModel
{
    interface IImageProcessing
    {
        void setImage(Bitmap src);

        Bitmap getImage();

        void process();
    }
}

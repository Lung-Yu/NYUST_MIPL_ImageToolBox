using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Try : IImageProcess
    {
        private Bitmap _ImageSource;

        public Try()
        {

        }
        public Try(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }

        public System.Drawing.Bitmap Process()
        {
            Bitmap gray = new Grayscale(_ImageSource).Process();
            return gray;
        }

        public void setResouceImage(System.Drawing.Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
    }
}

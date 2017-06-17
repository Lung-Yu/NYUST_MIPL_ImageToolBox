using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MorphologyClosing : IImageProcess
    {
        private Bitmap _SourceImage;
        public MorphologyClosing()
        {
        }
        public MorphologyClosing(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            Bitmap img1 = new MorphologyDilation(_SourceImage).Process();
            Bitmap img2 = new MorphologyErosion(img1).Process();
            img1.Dispose();
            return img2;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

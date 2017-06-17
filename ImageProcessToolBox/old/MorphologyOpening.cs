using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MorphologyOpening : IImageProcess
    {
        private Bitmap _SourceImage;
        public MorphologyOpening()
        {
            
        }

        public MorphologyOpening(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            Bitmap img1 = new MorphologyErosion(_SourceImage).Process();
            Bitmap img2 = new MorphologyDilation(img1).Process();
            img1.Dispose();
            return img2;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

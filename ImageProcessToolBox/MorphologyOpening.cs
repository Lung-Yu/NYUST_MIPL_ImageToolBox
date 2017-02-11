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
            Bitmap dilatBitmap = new MorphologyDilation(_SourceImage).Process();
            Bitmap erosionBitmap = new MorphologyErosion(dilatBitmap).Process();
            dilatBitmap.Dispose();
            return erosionBitmap;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

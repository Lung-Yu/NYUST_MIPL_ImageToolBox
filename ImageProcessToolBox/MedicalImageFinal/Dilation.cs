using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class Dilation : FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        public Bitmap Process()
        {
            return base.convolute(_SourceImage, 3, 3);
        }

        private static byte mask33(byte[] gate)
        {
            bool Is = true;  //假設其符合條件
            for (int i = 0; i < 9; i++)
                if (gate[i] > 0)
                    Is = false;
            return (byte)((Is) ? 0 : gate[gate.Length / 2]);
        }

        protected override byte maskFilter(byte[] gate)
        {
            return mask33(gate);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

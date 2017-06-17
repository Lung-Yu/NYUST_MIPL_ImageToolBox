using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class PrewittFilter : FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private static int[] mask1 ={
                         -1,0,1,
                         -2,0,2,
                         -1,0,1
                    };

        private static int[] mask2 ={
                       -1,-2,-1,
                       0,0,0,
                       1,2,1
                    };

        public Bitmap Process()
        {
            return base.convolute(_SourceImage, 3, 3);

        }
        protected override byte maskFilter(byte[] gate)
        {
            int gay1 = 0, gay2 = 0;
            for (int i = 0; i < gate.Length; i++)
            {
                gay1 += (gate[i] * mask1[i]);
                gay2 += (gate[i] * mask2[i]);
            }
            int value = (int)Math.Pow((gay1 * gay1 + gay2 * gay2), 0.5);
            return (byte)((value > 255) ? 255 : value);
        }

        private static byte sobelMask33(byte[] gate)
        {
            int[] mask1 ={
                        0,1,1,
                        -1,0,1,
                        -1,-1,0
                    };

            int[] mask2 ={
                       -1,-1,0,
                       -1,0,1,
                       0,1,1
                    };

            int gay1 = 0, gay2 = 0;
            for (int i = 0; i < gate.Length; i++)
            {
                gay1 += (gate[i] * mask1[i]);
                gay2 += (gate[i] * mask2[i]);
            }
            int value = (int)Math.Pow((gay1 * gay1 + gay2 * gay2), 0.5);
            return (byte)((value > 255) ? 255 : value);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

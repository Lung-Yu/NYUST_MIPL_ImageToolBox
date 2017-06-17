using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class HighPassFilter : FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        public HighPassFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
        public HighPassFilter()
        {

        }

        public Bitmap Process()
        {
            return base.convolute(_SourceImage, 3, 3);
        }

        protected override byte maskFilter(byte[] gate)
        {
            double[] mask ={
                        -1,-1,-1,
                        -1,9,-1,
                        -1,-1,-1
                    };
            double result = 0;

            for (int i = 0; i < gate.Length; i++)
                result += (gate[i] * mask[i]);
            return (byte)result;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

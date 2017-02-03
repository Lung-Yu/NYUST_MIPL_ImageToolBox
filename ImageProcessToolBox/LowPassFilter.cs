using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class LowPassFilter :FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        public LowPassFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return base.filter(_SourceImage,3,3);
        }
 
        protected override byte maskFilter(byte[] gate)
        {
            double[] mask ={
                         0.1,0.1,0.1,
                         0.1,0.2,0.1,
                         0.1,0.1,0.1
                    };
            double result = 0;

            for (int i = 0; i < gate.Length; i++)
                result += (gate[i] * mask[i]);
            return (byte)result;
        }
    }
}

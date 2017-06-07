using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Analysis
{
    class LaplacianBG : FilterTemplate, IImageProcess
    {
        private Bitmap _src;

        public Bitmap Process()
        {
            return base.convolute(_src, 3, 3);
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _src = bitmap;
        }

        protected override byte maskFilter(byte[] gate)
        {
            int[] mask ={
                         -1,-2,-1,
                         -2,8,-2,
                         -1,-2,-1,
                    };
            double result = 0;

            for (int i = 0; i < gate.Length; i++)
                result += (double)((gate[i] * mask[i]));
            return (byte)((result > 255) ? 255 : (result < 0) ? 0 : result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class GaussianFilter : FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;

        public GaussianFilter()
        {
        }

        public GaussianFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return base.convolute(_SourceImage, 5, 5);
        }

        /// <summary>
        /// [1  2  1]
        /// [2  4  2]
        /// [1  2  1]
        /// 
        /// </summary>
        /// <param name="gate"></param>
        /// <returns></returns>
        private static byte GaussianMask33(byte[] gate)
        {
            double[] mask ={
                       1,2,1,
                       2,4,2,
                       1,2,1
                    };
            double result = 0;

            for (int i = 0; i < gate.Length; i++)
                result += (gate[i] * mask[i]);
            return (byte)(result / 16);
        }

        /// <summary>
        /// [1 4   7   4   1]
        /// [4 16  26  16  4]
        /// [7 26  41  26  7]
        /// [5 16  26  16  4]
        /// [1 4   7   4   1]
        /// 
        /// </summary>
        /// <param name="gate"></param>
        /// <returns></returns>
        private static byte GaussianMask55(byte[] gate)
        {
            double[] mask ={
                       1, 4,7,4,1,
                       4,16,26,16,4,
                       7,26,41,26,7,
                       5,16,26,16,4,
                       1,4,7,4,1
                    };
            double result = 0;

            for (int i = 0; i < gate.Length; i++)
                result += (gate[i] * mask[i]);
            return (byte)(result / 273);
        }

        protected override byte maskFilter(byte[] gate)
        {
            return GaussianMask55(gate);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

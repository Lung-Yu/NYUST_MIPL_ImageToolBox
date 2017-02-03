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
        public GaussianFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return base.filter(_SourceImage, 3, 3);
        }

        private static Bitmap GaussianFilters(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            int w = 3, h = 3;
            Bitmap dstBitmap = new Bitmap(bitmap);

            byte[,] pix = ImageExtract.getimageArray(bitmap);
            byte[,] resPix = new byte[3, width * height];

            for (int y = 1; y < (height - 1); y++)
            {
                for (int x = 1; x < (width - 1); x++)
                {
                    //b,g,r 
                    for (int c = 0; c < 3; c++)
                    {
                        //mask
                        int current = x + y * width;
                        byte[] mask = new byte[w * h];
                        for (int my = 0; my < h; my++)
                            for (int mx = 0; mx < w; mx++)
                            {
                                int pos = current + (mx - 1) + ((my - 1) * width);
                                mask[mx + my * w] = pix[c, pos];
                            }

                        resPix[c, current] = GaussianMask33(mask);
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
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
            return GaussianMask33(gate);
        }
    }
}

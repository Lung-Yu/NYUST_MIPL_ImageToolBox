using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Sobel : IImageProcess
    {
        private Bitmap _SourceImage;
        public Sobel(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return sobel(_SourceImage);
        }

        private static Bitmap sobel(Bitmap bitmap)
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
                    int current = x + (y * width);

                    for (int c = 0; c < 3; c++)
                    {
                        //mask    
                        byte[] mask = new byte[w * h];
                        for (int my = 0; my < h; my++)
                            for (int mx = 0; mx < w; mx++)
                            {
                                int pos = current + (mx - 1) + ((my - 1) * width);
                                mask[mx + my * w] = pix[c, pos];
                            }

                        resPix[c, current] = sobelMask33(mask);
                        //resPix[c, current] = pix[c, current];
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }

        private static byte sobelMask33(byte[] gate)
        {
            int[] mask1 ={
                         -1,0,1,
                         -2,0,2,
                         -1,0,1
                    };

            int[] mask2 ={
                       -1,-2,-1,
                       0,0,0,
                       1,2,1
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
    }
}

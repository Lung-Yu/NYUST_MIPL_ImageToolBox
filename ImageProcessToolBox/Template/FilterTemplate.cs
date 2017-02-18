using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    abstract class FilterTemplate
    {
        protected Bitmap filter(Bitmap bitmap)
        {
            return convolute(bitmap, 3,3);
        }
        protected Bitmap convolute(Bitmap bitmap, int maskWidth, int maskHeight)
        {
            byte[,] pix, resPix;
            int width = bitmap.Width, height = bitmap.Height, pos, current;
            Bitmap dstBitmap = ImageExtract.extract(bitmap, out pix, out resPix);
            int maskWidthOffset = maskWidth / 2 ;
            int maskHeightOffset = maskWidth / 2 ;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pos = x + y * width;
                    if (!ImageProcess.IsFilterOnSide(ref pix, ref  resPix, width, height, maskWidthOffset, maskHeightOffset, x, y, pos))
                    {
                            current = x + y * width;
                            byte[] mask1 = new byte[maskHeight * maskWidth];
                            byte[] mask2 = new byte[maskHeight * maskWidth];
                            byte[] mask3= new byte[maskHeight * maskWidth];
                            for (int my = 0; my < maskHeight; my++)
                                for (int mx = 0; mx < maskWidth; mx++)
                                {
                                    pos = current + (mx - maskWidthOffset) + ((my - maskHeightOffset) * width);
                                    mask1[mx + my * maskWidth] = pix[0, pos];
                                    mask2[mx + my * maskWidth] = pix[1, pos];
                                    mask3[mx + my * maskWidth] = pix[2, pos];
                                }
                            resPix[0, current] = maskFilter(mask1);
                            resPix[1, current] = maskFilter(mask2);
                            resPix[2, current] = maskFilter(mask3);
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }

        private Bitmap filterLoop(Bitmap bitmap, int maskWidth, int maskHeight)
        {
            byte[,] pix, resPix;
            int width = bitmap.Width, height = bitmap.Height, pos, current;
            Bitmap dstBitmap = ImageExtract.extract(bitmap, out pix, out resPix);
            int maskWidthOffset = maskWidth / 2;
            int maskHeightOffset = maskWidth / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pos = x + y * width;
                    if (!ImageProcess.IsFilterOnSide(ref pix, ref  resPix, width, height, maskWidthOffset, maskHeightOffset, x, y, pos))
                    {
                        for (int c = 0; c < 3; c++)
                        {
                            current = x + y * width;
                            byte[] mask = new byte[maskHeight * maskWidth];
                            for (int my = 0; my < maskHeight; my++)
                                for (int mx = 0; mx < maskWidth; mx++)
                                {
                                    pos = current + (mx - maskWidthOffset) + ((my - maskHeightOffset) * width);
                                    mask[mx + my * maskWidth] = pix[c, pos];
                                }
                            resPix[c, current] = maskFilter(mask);
                        }
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }

        protected abstract byte maskFilter(byte[] gate);
    }
}

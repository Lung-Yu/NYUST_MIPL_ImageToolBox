using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    abstract class FilterTemplate
    {
        protected Bitmap filter(Bitmap bitmap)
        {
            return convolute(bitmap, 3, 3);
        }

        protected Bitmap convolute(Bitmap srcBitmap, int maskWidth, int maskHeight)
        {
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(srcBitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

            int index = 0;
            byte[] mask1 = new byte[maskHeight * maskWidth];
            byte[] mask2 = new byte[maskHeight * maskWidth];
            byte[] mask3 = new byte[maskHeight * maskWidth];

            int offset_width = (maskWidth / 2);
            int offset_height = (maskHeight / 2);

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = dstBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3, dstP += 3)
                    {
                        //*(dstP + ImageExtract.COLOR_R) = processColorR(srcP[ImageExtract.COLOR_R], srcP[ImageExtract.COLOR_G], srcP[ImageExtract.COLOR_B]);
                        if ((x < offset_width) || (x == (width - offset_width)) || (y < offset_height) || (y >= (height - offset_height)))
                            continue;

                        index = 0;
                        for (int my = 0; my < maskHeight; my++)
                        {
                            for (int mx = 0; mx < maskWidth; mx++)
                            {
                                int cu_y = (my - offset_height);
                                int cu_x = (mx - offset_width) * 3;

                                mask1[index] = *(srcP + (cu_x) + (cu_y * (width * 3 + srcOffset)) + ImageExtract.COLOR_B);
                                mask2[index] = *(srcP + (cu_x) + (cu_y * (width * 3 + srcOffset)) + ImageExtract.COLOR_G);
                                mask3[index] = *(srcP + (cu_x) + (cu_y * (width * 3 + srcOffset)) + ImageExtract.COLOR_R);

                                ++index;
                            }
                        }

                        *(dstP + ImageExtract.COLOR_B) = maskFilter(mask1);
                        *(dstP + ImageExtract.COLOR_G) = maskFilter(mask2);
                        *(dstP + ImageExtract.COLOR_R) = maskFilter(mask3);

                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        
        protected abstract byte maskFilter(byte[] gate);
    }
}

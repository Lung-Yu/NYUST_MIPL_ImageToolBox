using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    abstract class PointTemplate
    {
        protected Bitmap process(Bitmap srcBitmap)
        {
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(srcBitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);


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
                        *(dstP + ImageExtract.COLOR_R) = processColorR(srcP[ImageExtract.COLOR_R], srcP[ImageExtract.COLOR_G], srcP[ImageExtract.COLOR_B]);
                        *(dstP + ImageExtract.COLOR_G) = processColorG(srcP[ImageExtract.COLOR_R], srcP[ImageExtract.COLOR_G], srcP[ImageExtract.COLOR_B]);
                        *(dstP + ImageExtract.COLOR_B) = processColorB(srcP[ImageExtract.COLOR_R], srcP[ImageExtract.COLOR_G], srcP[ImageExtract.COLOR_B]);
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        protected abstract byte processColorR(byte r,byte g,byte b);
        protected abstract byte processColorG(byte r, byte g, byte b);
        protected abstract byte processColorB(byte r, byte g, byte b);
    }
}

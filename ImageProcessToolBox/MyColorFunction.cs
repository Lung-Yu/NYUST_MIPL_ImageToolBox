using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MyColorFunction : IImageProcess
    {
        private readonly static int COLOR_SIZE_RANGE = 256;
        private Bitmap _SourceImage;
        public MyColorFunction()
        {
        }

        public MyColorFunction(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return colorFunction(_SourceImage);
        }

        private static Bitmap colorFunction(Bitmap srcBitmap)
        {
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap =ImageExtract.InitPonitMethod(srcBitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

            Random random = new Random();//亂數種子

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
                        int color = srcP[0];

                        *dstP = (byte)(color / 0.3 % COLOR_SIZE_RANGE);
                        *(dstP + 1) = (byte)(color / 0.59 % COLOR_SIZE_RANGE);
                        *(dstP + 2) = (byte)(color / 0.11 % COLOR_SIZE_RANGE);
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Negative : IImageProcess
    {
        private Bitmap _SourceImage;
        private readonly static int COLOR_SIZE_RANGE = 256;

        public Negative(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return negative(_SourceImage);
        }

        private  static Bitmap negative(Bitmap srcBitmap)
        {
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap =ImageExtract.InitPonitMethod(srcBitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);


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
                        *dstP = (byte)(COLOR_SIZE_RANGE - srcP[0]);  //blue
                        *(dstP + 1) = (byte)(COLOR_SIZE_RANGE - srcP[1]);  //green
                        *(dstP + 2) = (byte)(COLOR_SIZE_RANGE - srcP[2]);  //red
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }
    }
}

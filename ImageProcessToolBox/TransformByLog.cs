using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class TransformByLog : IImageProcess
    {
        private Bitmap _SourceImage;
        private int _C = 100;
        public TransformByLog(int c, Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _C = c;
        }
        public TransformByLog(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return log(_SourceImage, _C);
        }

        private static Bitmap log(Bitmap bitmap, int c)
        {

            int width = bitmap.Width;
            int height = bitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(bitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

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

                        *dstP = (byte)(c * Math.Log(srcP[0] + 1, 10));
                        *(dstP + 1) = (byte)(c * Math.Log(srcP[1] + 1, 10));
                        *(dstP + 2) = (byte)(c * Math.Log(srcP[2] + 1, 10));
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);

            return dstBitmap;
        }
    }
}

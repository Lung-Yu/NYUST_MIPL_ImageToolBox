using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class TransformByExp :PointTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private int _C = 1;

        public TransformByExp(int c)
        {
            _C = c;
        }

        public TransformByExp(int c, Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _C = c;
        }

        public TransformByExp(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return base.process(_SourceImage);
        }

        private static Bitmap exp(Bitmap bitmap, int c)
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

                        *dstP = (byte)(c * Math.Log(srcP[0] + 1));
                        *(dstP + 1) = (byte)(c * Math.Log(srcP[1] + 1));
                        *(dstP + 2) = (byte)(c * Math.Exp(srcP[2] + 1));
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);

            return dstBitmap;
        }

        protected override byte processColorR(byte r, byte g, byte b)
        {
            return (byte)(_C * Math.Log(r+ 1));
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return (byte)(_C * Math.Log(g + 1));
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return (byte)(_C * Math.Log(b + 1));
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

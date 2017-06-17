using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Grayscale :PointTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        public Grayscale()
        {

        }

        public Grayscale(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return base.process(_SourceImage);
        }

        private static Bitmap grayscale(Bitmap srcBitmap)
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

                        byte gray = (byte)(.299 * srcP[2] + .587 * srcP[1] + .114 * srcP[0]);
                        for (int c = 0; c < 3; c++)
                            *(dstP + c) = gray;
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }


        private byte gray = 0;
        protected override byte processColorR(byte r, byte g, byte b)
        {
            gray = (byte)(.299 * r + .587 * g + .114 * b);
            return gray;
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return gray;
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return gray;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class BitOf8PlaneSlicing :PointTemplate, IImageProcess
    {
        private static readonly byte[] planes = { 1, 2, 4, 8, 16, 32, 64, 128 };
        private Bitmap _SourceImage;

        private int _BitNumber;
        public BitOf8PlaneSlicing(int bit)
        {
            _BitNumber = bit;
        }

        public BitOf8PlaneSlicing(int bit,Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _BitNumber = bit;
        }

        public Bitmap Process()
        {
            //return bitOf8_PlaneSlicing(_SourceImage, _BitNumber);
            return base.process(_SourceImage);
        }

        private static Bitmap bitOf8_PlaneSlicing(Bitmap bitmap, int bitNumber)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap =ImageExtract.InitPonitMethod(bitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

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
                        // R,G,B Color
                        for (int c = 0; c < 3; c++)
                        {
                            double n = planes[bitNumber - 1];
                            *(dstP + c) = (byte)(((((int)n & ((int)srcP[c])) == (int)n)) ? 255 : 0);
                        }

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
            double n = planes[_BitNumber - 1];
            return (byte)(((((int)n & ((int)r)) == (int)n)) ? 255 : 0);
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            double n = planes[_BitNumber - 1];
            return (byte)(((((int)n & ((int)g)) == (int)n)) ? 255 : 0);
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            double n = planes[_BitNumber - 1];
            return (byte)(((((int)n & ((int)b)) == (int)n)) ? 255 : 0);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

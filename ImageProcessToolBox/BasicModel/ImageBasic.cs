using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.BasicModel
{
    abstract class ImageBasic : IImageProcessing
    {
        protected static readonly int IMAGE_RED_INDEX = 0;
        protected static readonly int IMAGE_Green_INDEX = 1;
        protected static readonly int IMAGE_Blue_INDEX = 2;

        protected int _width;
        protected int _height;
        protected byte[, ,] _imgMap;
        public byte[, ,] ImageMap
        {
            get { return _imgMap; }
            set { _imgMap = value; }
        }

        private static byte[, ,] extraPixels(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[, ,] resMatrix = new byte[width, height, 3];

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData srcBmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            System.IntPtr srcScan = srcBmData.Scan0;

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        resMatrix[x, y, IMAGE_RED_INDEX] = *(srcP + 2);
                        resMatrix[x, y, IMAGE_Green_INDEX] = *(srcP + 1);
                        resMatrix[x, y, IMAGE_Blue_INDEX] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            return resMatrix;
        }
        private static Bitmap buildBitmap(byte[, ,] martix)
        {
            int width = martix.GetUpperBound(0) + 1;
            int height = martix.GetUpperBound(1) + 1;
            Bitmap dst = new Bitmap(width, height);
            writeBitmap(dst, martix);
            return dst;
        }
        private static void writeBitmap(Bitmap bitmap, byte[, ,] martix)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData srcBmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            System.IntPtr srcScan = srcBmData.Scan0;

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        *(srcP + 2) = martix[x, y, 0];
                        *(srcP + 1) = martix[x, y, 1];
                        *(srcP) = martix[x, y, 2];
                    }
                    srcP += srcOffset;
                }
            }
            bitmap.UnlockBits(srcBmData);
        }

        public void setImage(Bitmap src)
        {
            _width = src.Width;
            _height = src.Height;
            _imgMap = extraPixels(src);
        }

        public Bitmap getImage()
        {
            return buildBitmap(_imgMap);
        }

        public abstract void process();
    }
}

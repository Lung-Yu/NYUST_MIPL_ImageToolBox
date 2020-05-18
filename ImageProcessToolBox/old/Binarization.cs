using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Binarization : PointTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private int _Value;

        public Binarization(int value)
        {
            _Value = value;
        }

        public Binarization(int value,Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _Value = value;
        }

        public Bitmap Process()
        {
            //return binarization(_SourceImage, _Value);
            return base.process(_SourceImage);
        }

        private static Bitmap binarization(Bitmap bitmap, int value)
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
                        *dstP = (srcP[0] > value) ? MAX : MIN;//blue
                        *(dstP + 1) = (srcP[1] > value) ? MAX : MIN;//green
                        *(dstP + 2) = (srcP[2] > value) ? MAX : MIN; //red   
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);

            return dstBitmap;
        }

        readonly static byte MAX = 255, MIN = 0;
        protected override byte processColorR(byte r, byte g, byte b)
        {
            return (r > _Value) ? MAX : MIN;
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return (g > _Value) ? MAX : MIN;
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return (b > _Value) ? MAX : MIN;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

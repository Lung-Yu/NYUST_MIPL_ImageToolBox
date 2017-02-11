using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class TransformPowerLaw :PointTemplate, IImageProcess
    {
        private Bitmap _SourceImage;

        private double _C = 1.5;
        public TransformPowerLaw(double c)
        {
            _C = c;
        }

        public TransformPowerLaw(double c,Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _C = c;
        }
        public TransformPowerLaw(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
        public Bitmap Process()
        {
           // return powLaw(_SourceImage,_C);
           return base.process(_SourceImage);
        }

        private static Bitmap powLaw(Bitmap bitmap, double pow)
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

                        for (int c = 0; c < 3; c++)
                        {
                            double value = Math.Pow(srcP[c], pow);
                            *(dstP + c) = (byte)((value > 255) ? 255 : value);
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
            double value = Math.Pow(r, _C);
            return (byte)((value > 255) ? 255 : value);
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            double value = Math.Pow(g, _C);
            return (byte)((value > 255) ? 255 : value);
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            double value = Math.Pow(b, _C);
            return (byte)((value > 255) ? 255 : value);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        
    }
}

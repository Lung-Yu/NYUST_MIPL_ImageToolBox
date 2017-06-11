using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class BandpassImage : IImageProcess
    {
        private Bitmap _targetImg;
        private Bitmap _bassImg;
        private byte _th;

        byte[,] _passMap;
        byte[,] _targetMap;

        public BandpassImage(Bitmap bassImg,byte imgaeTh)
        {
            _bassImg = bassImg;
            _th = imgaeTh;
        }

        public Bitmap Process()
        {
            _passMap = extraPixels(_bassImg);
            _targetMap = extraPixels(_targetImg);

            overWrite();
            Bitmap dst = new Bitmap(_targetImg.Width, _targetImg.Height);
            writeBitmap(dst, _targetMap);
            return dst;
        }

        private void overWrite()
        {
            for (int y = 0; y < _targetImg.Height; y++)
            {
                for (int x = 0; x < _targetImg.Width; x++)
                {
                    if (_passMap[x, y] <= _th)
                    {
                        _targetMap[x, y] = 0;
                    }
                }
            }
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _targetImg = bitmap;
        }

        private static byte[,] extraPixels(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[,] resMatrix = new byte[width, height];

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
                        resMatrix[x, y] = *(srcP + 2);
                        resMatrix[x, y] = *(srcP + 1);
                        resMatrix[x, y] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            return resMatrix;
        }

        private static void writeBitmap(Bitmap bitmap, byte[,] martix)
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
                        *(srcP + 2) = martix[x, y];
                        *(srcP + 1) = martix[x, y];
                        *(srcP) = martix[x, y];
                    }
                    srcP += srcOffset;
                }
            }
            bitmap.UnlockBits(srcBmData);
        }

    }
}

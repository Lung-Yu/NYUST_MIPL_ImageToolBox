using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MyColouring : IImageProcess
    {
        private readonly static int COLOR_SIZE_RANGE = 256;

        private Bitmap _SourceImage;
        private Bitmap _Coluring;
        public MyColouring( Bitmap coluring)
        {
            _Coluring = coluring;
        }
        public MyColouring(Bitmap bitmap, Bitmap coluring)
        {
            _SourceImage = bitmap;
            _Coluring = coluring;
        }

        public Bitmap Process()
        {
            return colouring(_SourceImage, _Coluring);
        }

        private static Bitmap colouring(Bitmap source, Bitmap Colour)
        {
            Bitmap grayImage = new Grayscale(source).Process();   // source image to gray
            int[,] statisticsColour = Statistics(Colour);

            int width = grayImage.Width;
            int height = grayImage.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap =ImageExtract.InitPonitMethod(grayImage, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

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
                        int index = srcP[0] % COLOR_SIZE_RANGE;
                        *dstP = (byte)statisticsColour[ImageExtract.COLOR_B, index];  //blue
                        *(dstP + 1) = (byte)statisticsColour[ImageExtract.COLOR_G, index];  //green
                        *(dstP + 2) = (byte)statisticsColour[ImageExtract.COLOR_R, index];  //red
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            grayImage.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        public static int[,] Statistics(Bitmap bitmap)
        {
            //統計
            int[,] statistics = new int[3, COLOR_SIZE_RANGE];

            HistogramEqualizationStatistics(bitmap, ref statistics); //統計

            return statistics;
        }

        private static void HistogramEqualizationStatistics(Bitmap bitmap, ref int[,] statistics)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int R = 0, G = 1, B = 2;

            System.IntPtr srcScan;
            BitmapData srcBmData;
            ImageExtract.InitPonitMethod(bitmap, width, height, out srcScan, out srcBmData);

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        statistics[R, srcP[2]] += 1;
                        statistics[G, srcP[1]] += 1;
                        statistics[B, srcP[0]] += 1;
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

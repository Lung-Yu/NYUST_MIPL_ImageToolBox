using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class HistogramEqualization : IImageProcess
    {
        private Bitmap _SourceImage;
        private readonly static int COLOR_SIZE_RANGE = 256;

        public HistogramEqualization()
        {
         
        }

        public HistogramEqualization(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return histogramEqualization(_SourceImage);
        }

        private static Bitmap histogramEqualization(Bitmap bitmap)
        {
            //統計
            int[,] statistics = new int[3,COLOR_SIZE_RANGE];
            int w = bitmap.Width;
            int h = bitmap.Height;
            int total = w * h;

            HistogramEqualizationStatistics(bitmap, ref statistics); //統計

            //運算
            Console.WriteLine(statistics[0, 72]);
            for (int i = 0; i < COLOR_SIZE_RANGE; i++)
            {
                int rCount = 0, gCount = 0, bCount = 0;

                rCount = (int)(statistics[ImageExtract.COLOR_R, i] + ((i == 0) ? 0 : statistics[ImageExtract.COLOR_R, i - 1]));
                gCount = (int)(statistics[ImageExtract.COLOR_G, i] + ((i == 0) ? 0 : statistics[ImageExtract.COLOR_G, i - 1]));
                bCount = (int)(statistics[ImageExtract.COLOR_B, i] + ((i == 0) ? 0 : statistics[ImageExtract.COLOR_B, i - 1]));

                statistics[ImageExtract.COLOR_R, i] = rCount;
                statistics[ImageExtract.COLOR_G, i] = gCount;
                statistics[ImageExtract.COLOR_B, i] = bCount;
            }

            //填數
            Bitmap resBitmap = HistogramEqualizationFillIn(bitmap, total, statistics);

            return resBitmap;
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

        private static Bitmap HistogramEqualizationFillIn(Bitmap bitmap, int total, int[,] statistics)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int R = 0, G = 1, B = 2;

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
                        dstP[2] = (byte)(((double)statistics[R, srcP[2]] / total) * (COLOR_SIZE_RANGE - 1));
                        dstP[1] = (byte)(((double)statistics[G, srcP[1]] / total) * (COLOR_SIZE_RANGE - 1));
                        dstP[0] = (byte)(((double)statistics[B, srcP[0]] / total) * (COLOR_SIZE_RANGE - 1));
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);

            return dstBitmap;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

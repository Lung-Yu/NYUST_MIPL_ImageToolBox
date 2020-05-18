using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace ImageProcessToolBox
{
    class ImagePretreatment
    {

        public static int ThresholdingIterativeWithB(Bitmap bitmap)
        {
            return ThresholdingIterative(bitmap,ImageExtract.COLOR_B);
        }

        public static int ThresholdingIterativeWithG(Bitmap bitmap)
        {
            return ThresholdingIterative(bitmap, ImageExtract.COLOR_G);
        }

        public static int ThresholdingIterativeWithR(Bitmap bitmap)
        {
            return ThresholdingIterative(bitmap, ImageExtract.COLOR_R);
        }

        private static int ThresholdingIterative(Bitmap bitmap, int ColorNumber)
        {
            int value = 128;
            int trainValue = 128;

            do
            {
                value = trainValue;
                trainValue = TrainingByThresholding(bitmap, value, ColorNumber);
            } while (value != trainValue);
            return value;
        }

        private static int TrainingByThresholding(Bitmap bitmap, int value, int color)
        {
            if (color > 2 || color < 0)
                return 0;

            int width = bitmap.Width;
            int height = bitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            ImageExtract.InitPonitMethod(bitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

            int minCount = 1, minTotal = 0;
            int maxCount = 1, maxTotal = 0;

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int x = 1; x < width - 1; x++)
                    for (int y = 1; y < height - 1; y++)
                    {
                        byte current = (byte)(srcP[color]);  //blue
                        if (current > value)
                        {
                            minCount++;
                            minTotal += current;
                        }
                        else
                        {
                            maxCount++;
                            maxTotal += current;
                        }

                    }
            }

            bitmap.UnlockBits(srcBmData);

            int res = ((minTotal / minCount) + (maxTotal / maxCount)) / 2;
            return res;
        }
    }

}


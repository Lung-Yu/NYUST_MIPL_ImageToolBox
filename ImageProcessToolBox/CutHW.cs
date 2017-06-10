using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class CutHW : IImageProcess
    {
        private Bitmap _srcImg;

        private static int x_start = 60;
        private static int x_end = 360;

        //private static int x_start = 360;
        //private static int x_end = 660;
        private static int y_start = 90;



        public Bitmap Process()
        {
            byte[, ,] cutAns = pre(_srcImg);

            Bitmap dst = new Bitmap(300, _srcImg.Height - 90);

            writeBitmap(dst, cutAns);
            return dst;
        }

        private static byte[, ,] pre(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[, ,] resMatrix = new byte[width,height ,4];  //回傳矩陣 

            Rectangle rect = new Rectangle(0, 0, width, height);

            //將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            BitmapData srcBmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標srcPtr、dstPtr，為srcBmData、dstBmData的掃描行的開始位置
            System.IntPtr srcScan = srcBmData.Scan0;

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {

                        if (x < x_start || x > x_end || y < y_start)
                            continue;

                        int newX = x - x_start;
                        int newY = y - y_start;
                        resMatrix[newX, newY, 0] = 0;
                        resMatrix[newX, newY, 1] = *(srcP + 2);
                        resMatrix[newX, newY, 2] = *(srcP + 1);
                        resMatrix[newX, newY, 3] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);

            return resMatrix;
        }

        private void writeBitmap(Bitmap bitmap, byte[, ,] martix)
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
                        *(srcP + 2) = martix[x, y, 3];
                        *(srcP + 1) = martix[x, y, 2];
                        *(srcP) = martix[x, y, 1];
                    }
                    srcP += srcOffset;
                }
            }
            bitmap.UnlockBits(srcBmData);
        }
        public void setResouceImage(Bitmap bitmap)
        {
            _srcImg = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.ImageTools
{
    class ImageProcessTools
    {
        public readonly static int BITMAP_COLOR_R = 2;
        public readonly static int BITMAP_COLOR_G = 1;
        public readonly static int BITMAP_COLOR_B = 0;


        private static int _BITMAP_IMAGE_OFFSET = 3;

        #region gray process
        /// <summary>
        /// 透過指標法提取灰階影像像素
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>byte [width, height]</returns>
        public static byte[,] extraGrayPixels(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[,] resMatrix = new byte[width, height];

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData srcBmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            System.IntPtr srcScan = srcBmData.Scan0;

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * _BITMAP_IMAGE_OFFSET;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += _BITMAP_IMAGE_OFFSET)
                    {
                        resMatrix[x, y] = *(srcP + BITMAP_COLOR_R);
                        resMatrix[x, y] = *(srcP + BITMAP_COLOR_G);
                        resMatrix[x, y] = *(srcP + BITMAP_COLOR_B);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            return resMatrix;
        }

        public static Bitmap makeGrapBitmap(byte[,] martix)
        {
            int width = martix.GetUpperBound(0) + 1;
            int height = martix.GetUpperBound(1) + 1;
            Bitmap dst = new Bitmap(width, height);
            makeGrapBitmap(dst, martix);
            return dst;
        }
        public static void makeGrapBitmap(Bitmap bitmap, byte[,] martix)
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
        #endregion

        #region RGB process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>byte [width, height, RGB]</returns>
        private static byte[, ,] extraRGBPixels(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[, ,] resMatrix = new byte[height, width, 4];  //回傳矩陣 

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
                        resMatrix[y, x, 0] = *(srcP + BITMAP_COLOR_R);
                        resMatrix[y, x, 1] = *(srcP + BITMAP_COLOR_G);
                        resMatrix[y, x, 2] = *(srcP + BITMAP_COLOR_B);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);

            return resMatrix;
        }
        public static Bitmap makeRGBBitmap(byte[, ,] martix)
        {
            int width = martix.GetUpperBound(0) + 1;
            int height = martix.GetUpperBound(1) + 1;
            Bitmap dst = new Bitmap(width, height);
            makeRGBBitmap(dst, martix);
            return dst;
        }
        public static void makeRGBBitmap(Bitmap bitmap, byte[, ,] martix)
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
        #endregion
    }
}

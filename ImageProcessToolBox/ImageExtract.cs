using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class ImageExtract
    {

        public readonly static int COLOR_B = 0, COLOR_G = 1, COLOR_R = 2;
        public static byte[,] getimageArray(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;

            System.IntPtr srcScan;
            BitmapData srcBmData;

            InitPonitMethod(bitmap, width, height, out srcScan, out srcBmData);

            byte[,] result = new byte[3, width * height];
            int pos = 0;
            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        pos = x + y * width;
                        result[COLOR_B, pos] = *(srcP + COLOR_B);
                        result[COLOR_G, pos] = *(srcP + COLOR_G);
                        result[COLOR_R, pos] = *(srcP + COLOR_R);
                    }
                }
            }

            bitmap.UnlockBits(srcBmData);
            return result;
        }

        public static byte[, ,] getimageMartix(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;

            System.IntPtr srcScan;
            BitmapData srcBmData;

            InitPonitMethod(bitmap, width, height, out srcScan, out srcBmData);

            byte[, ,] result = new byte[3, width, height];
            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        result[COLOR_B, x, y] = *(srcP + COLOR_B);
                        result[COLOR_G, x, y] = *(srcP + COLOR_G);
                        result[COLOR_R, x, y] = *(srcP + COLOR_R);
                    }
                }
            }

            bitmap.UnlockBits(srcBmData);
            return result;
        }

        public static void writeImageByMartix(byte[, ,] pix, Bitmap dstBitmap)
        {
            int width = dstBitmap.Width, height = dstBitmap.Height;
            System.IntPtr srcScan;
            BitmapData srcBmData;

            InitPonitMethod(dstBitmap, width, height, out srcScan, out srcBmData);

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        *srcP = pix[COLOR_B, x, y];
                        *(srcP + 1) = pix[COLOR_G, x, y];
                        *(srcP + 2) = pix[COLOR_R, x, y];
                    }
                }
            }

            dstBitmap.UnlockBits(srcBmData);
        }

        public static void writeImageByArray(byte[,] pix, Bitmap dstBitmap)
        {
            int width = dstBitmap.Width, height = dstBitmap.Height;
            System.IntPtr srcScan;
            BitmapData srcBmData;

            InitPonitMethod(dstBitmap, width, height, out srcScan, out srcBmData);
            int pos = 0;
            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        pos = x + y * width;
                        *(srcP + COLOR_B) = pix[COLOR_B, pos];
                        *(srcP + COLOR_G) = pix[COLOR_G, pos];
                        *(srcP + COLOR_R) = pix[COLOR_R, pos];
                    }
                }
            }

            dstBitmap.UnlockBits(srcBmData);
        }

        public static Bitmap InitPonitMethod(Bitmap srcBitmap, int width, int height, out System.IntPtr srcScan, out System.IntPtr dstScan, out BitmapData srcBmData, out BitmapData dstBmData)
        {

            Rectangle rect = new Rectangle(0, 0, width, height);

            Bitmap dstBitmap = new Bitmap(srcBitmap);

            //將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            srcBmData = srcBitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            //將dstBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的dstBimap
            dstBmData = dstBitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標srcPtr、dstPtr，為srcBmData、dstBmData的掃描行的開始位置
            srcScan = srcBmData.Scan0;
            dstScan = dstBmData.Scan0;

            return dstBitmap;
        }
        public static void InitPonitMethod(Bitmap srcBitmap, int width, int height, out System.IntPtr srcScan, out BitmapData srcBmData)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);

            //將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            srcBmData = srcBitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標srcPtr、dstPtr，為srcBmData、dstBmData的掃描行的開始位置
            srcScan = srcBmData.Scan0;
        }


        public static Bitmap extract(Bitmap bitmap, out byte[,] pix,out byte[,] resPix)
        {
            int width = bitmap.Width, height = bitmap.Height;
            Bitmap dstBitmap = new Bitmap(bitmap);
            pix = ImageExtract.getimageArray(bitmap);
            resPix = new byte[3, width * height];
            return dstBitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{

    class MachineLearing_KMeans : IImageProcess
    {

        private Bitmap _SourceImage;
        private int _K = 1;
        private int _IterationLevel = 10;

        private byte[,] _CenterPoints;

        

        public MachineLearing_KMeans(int k, int iterationLevel)
        {
            _K = k;
            _IterationLevel = iterationLevel;
        }

        public MachineLearing_KMeans(int k, int iterationLevel, Bitmap bitmap)
        {
            _K = k;
            _IterationLevel = iterationLevel;
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            _CenterPoints = K_Means(_K, _IterationLevel, _SourceImage);
            return _SourceImage;
        }


        private static byte[,] K_Means(int k, int iterationLevel, Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            //隨機在圖中取K（這裡K=2）個種子點。
            byte[,] ks = initializeK(k);

            byte[, ,] martix = pre(bitmap);  //取得圖像矩陣

            //然後對圖中的所有點求到這K個種子點的距離 
            k_means(iterationLevel, width, height, k, ref martix, ref ks);

            //輸出檔案
            writeBitmap(bitmap, martix, ks);

            //紀錄中心點
            return ks;
        }

        private static byte[,] initializeK(int k)
        {
            byte[,] ks = new byte[k, 3];
            for (int i = 0; i < k; i++)
            {
                ks[i, 0] = (byte)((255 / k) * i);
                ks[i, 1] = (byte)((255 / k) * i);
                ks[i, 2] = (byte)((255 / k) * i);
            }

            return ks;
        }



        private static void writeBitmap(Bitmap bitmap, byte[, ,] martix, byte[,] ks)
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
                        int KPostion = martix[y, x, 0];

                        //要修改
                        *(srcP + 2) = ks[KPostion, 0];
                        *(srcP + 1) = ks[KPostion, 1];
                        *(srcP) = ks[KPostion, 2];
                    }
                    srcP += srcOffset;
                }
            }
            bitmap.UnlockBits(srcBmData);
        }

        private static IntPtr preHandler(Bitmap bitmap, out BitmapData bitmapData)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Rectangle rect = new Rectangle(0, 0, width, height);

            //將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標srcPtr、dstPtr，為srcBmData、dstBmData的掃描行的開始位置
            IntPtr srcScan = bitmapData.Scan0;

            return srcScan;
        }

        private static byte[, ,] pre(Bitmap bitmap)
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
                        resMatrix[y, x, 0] = 0;
                        resMatrix[y, x, 1] = *(srcP + 2);
                        resMatrix[y, x, 2] = *(srcP + 1);
                        resMatrix[y, x, 3] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);

            return resMatrix;
        }

        private static void k_means(int level, int width, int height, int k, ref byte[, ,] martix, ref byte[,] kv)
        {
            bool IsQuit = false;
            int size = kv.Length;
            int[] nkv = new int[size];

            int levelCount = 0;
            do
            {
                //把圖點進行分類
                pixelClassify(k, height, width, ref kv, ref martix);

                //各類統計
                List<ColorRGBModel>[] cols;
                KAddUp(k, width, height, martix, out  cols);
                //產生新平均值
                newMean(k, kv, cols, ref IsQuit);

                levelCount++;
            } while (!IsQuit && levelCount < level);

        }

        private static void pixelClassify(int k, int height, int width, ref byte[,] kv, ref byte[, ,] martix)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {


                    byte[] mRGB = new byte[3];
                    mRGB[0] = martix[y, x, 1];
                    mRGB[1] = martix[y, x, 2];
                    mRGB[2] = martix[y, x, 3];

                    byte[] kRGB = new byte[3];
                    int dif = int.MaxValue;
                    for (int i = 0; i < k; i++)
                    {
                        kRGB[0] = kv[i, 0];
                        kRGB[1] = kv[i, 1];
                        kRGB[2] = kv[i, 2];

                        int d = distance(mRGB, kRGB);
                        if (d < dif) //如果屬於該類，則將其標標記為該類編號
                        {
                            dif = d;
                            martix[y, x, 0] = (byte)i;
                        }
                    }
                }
            }
        }

        private static void KAddUp(int k, int width, int height, byte[, ,] martix, out List<ColorRGBModel>[] cols)
        {

            cols = new List<ColorRGBModel>[k];
            for (int i = 0; i < k; i++)
                cols[i] = new List<ColorRGBModel>();
            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    int KPosition = martix[y, x, 0];
                    ColorRGBModel value = new ColorRGBModel();
                    value.R = martix[y, x, 1];
                    value.G = martix[y, x, 2];
                    value.B = martix[y, x, 3];

                    cols[KPosition].Add(value);
                }
            }

        }

        private static void newMean(int k, byte[,] kv, List<ColorRGBModel>[] cols, ref bool IsQuit)
        {

            for (int i = 0; i < k; i++)
            {
                ColorRGBModel kMeanValue = mean(cols[i]);
                if (kv[i, 0] == kMeanValue.R && kv[i, 1] == kMeanValue.G && kv[i, 2] == kMeanValue.B)
                    IsQuit = true;
                else
                {
                    kv[i, 0] = kMeanValue.R;
                    kv[i, 1] = kMeanValue.G;
                    kv[i, 2] = kMeanValue.B;
                    IsQuit = false;
                }
            }
        }


        private static ColorRGBModel mean(List<ColorRGBModel> collections)
        {
            ColorRGBModel res = new ColorRGBModel();
            if (!(collections.Count > 0))
                return res;

            int r = 0, g = 0, b = 0;
            foreach (ColorRGBModel c in collections)
            {
                r += c.R;
                g += c.G;
                b += c.B;
            }

            res.R = (byte)(r / collections.Count);
            res.G = (byte)(g / collections.Count);
            res.B = (byte)(b / collections.Count);

            return res;
        }

        private static int distance(byte[] aRGB, byte[] bRGB)
        {
            int dr = (aRGB[0] - bRGB[0]) * (aRGB[0] - bRGB[0]);
            int dg = (aRGB[1] - bRGB[1]) * (aRGB[1] - bRGB[1]);
            int db = (aRGB[2] - bRGB[2]) * (aRGB[2] - bRGB[2]);

            return (int)Math.Sqrt(dr + db + dg);
        }

        private static int distance(int a, int b)
        {
            return a - b;
        }

        private static int distance(ColorRGBModel a, int k)
        {
            int dr = (a.R - k) * (a.R - k);
            int db = (a.B - k) * (a.B - k);
            int dg = (a.G - k) * (a.G - k);

            return (int)Math.Sqrt(dr + db + dg);
        }

        private static int distance(ColorRGBModel a, ColorRGBModel b)
        {
            int dr = (a.R - b.R) * (a.R - b.R);
            int db = (a.B - b.B) * (a.B - b.B);
            int dg = (a.G - b.G) * (a.G - b.G);

            return (int)Math.Sqrt(dr + db + dg);
        }

        
        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public byte[,] CenterPoints
        {
            get { return _CenterPoints; }
            set { _CenterPoints = value; }
        }
    }


}

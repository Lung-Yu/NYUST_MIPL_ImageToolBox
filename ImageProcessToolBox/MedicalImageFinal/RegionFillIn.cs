using ImageProcessToolBox.Analysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class RegionFillIn : IImageProcess
    {
        private Bitmap _srcImg;
        private byte[,] _srcMap;
        private byte[,] _travelMap;
        private int _width;
        private int _height;

        private int _regionfillStartWithVertical = 0;
        private int _regionfillEndWithVertical = 0;

        private int _regionfillStartWithHorizontal = 0;
        private int _regionfillEndWithHorizontal = 0;

        public Bitmap Process()
        {
            extraInfo();

            calcRegionRange();
            travel();

            return ImageProcessTools.writeBitmap(_travelMap);
        }

        private void extraInfo()
        {
            _width = _srcImg.Width;
            _height = _srcImg.Height;

            _srcMap = ImageProcessTools.extraPixels(_srcImg);
            _travelMap = ImageProcessTools.extraPixels(_srcImg);
        }

        private void travel()
        {
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                {
                    int tval = _travelMap[x, y];

                    //如果此資料為Mark Value 代表其已被標記為某區域，因此略過計算
                    if (tval == MarkValue)
                    {
                        continue;
                    }
                    //如果此資料為 EdageValue (邊界) 則略過
                    else if (tval == EdageValue)
                    {
                        continue;
                    }
                    else
                    {
                        //標記區域
                        if (x == 0 || y == 0)
                            markRegion(x, y, false);
                        else
                            markRegion(x, y, true);
                    }
                }
        }
        private static readonly byte MarkValue = 127;
        private static readonly byte EdageValue = 255;

        private void markRegion(int x, int y, bool isInteralRegion)
        {
            Stack<Coordinate> stack = new Stack<Coordinate>();
            List<Coordinate> candidates = new List<Coordinate>();
            stack.Push(new Coordinate(x, y));
            candidates.Add(new Coordinate(x, y));

            while (stack.Count > 0)
            {
                Coordinate p = stack.Pop();
                #region 偵測邊界(牆壁、區域邊界)
                //區域邊界
                if (p.X >= _width || p.X < 0)
                {
                    isInteralRegion = false;
                    candidates.Remove(p);
                    continue;
                }

                if (p.Y >= _height || p.Y < 0)
                {
                    isInteralRegion = false;
                    candidates.Remove(p);
                    continue;
                }

                //如果碰到牆壁則略過
                if (_travelMap[p.X, p.Y] == EdageValue)
                    continue;
                #endregion

                //向下與右下 延展
                Coordinate np1 = new Coordinate(p.X + 1, p.Y);
                Coordinate np2 = new Coordinate(p.X, p.Y + 1);
                if (!candidates.Contains(np1))
                {
                    stack.Push(np1);
                    candidates.Add(np1);
                }
                if (!candidates.Contains(np1))
                {
                    stack.Push(np2);
                    candidates.Add(np2);
                }

            }


            if (isInteralRegion)
                fillInRegion(candidates, EdageValue);
            else
                fillInRegion(candidates, MarkValue);

            // release resouce
            stack.Clear();
            stack = null;
            candidates.Clear();
            candidates = null;
        }

        private void fillInRegion(List<Coordinate> list, byte fillValue)
        {
            foreach (Coordinate p in list)
                _travelMap[p.X, p.Y] = fillValue;
        }

        private void calcRegionRange()
        {
            ProjectionFactory factory = new ProjectionFactory(_srcImg, 127);
            int[] v = factory.getVerticalProject();
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] > 0)
                {
                    _regionfillStartWithVertical = i;
                    break;
                }
            }

            for (int i = v.Length - 1; i >= 0; i--)
            {
                if (v[i] > 0)
                {
                    _regionfillEndWithVertical = i + 1;
                    break;
                }
            }

            int[] h = factory.getHorizontalProject();
            for (int i = 0; i < v.Length; i++)
            {
                if (h[i] > 0)
                {
                    _regionfillStartWithHorizontal = i;
                    break;
                }
            }

            for (int i = h.Length - 1; i >= 0; i--)
            {
                if (h[i] > 0)
                {
                    _regionfillEndWithHorizontal = i + 1;
                    break;
                }
            }
        }
        public void setResouceImage(Bitmap bitmap)
        {
            _srcImg = bitmap;
        }
    }
    class ImageProcessTools
    {
        public static byte[,] extraPixels(Bitmap bitmap)
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

        public static Bitmap writeBitmap(byte[,] martix)
        {
            int width = martix.GetUpperBound(0) + 1;
            int height = martix.GetUpperBound(1) + 1;
            Bitmap dst = new Bitmap(width, height);
            writeBitmap(dst, martix);
            return dst;
        }
        public static void writeBitmap(Bitmap bitmap, byte[,] martix)
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
    class Coordinate
    {
        public int X;
        public int Y;
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public string ToString()
        {
            return String.Format("[{0},{1}]", X, Y);
        }
    }
}

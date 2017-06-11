using ImageProcessToolBox.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class RegionFill : IImageProcess
    {
        private Bitmap _srcImg;
        private int _width;
        private int _height;
        private byte _edgeValue = 255;
        private byte _fillValue = 255;

        private byte[,] _srcMap;

        private int _regionfillStartWithVertical = 0;
        private int _regionfillEndWithVertical = 0;

        private int _regionfillStartWithHorizontal = 0;
        private int _regionfillEndWithHorizontal = 0;


        public RegionFill()
        {

        }
        public RegionFill(byte edgeVal)
        {
            _edgeValue = edgeVal;
        }

        public Bitmap Process()
        {
            _srcMap = extraPixels(_srcImg);

            _width = _srcImg.Width;
            _height = _srcImg.Height;

            calcRegionRange();
            FindFillRegion();

            Bitmap dst = new Bitmap(_width, _height);
            writeBitmap(dst, _srcMap);

            return dst;
        }


        private void FindFillRegion()
        {
            for (int x = _regionfillStartWithHorizontal; x < _regionfillEndWithHorizontal; x++)
            {
                for (int y = _regionfillStartWithVertical; y < _regionfillEndWithVertical; y++)
                {
                    if (_srcMap[x, y] == _edgeValue)
                    {
                        Coordinate p = new Coordinate(x, y+1);
                        if (fillColAtFinalPoint(p))
                            break;
                    }
                }
            }
        }

        private bool fillColAtFinalPoint(Coordinate p)
        {
            List<Coordinate> list = new List<Coordinate>();

            bool isOutSize = true;
            for (int y = p.Y; y < _regionfillEndWithVertical; y++)
            {
                if (_srcMap[p.X, y] == _edgeValue)
                {
                    isOutSize = false;
                    break;
                }

                list.Add(new Coordinate(p.X, y));
            }

            if (!isOutSize)
                fillIn(list);

            return isOutSize;
        }

        private bool fillRowAtFinalPoint(Coordinate p)
        {
            List<Coordinate> list = new List<Coordinate>();

            bool isOutSize = true;
            for (int x = p.X; x < _regionfillEndWithHorizontal; x++)
            {
                if (_srcMap[x, p.Y] == _edgeValue)
                {
                    isOutSize = false;
                    break;
                }

                list.Add(new Coordinate(x, p.Y));
            }

            if (!isOutSize)
                fillIn(list);

            return isOutSize;
        }

        private void calcRegionRange()
        {
            ProjectionFactory factory = new ProjectionFactory(_srcImg, _edgeValue - 1);
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
        private void fillIn(List<Coordinate> fillPoints)
        {
            foreach (Coordinate p in fillPoints)
            {
                _srcMap[p.X, p.Y] = _fillValue;
            }
        }


        private bool isOutOfImageSide(Coordinate p)
        {
            if (p.X <= 0 || p.X >= _width || p.Y <= 0 || p.Y >= _height)
                return true;
            else
                return false;
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _srcImg = bitmap;
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
}

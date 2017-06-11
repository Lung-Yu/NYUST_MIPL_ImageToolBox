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
        private byte[,] _travelMap;



        private int _regionfillStartWithVertical = 0;
        private int _regionfillEndWithVertical = 0;

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
            _travelMap = extraPixels(_srcImg);

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
            for (int y = _regionfillStartWithVertical; y < _regionfillEndWithVertical; y++)
            {
                for (int x = 145; x < _width; x++)
                {
                    if (_travelMap[x, y] == _edgeValue)
                        continue;

                    List<Coordinate> fillPoints = TravelPointInRegion(x, y);
                    //Console.WriteLine(x + "," + y + ":" + fillPoints.Count);
                    fillIn(fillPoints);
                }
            }


            //List<Coordinate> fillPoints = TravelPointInRegion(80, 90);
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
        }

        private byte travalVale = 127;
        private List<Coordinate> TravelPointInRegion(int x, int y)
        {
            Queue<Coordinate> traversalPoint = new Queue<Coordinate>();
            List<Coordinate> candidatePoint = new List<Coordinate>();

            Coordinate seedP = new Coordinate(x, y);
            traversalPoint.Enqueue(seedP);
            candidatePoint.Add(seedP);

            bool result = true;

            while (traversalPoint.Count > 0 )
            {
                if (traversalPoint.Count > 225)
                {
                    result = false;
                    break;
                }

                Coordinate p = traversalPoint.Dequeue();

                if (isOutOfImageSide(p))
                {
                    result = false;                 
                    break;
                }
                else if (_travelMap[p.X, p.Y] == _edgeValue)
                    continue;
                else if (_travelMap[p.X, p.Y] != travalVale)
                {
                    _travelMap[p.X, p.Y] = travalVale;

                    Coordinate[] addItems = new Coordinate[] { 
                        //new Coordinate(x - 1, y-1),
                        //new Coordinate(x, y-1),
                        //new Coordinate(x+1, y-1),

                        new Coordinate(x - 1, y),
                        new Coordinate(x + 1, y),
                        
                        //new Coordinate(x - 1, y+1),
                        new Coordinate(x, y+1),
                        //new Coordinate(x+1, y+1),                        
                    };

                    foreach (Coordinate addItem in addItems)
                    {
                        //尚未確認過之座標點，則加入候選名單並等候檢驗
                        if (!candidatePoint.Contains(addItem))
                        {
                            traversalPoint.Enqueue(addItem);
                            candidatePoint.Add(p);
                        }
                    }
                }
            }

            //clear

            traversalPoint.Clear();
            traversalPoint = null;

            foreach (Coordinate item in candidatePoint)
                _travelMap[item.X, item.Y] = 0;

            if (!result)
                candidatePoint.Clear();

            return candidatePoint;
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
            //else if (p.Y < _regionfillStartWithVertical || p.Y > _regionfillEndWithVertical)
            //    return true;
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
            public Coordinate(int x, int y)
            {
                _X = x;
                _Y = y;
            }

            int _X;

            public int X
            {
                get { return _X; }
                set { _X = value; }
            }
            int _Y;

            public int Y
            {
                get { return _Y; }
                set { _Y = value; }
            }
        }
    }
}

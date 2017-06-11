using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class RegionGrowpIn : IImageProcess
    {
        private Bitmap _srcImg;
        private List<Point> _seeds;
        private int _width;
        private int _height;
        private byte _fillColor = 255;
        private byte _targetValue;


        private bool isSetTargetValue = false;


        private byte[,] _imgMap;// x , y
        public RegionGrowpIn()
        {

        }

        public RegionGrowpIn(int x, int y, byte targetValue)
        {
            _seeds = new List<Point>();
            _seeds.Add(new Point(x, y));

            TargetValue = targetValue;
        }

        public RegionGrowpIn(int x, int y)
        {
            _seeds = new List<Point>();
            _seeds.Add(new Point(x, y));
        }
        public RegionGrowpIn(Point seed)
        {
            _seeds = new List<Point>();
            _seeds.Add(seed);
        }
        public RegionGrowpIn(List<Point> seeds, byte targetValue)
        {
            _seeds = seeds;
            TargetValue = targetValue;
        }

        public Bitmap Process()
        {
            _width = _srcImg.Width;
            _height = _srcImg.Height;
            _imgMap = extraPixels(_srcImg);

            growp();

            Bitmap dst = new Bitmap(_width, _height);
            writeBitmap(dst, _imgMap);

            return dst;
        }

        private void growp()
        {

            byte target = _targetValue;

            foreach (Point seed in _seeds)
            {
                if (!IsSetTargetValue)
                    target = _imgMap[seed.X, seed.Y];

                growp(seed,target);
                
            }
        }

        private void growp(Point seed,byte target)
        {
            int x = seed.X, y = seed.Y;
            Queue<Point> growPoints = new Queue<Point>();
            growPoints.Enqueue(seed);

            int tX, tY;
            do
            {
                Point p = growPoints.Dequeue();
                tX = p.X;
                tY = p.Y;

                if (tX < 0 || tX >= _width || tY < 0 || tY >= _height)
                    continue;
                byte compVal = _imgMap[tX, tY];
                if (compVal == target)
                {
                    _imgMap[tX, tY] = _fillColor;

                    growPoints.Enqueue(new Point(tX + 1, tY - 1));
                    growPoints.Enqueue(new Point(tX - 1, tY - 1));
                    growPoints.Enqueue(new Point(tX, tY - 1));

                    growPoints.Enqueue(new Point(tX + 1, tY));
                    growPoints.Enqueue(new Point(tX - 1, tY));

                    growPoints.Enqueue(new Point(tX + 1, tY + 1));
                    growPoints.Enqueue(new Point(tX - 1, tY + 1));
                    growPoints.Enqueue(new Point(tX, tY + 1));


                }
            } while (growPoints.Count != 0);
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

        public byte TargetValue
        {
            get { return _targetValue; }
            set
            {
                //IsSetTargetValue = true;
                _targetValue = value;
            }
        }

        public bool IsSetTargetValue
        {
            get { return isSetTargetValue; }
            set { isSetTargetValue = value; }
        }
        public void setResouceImage(Bitmap bitmap)
        {
            _srcImg = bitmap;
        }
    }
}

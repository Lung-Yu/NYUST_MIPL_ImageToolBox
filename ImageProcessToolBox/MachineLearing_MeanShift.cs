using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MachineLearing_MeanShift
    {
        private Bitmap _srcImg;

        private int _distance = 0;
        private int _targetVal = 26;
        private int _IterationLevel = 100;

        private Point _center = new Point(-1, -1);
        private int _width = 0;
        private int _height = 0;
        private byte[, ,] _imgMap;

        public MachineLearing_MeanShift(int hDistance, int targetValue)
        {
            _distance = hDistance;
            _targetVal = targetValue;
        }
        private void setDefalut()
        {
            _center = new Point();
            _center.X = _srcImg.Width / 2;
            _center.Y = _srcImg.Height / 2;
        }

        public void Process()
        {
            int width = _srcImg.Width;
            int height = _srcImg.Height;

            _imgMap = pre(_srcImg);  //取得圖像矩陣

            shif2Center();
        }

        private void shif2Center()
        {
            if (IsCenterOutofImageMartix(_center))
                setDefalut();

            int executeCount = 0;

            Point shiftVector = new Point(0, 0);
            do
            {
                shiftVector = new Point(0, 0);
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        int d = (int)Math.Sqrt((x - _center.X) * (x - _center.X) + (y - _center.Y) * (y - _center.Y));
                        int temTarget = _imgMap[y, x, 0];
                        if (d <= _distance && temTarget != 0)
                        {
                            Point p = new Point(x, y);

                            int shiftX = p.X - _center.X;
                            int shiftY = p.Y - _center.Y;

                            shiftVector.X = (shiftX + shiftVector.X) / 2;
                            shiftVector.Y = (shiftY + shiftVector.Y) / 2;
                        }
                    }
                }

                _center.X = (_center.X + shiftVector.X);
                _center.Y = (_center.Y + shiftVector.Y);

            } while (++executeCount < _IterationLevel && isShift(shiftVector));

            Console.WriteLine(_center);
        }

        private bool isShift(Point shiftVector)
        {
            if (shiftVector.X == 0 && shiftVector.Y == 0)
                return false;
            else
                return true;
        }

        private bool IsCenterOutofImageMartix(Point centor)
        {
            if (centor.X > _width || centor.X < 0 || centor.Y < 0 || centor.Y > _height)
                return true;
            if (_width == 0 || _height == 0)
                return true;
            return false;
        }


        private static byte[, ,] pre(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[, ,] resMatrix = new byte[height, width, 1];

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
                        resMatrix[y, x, 0] = *(srcP + 2);
                        //resMatrix[y, x, 1] = *(srcP + 1);
                        //resMatrix[y, x, 2] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            return resMatrix;
        }
        public void setResouceImage(System.Drawing.Bitmap bitmap)
        {
            _width = bitmap.Width;
            _height = bitmap.Height;
            _srcImg = bitmap;
        }
        public Point Center
        {
            get { return _center; }
            set { _center = value; }
        }

        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public int IterationLevel
        {
            get { return _IterationLevel; }
            set { _IterationLevel = value; }
        }
    }
}

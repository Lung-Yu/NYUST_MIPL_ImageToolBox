using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MachineLearing_MeanShift : IImageProcess
    {
        private Bitmap _srcImg;
        private int _distance = 0;
        private int _targetVal = 26;
        private int limit = 10;
        private Point center;
        public MachineLearing_MeanShift(int hDistance)
        {
            _distance = hDistance;
            //centers = new Point[1];
        }

        public Bitmap Process()
        {
            center = new Point();
            center.X = _srcImg.Width / 2;
            center.Y = _srcImg.Height / 2;

            int width = _srcImg.Width;
            int height = _srcImg.Height;

            byte[, ,] martix = pre(_srcImg);  //取得圖像矩陣


            int executeCount = 0;
            

            do
            {
                Point shiftVector = new Point(0, 0);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int d = (int)Math.Sqrt((x - center.X) * (x - center.X) + (y - center.Y) * (y - center.Y));
                        if (d <= _distance && martix[y,x,0]==_targetVal)
                        {
                            Point p = new Point(x, y);
                            shiftVector = calDistance(p, shiftVector);
                        }
                    }
                }

                center.X = center.X + shiftVector.X;
                center.Y = center.Y + shiftVector.Y;

            } while (++executeCount < limit);

            Console.WriteLine(String.Format("centor :: x={0},y={1}",center.X,center.Y));
            return new Bitmap(_srcImg);
        }



        private Point calDistance(Point p1, Point p2)
        {
            Point res = new Point();
            res.X = (p1.X - p2.X)/2;
            res.Y = (p1.Y - p2.Y)/2;
            return res;
        }
        private static byte[, ,] pre(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[, ,] resMatrix = new byte[height, width, 4];

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
                        resMatrix[y, x, 1] = *(srcP + 1);
                        resMatrix[y, x, 2] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);
            return resMatrix;
        }
        public void setResouceImage(System.Drawing.Bitmap bitmap)
        {
            _srcImg = bitmap;
        }
    }
}

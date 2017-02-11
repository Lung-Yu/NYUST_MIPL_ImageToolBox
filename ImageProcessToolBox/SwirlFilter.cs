using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class SwirlFilter : IImageProcess
    {
        //https://www.kancloud.cn/trent/hotoimagefilter/102810
        private Bitmap _SourceImage;
        public SwirlFilter()
        {

        }
        public SwirlFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }


        public System.Drawing.Bitmap Process()
        {
            return SwirlFilterProcess(_SourceImage, _SourceImage.Width / 2, _SourceImage.Height / 2, 25);
        }

        private Bitmap SwirlFilterProcess(Bitmap srcBitmap, int cenX, int cenY, int swilDegree)
        {
            Bitmap a = new Bitmap(srcBitmap);
            int w = a.Width;
            int h = a.Height;
            int radius = 0;
            swilDegree = Math.Max(0, Math.Min(360, swilDegree));
            double k = swilDegree / 3600.0;
            Bitmap dst = new Bitmap(w, h);
            System.Drawing.Imaging.BitmapData srcData = a.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData dstData = dst.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                byte* p = null;
                int stride = srcData.Stride - w * 4;
                int offsetX = 0, offsetY = 0;
                int newX = 0, newY = 0;
                double radian = 0;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        offsetX = x - cenX;
                        offsetY = y - cenY;
                        radian = Math.Atan2(offsetY, offsetX);
                        radius = (int)(Math.Sqrt(offsetX * offsetX + offsetY * offsetY));
                        newX = (int)(radius * Math.Cos(radian + k * radius)) + cenX;
                        newY = (int)(radius * Math.Sin(radian + k * radius)) + cenY;
                        newX = Math.Min(w - 1, Math.Max(0, newX));
                        newY = Math.Min(h - 1, Math.Max(0, newY));
                        p = pIn + newY * srcData.Stride + newX * 4;
                        pOut[0] = (byte)p[0];
                        pOut[1] = (byte)p[1];
                        pOut[2] = (byte)p[2];
                        pOut[3] = (byte)255;
                        pOut += 4;
                    }
                    pOut += stride;
                }
                a.UnlockBits(srcData);
                dst.UnlockBits(dstData);
            }
            return dst;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

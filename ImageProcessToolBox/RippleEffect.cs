using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    //https://dotblogs.com.tw/junegoat/2012/08/23/c-sharp-ripple-effect
    //https://www.codeproject.com/articles/3419/image-processing-for-dummies-with-c-and-gdi-part-5
    class RippleEffect : IImageProcess
    {
        private Bitmap _ImageSource;
        private short _Wave = 15;
        public RippleEffect()
        {
          
        }
        public RippleEffect(short wave)
        {
            _Wave = wave;
        }
        public RippleEffect(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
        public RippleEffect(Bitmap bitmap, short wave)
        {
            _ImageSource = bitmap;
            _Wave = wave;
        }


        public Bitmap Process()
        {
            return effectProcess(_ImageSource, _Wave);
        }
         
        private struct FloatPoint
        {
            public int X;
            public int Y;
        }

        private Bitmap effectProcess(Bitmap src, short wave)
        {
            int width = src.Width;
            int height = src.Height;
            FloatPoint[,] fp = sampling(width, height, wave);

            //進行合成
            Bitmap bSrc = (Bitmap)src.Clone();

            // 依照 Format24bppRgb 每三個表示一 Pixel 0: 藍 1: 綠 2: 紅
            BitmapData bitmapData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int scanline = bitmapData.Stride;
            IntPtr Scan0 = bitmapData.Scan0;
            IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = bitmapData.Stride - src.Width * 3;
                int xOffset, yOffset;
                for (int y = 0; y < height; y++, p += nOffset)
                {
                    for (int x = 0; x < width; x++, p += 3)
                    {
                        xOffset = fp[x, y].X;
                        yOffset = fp[x, y].Y;
                        if (yOffset >= 0 && yOffset < height && xOffset >= 0 && xOffset < width)
                        {
                            p[0] = pSrc[(yOffset * scanline) + (xOffset * 3)];
                            p[1] = pSrc[(yOffset * scanline) + (xOffset * 3) + 1];
                            p[2] = pSrc[(yOffset * scanline) + (xOffset * 3) + 2];
                        }
                    }
                }
            }
            src.UnlockBits(bitmapData);
            bSrc.UnlockBits(bmSrc);
            return src;
        }

        private static FloatPoint[,] sampling(int width, int height, short wave)
        {
            FloatPoint[,] fp = new FloatPoint[width, height];
            double xo, yo, newX, newY;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    xo = ((double)wave * Math.Sin(2.0 * 3.1415 * (float)y / 128.0));
                    yo = ((double)wave * Math.Cos(2.0 * 3.1415 * (float)x / 128.0));
                    newX = (x + xo);
                    newY = (y + yo);

                    fp[x, y].X = (int)((newX > 0 && newX < width) ? newX : 0);
                    fp[x, y].Y = (int)((newY > 0 && newY < height) ? newY : 0);
                }

            return fp;
        }




        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
    }
}

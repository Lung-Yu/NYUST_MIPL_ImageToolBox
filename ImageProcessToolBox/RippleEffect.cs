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
        private short _Wave = 5;
        public RippleEffect(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
        public RippleEffect(Bitmap bitmap,short wave)
        {
            _ImageSource = bitmap;
            _Wave = wave;
        }


        public Bitmap Process()
        {
            return AdjustRippleEffect(_ImageSource, _Wave);
        }

        private struct FloatPoint{
            public double X;
            public double Y;
        }

        public Bitmap AdjustRippleEffect(Bitmap src, short nWave)
        {            
            int nWidth = src.Width;
            int nHeight = src.Height;
            // 透過公式進行水波紋的採樣
            FloatPoint[,] fp = new FloatPoint[nWidth, nHeight];
            Point[,] pt = new Point[nWidth, nHeight];            
            Point mid = new Point();
            mid.X = nWidth / 2;
            mid.Y = nHeight / 2;            
            double newX, newY;
            double xo, yo;            
            //先取樣將水波紋座標跟RGB取出
            for (int x = 0; x < nWidth; ++x)
                for (int y = 0; y < nHeight; ++y)
                {
                    xo = ((double)nWave * Math.Sin(2.0 * 3.1415 * (float)y / 128.0));
                    yo = ((double)nWave * Math.Cos(2.0 * 3.1415 * (float)x / 128.0));
                    newX = (x + xo);
                    newY = (y + yo);
                    if (newX > 0 && newX < nWidth){
                        fp[x, y].X = newX;
                        pt[x, y].X = (int)newX;
                    }else{
                        fp[x, y].X = 0.0;
                        pt[x, y].X = 0;
                    }
                    
                    if (newY > 0 && newY < nHeight)
                    {
                        fp[x, y].Y = newY;
                        pt[x, y].Y = (int)newY;
                    }else{
                        fp[x, y].Y = 0.0;
                        pt[x, y].Y = 0;
                    }
                }

            //進行合成
            Bitmap bSrc = (Bitmap)src.Clone();
            // 依照 Format24bppRgb 每三個表示一 Pixel 0: 藍 1: 綠 2: 紅

            BitmapData bitmapData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite,PixelFormat.Format24bppRgb);
            int scanline = bitmapData.Stride;
            IntPtr Scan0 = bitmapData.Scan0;
            IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            { 
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = bitmapData.Stride - src.Width * 3;
                int xOffset, yOffset;
                for (int y = 0; y < nHeight; ++y){
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = pt[x, y].X;
                        yOffset = pt[x, y].Y;
                        if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                        {
                            p[0] = pSrc[(yOffset * scanline) + (xOffset * 3)];
                            p[1] = pSrc[(yOffset * scanline) + (xOffset * 3) + 1];
                            p[2] = pSrc[(yOffset * scanline) + (xOffset * 3) + 2];
                        }
                        p += 3;
                    }
                    p += nOffset;
                }
            }
            src.UnlockBits(bitmapData);
            bSrc.UnlockBits(bmSrc);
            return src;
        }
    }
}

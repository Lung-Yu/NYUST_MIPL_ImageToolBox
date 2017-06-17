using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class LomoFilter : IImageProcess
    {
        //https://www.kancloud.cn/trent/hotoimagefilter/102803
        private Bitmap _SourceImage;
        public LomoFilter()
        {
            
        }

        public LomoFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        private Bitmap LOMOFilterProcess(Bitmap src)
        {
            Bitmap srcBitmap = new Bitmap(src);
            Bitmap dst = new Bitmap(src);
            int w = dst.Width;
            int h = dst.Height;
            BitmapData dstData = dst.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData srcData = srcBitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* pSrc = (byte*)srcData.Scan0;
                byte* pDst = (byte*)dstData.Scan0;
                int offset = dstData.Stride - w * 4;
                int r, g, b;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        b = ModeSmoothLight(pSrc[0], pDst[0]);
                        g = ModeSmoothLight(pSrc[1], pDst[1]);
                        r = ModeSmoothLight(pSrc[2], pDst[2]);
                        b = ModeExclude(b, 80);
                        g = ModeExclude(g, 15);
                        r = ModeExclude(r, 5);
                        pDst[0] = (byte)b;
                        pDst[1] = (byte)g;
                        pDst[2] = (byte)r;
                        pDst[3] = (byte)255;
                        pSrc += 4;
                        pDst += 4;
                    }
                    pSrc += offset;
                    pDst += offset;
                }
            }
            dst.UnlockBits(dstData);
            srcBitmap.UnlockBits(srcData);
            return dst;
        }
        private int ModeSmoothLight(int basePixel, int mixPixel)
        {
            int res = 0;
            res = mixPixel > 128 ? ((int)((float)basePixel + ((float)mixPixel + (float)mixPixel - 255.0f) * ((Math.Sqrt((float)basePixel / 255.0f)) * 255.0f - (float)basePixel) / 255.0f)) :
                  ((int)((float)basePixel + ((float)mixPixel + (float)mixPixel - 255.0f) * ((float)basePixel - (float)basePixel * (float)basePixel / 255.0f) / 255.0f));
            return Math.Min(255, Math.Max(0, res));
        }

        private int ModeExclude(int basePixel, int mixPixel)
        {
            int res = 0;
            res = (mixPixel + basePixel) - mixPixel * basePixel / 128;
            return Math.Min(255, Math.Max(0, res));
        }

        public Bitmap Process()
        {
            return LOMOFilterProcess(_SourceImage);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

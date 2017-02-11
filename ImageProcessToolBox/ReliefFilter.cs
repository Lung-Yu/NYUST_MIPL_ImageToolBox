using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class ReliefFilter : IImageProcess
    {
        private Bitmap _SourceImage;

        public ReliefFilter()
        {

        }

        public ReliefFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
        
        public System.Drawing.Bitmap Process()
        {
            return AdjustToStone(_SourceImage);
        }

        public Bitmap AdjustToStone(Bitmap src)
        {

            // 依照 Format24bppRgb 每三個表示一 Pixel 0: 藍 1: 綠 2: 紅
            BitmapData bitmapData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                // 抓住第一個 Pixel 第一個數值
                byte* p = (byte*)(void*)bitmapData.Scan0; 

                // 跨步值 - 寬度 *3 可以算出畸零地 之後跳到下一行
                int nOffset = bitmapData.Stride - src.Width * 3;



                for (int y = 0; y < src.Height; ++y)
                {
                    for (int x = 0; x < src.Width; ++x)
                    {
                        // 為了理解方便 所以特地在命名
                        int r, g, b;
                        // 先取得下一個 Pixel
                        var q = p + 3;
                        r = Math.Abs(p[2] - q[2] + 128);
                        r = r < 0 ? 0 : r;
                        r = r > 255 ? 255 : r;
                        p[2] = (byte)r;

                        g = Math.Abs(p[1] - q[1] + 128);
                        g = g < 0 ? 0 : g;
                        g = g > 255 ? 255 : g;
                        p[1] = (byte)g;

                        b = Math.Abs(p[0] - q[0] + 128);
                        b = b < 0 ? 0 : b;
                        b = b > 255 ? 255 : b;
                        p[0] = (byte)b;

                        // 跳去下一個 Pixel
                        p += 3;

                    }
                    // 跨越畸零地
                    p += nOffset;
                }
            }
            src.UnlockBits(bitmapData);
            return src;


        }






        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

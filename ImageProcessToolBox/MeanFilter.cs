using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MeanFilter : IImageProcess
    {
        private Bitmap _SourceImage;
        private int _MaskWidth = 3;
        private int _MaskHeight = 3;


        public MeanFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return filter(_SourceImage, MaskWidth, MaskHeight);
        }

        private Bitmap filter(Bitmap bitmap, int maskWidth, int maskHeight)
        {
            byte[,] pix, resPix;
            int width = bitmap.Width, height = bitmap.Height, pos, count = maskWidth * maskHeight, current;
            Bitmap dstBitmap = ImageExtract.extract(bitmap, out pix, out resPix);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pos = x + y * width;
                    if (!ImageProcess.IsFilterOnSide(ref pix, ref resPix, width, height, x, y, pos))
                    {
                        current = x + y * width;
                        int[] sum = { 0, 0, 0 };
                        for (int my = 0; my < maskHeight; my++)
                            for (int mx = 0; mx < maskWidth; mx++)
                            {
                                pos = current + (mx - 1) + ((my - 1) * width);
                                sum[0] += pix[0, pos];
                                sum[1] += pix[1, pos];
                                sum[2] += pix[2, pos];
                            }

                        resPix[0, current] = (byte)(sum[0] / count);
                        resPix[1, current] = (byte)(sum[1] / count);
                        resPix[2, current] = (byte)(sum[2] / count);
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }

        public int MaskWidth
        {
            get { return _MaskWidth; }
            set { _MaskWidth = value; }
        }
        
        public int MaskHeight
        {
            get { return _MaskHeight; }
            set { _MaskHeight = value; }
        }
    }
}

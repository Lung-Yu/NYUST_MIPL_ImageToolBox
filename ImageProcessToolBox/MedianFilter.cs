using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MedianFilter : IImageProcess
    {
        private Bitmap _ImageSource;
        public MedianFilter(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }

        public Bitmap Process()
        {
            return Process(_ImageSource);
        }
        private Bitmap Process(Bitmap bitmap)
        {
            return filter(bitmap,3,3);
        }
        private Bitmap median(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            int w = 3, h = 3;
            Bitmap dstBitmap = new Bitmap(bitmap);

            byte[,] pix = ImageExtract.getimageArray(bitmap);
            byte[,] resPix = new byte[3, width * height];

            for (int y = 1; y < (height - 1); y++)
            {
                for (int x = 1; x < (width - 1); x++)
                {
                    //b,g,r 
                    for (int c = 0; c < 3; c++)
                    {
                        //mask
                        int current = x + y * width;
                        byte[] mask = new byte[w * h];
                        for (int my = 0; my < h; my++)
                            for (int mx = 0; mx < w; mx++)
                            {
                                int pos = current + (mx - 1) + ((my - 1) * width);
                                mask[mx + my * w] = pix[c, pos];
                            }

                        Heap heap = new Heap(mask, mask.Length);
                        heap.heapsort();
                        resPix[c, current] = (byte)heap.get()[(w * h) / 2];
                    }
                }
            }


            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }
        

        private Bitmap filter(Bitmap bitmap, int maskWidth, int maskHeight)
        {
            byte[,] pix, resPix;
            int width = bitmap.Width, height = bitmap.Height, pos, current;
            Bitmap dstBitmap = ImageExtract.extract(bitmap, out pix, out resPix);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pos = x + y * width;
                    if (!ImageProcess.IsFilterOnSide(ref pix, ref resPix, width, height, x, y, pos))
                    {
                        for (int c = 0; c < 3; c++)
                        {
                            current = x + y * width;
                            byte[] mask = new byte[maskHeight * maskWidth];
                            for (int my = 0; my < maskHeight; my++)
                                for (int mx = 0; mx < maskWidth; mx++)
                                {
                                    pos = current + (mx - 1) + ((my - 1) * width);
                                    mask[mx + my * maskWidth] = pix[c, pos];
                                }
                            Heap heap = new Heap(mask, mask.Length);
                            heap.heapsort();
                            resPix[c, current] = (byte)heap.get()[(maskWidth * maskHeight) / 2];
                        }
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }
    }
}

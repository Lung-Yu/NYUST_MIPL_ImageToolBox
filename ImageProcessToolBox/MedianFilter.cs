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
            int width = bitmap.Width, height = bitmap.Height;
            int w = 3, h = 3;
            //IComparer revComparer = new ReverseComparer();
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
    }
}

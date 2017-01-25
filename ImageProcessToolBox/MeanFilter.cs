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
            return meanFilter(_SourceImage,MaskWidth,MaskHeight);
        }
       
        private Bitmap meanFilter(Bitmap bitmap, int maskWidth, int maskHeight)
        {
            int width = bitmap.Width, height = bitmap.Height;
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
                        byte[] mask = new byte[maskWidth * maskHeight];
                        for (int my = 0; my < maskHeight; my++)
                            for (int mx = 0; mx < maskWidth; mx++)
                            {
                                int pos = current + (mx - 1) + ((my - 1) * width);
                                mask[mx + my * maskWidth] = pix[c, pos];
                            }

                        resPix[c, current] = calcMeans(mask);
                    }
                }
            }

            ImageExtract.writeImageByArray(resPix, dstBitmap);
            return dstBitmap;
        }

        private static byte calcMeans(byte[] mask)
        {
            int sum = 0, size = mask.Length;
            for (int i = 0; i < size; i++)
                sum += mask[i];
            return (byte)(sum / size);
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

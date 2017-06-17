using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class DiscreteWaveletTransformation : IImageProcess
    {
        private Bitmap _ImageSource;

        public Bitmap Process()
        {
            Bitmap horizontalImage = horizontal(_ImageSource);
            Bitmap verticalImage = vertical(horizontalImage);
            horizontalImage.Dispose();

            return verticalImage;
        }

        private Bitmap vertical(Bitmap srcBitmap)
        {
            Bitmap dstBitmap = new Bitmap(srcBitmap);
            int width = srcBitmap.Width, height = srcBitmap.Height;
            byte[,] pixels = ImageExtract.getimageArray(srcBitmap);
            byte[,] result = new byte[3, width * height];
            int x, y, offset;
            int r, g, b, halfWidth = width / 2;
            for (y = 0; y < height - (height%2); y++)
            {
                for (x = 0; x < width - (width%2); x += 2)
                {
                    offset = y * width;

                    r = pixels[ImageExtract.COLOR_R, x + offset] + pixels[ImageExtract.COLOR_R, x + 1 + offset];
                    result[ImageExtract.COLOR_R, x / 2 + offset] = (byte)((r > 255) ? 255 : (r < 0) ? 0 : r);
                    g = pixels[ImageExtract.COLOR_G, x + offset] + pixels[ImageExtract.COLOR_G, x + 1 + offset];
                    result[ImageExtract.COLOR_G, x / 2 + offset] = (byte)((g > 255) ? 255 : (g < 0) ? 0 : g);
                    b = pixels[ImageExtract.COLOR_B, x + offset] + pixels[ImageExtract.COLOR_B, x + 1 + offset];
                    result[ImageExtract.COLOR_B, x / 2 + offset] = (byte)((b > 255) ? 255 : (b < 0) ? 0 : b);

                    r = pixels[ImageExtract.COLOR_R, x + offset] - pixels[ImageExtract.COLOR_R, x + 1 + offset];
                    result[ImageExtract.COLOR_R, x / 2 + halfWidth + offset] = (byte)((r > 255) ? 255 : (r < 0) ? 0 : r);
                    g = pixels[ImageExtract.COLOR_G, x + offset] - pixels[ImageExtract.COLOR_G, x + 1 + offset];
                    result[ImageExtract.COLOR_G, x / 2 + halfWidth + offset] = (byte)((g > 255) ? 255 : (g < 0) ? 0 : g);
                    b = pixels[ImageExtract.COLOR_B, x + offset] - pixels[ImageExtract.COLOR_B, x + 1 + offset];
                    result[ImageExtract.COLOR_B, x / 2 + halfWidth + offset] = (byte)((b > 255) ? 255 : (b < 0) ? 0 : b);
                }
            }

            ImageExtract.writeImageByArray(result, dstBitmap);

            return dstBitmap;
        }

        private Bitmap horizontal(Bitmap srcBitmap)
        {
            Bitmap dstBitmap = new Bitmap(srcBitmap);
            int width = srcBitmap.Width, height = srcBitmap.Height;
            byte[,] pixels = ImageExtract.getimageArray(srcBitmap);
            byte[,] result = new byte[3, width * height];
            int x, y, offset, halfOffset, halfHeightOffset, halfHeight = height / 2;
            int r, g, b;
            for (y = 0; y < height - (height % 2); y += 2)
            {
                for (x = 0; x < width - (width % 2); x++)
                {
                    offset = y * width;
                    halfOffset = (y / 2) * width;
                    halfHeightOffset = halfHeight * width;

                    r = pixels[ImageExtract.COLOR_R, x + offset + width] + pixels[ImageExtract.COLOR_R, x + offset + width];
                    result[ImageExtract.COLOR_R, x + halfOffset] = (byte)((r > 255) ? 255 : (r < 0) ? 0 : r);
                    g = pixels[ImageExtract.COLOR_G, x + offset + width] + pixels[ImageExtract.COLOR_G, x + offset + width];
                    result[ImageExtract.COLOR_G, x + halfOffset] = (byte)((g > 255) ? 255 : (g < 0) ? 0 : g);
                    b = pixels[ImageExtract.COLOR_B, x + offset + width] + pixels[ImageExtract.COLOR_B, x + offset + width];
                    result[ImageExtract.COLOR_B, x + halfOffset] = (byte)((b > 255) ? 255 : (b < 0) ? 0 : b);


                    r = pixels[ImageExtract.COLOR_R, x + offset] - pixels[ImageExtract.COLOR_R, x + offset + width];
                    result[ImageExtract.COLOR_R, x + halfOffset + halfHeightOffset] = (byte)((r > 255) ? 255 : (r < 0) ? 0 : r);
                    g = pixels[ImageExtract.COLOR_G, x + offset] - pixels[ImageExtract.COLOR_G, x + offset + width];
                    result[ImageExtract.COLOR_G, x + halfOffset + halfHeightOffset] = (byte)((g > 255) ? 255 : (g < 0) ? 0 : g);
                    b = pixels[ImageExtract.COLOR_B, x + offset] - pixels[ImageExtract.COLOR_B, x + offset + width];
                    result[ImageExtract.COLOR_B, x + halfOffset + halfHeightOffset] = (byte)((b > 255) ? 255 : (b < 0) ? 0 : b);
                }
            }

            ImageExtract.writeImageByArray(result, dstBitmap);

            return dstBitmap;
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
    }
}

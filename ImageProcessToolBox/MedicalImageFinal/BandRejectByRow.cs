using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Analysis
{
    class BandRejectByRow :IImageProcess
    {
        private Bitmap _srcImage;
        private static int SegmationSize = 6;
        private byte _rejectVal = 0;
        public BandRejectByRow(byte rejectVal)
        {
            _rejectVal = rejectVal;
        }

        public Bitmap Process()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_srcImage, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

            int limit = width - (width / SegmationSize);
            unsafe
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = dstBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    int count = 0;
                    int size = 0;
                    for (int x = 0; x < width; x++, srcP += 3, dstP += 3)
                    {
                        if (srcP[ImageExtract.COLOR_R] == _rejectVal)
                            count++;
                        size++;
                    }

                    if (count > limit)
                    {
                        dstP = dstP - (3 * size);
                        for (int x = 0; x < width; x++, dstP += 3)
                            dstP[0] = dstP[1] = dstP[2] = 0;
                    }


                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            _srcImage.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _srcImage = bitmap;
        }
    }
}

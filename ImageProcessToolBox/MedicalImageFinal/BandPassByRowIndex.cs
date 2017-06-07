using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class BandPassByRowIndex : IImageProcess
    {
        private Bitmap _srcImage;
        private int _rejectStart = 0;
        private int _rejectEnd = 0;

        public BandPassByRowIndex(int rejectStart, int rejectEnd)
        {
            _rejectStart = rejectStart;
            _rejectEnd = rejectEnd;
        }

        public Bitmap Process()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_srcImage, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);


            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = dstBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3, dstP += 3)
                    {
                        if (x >= _rejectStart && x <= _rejectEnd)
                            dstP[0] = dstP[1] = dstP[2] = srcP[2];
                        else
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

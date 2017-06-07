using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class ShitEdge : IImageProcess
    {
        private Bitmap _srcImage;
        public System.Drawing.Bitmap Process()
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

                byte tempVal = 0;
                for (int y = 0; y < height; y++)
                {

                    tempVal = srcP[ImageExtract.COLOR_R];
                    for (int x = 0; x < width; x++, srcP += 3, dstP += 3)
                    {
                        int t = srcP[ImageExtract.COLOR_R] - tempVal;
                        dstP[0] = dstP[1] = dstP[2] = (byte)((t < 0) ? 0 : t);
                        tempVal = srcP[ImageExtract.COLOR_R];
                    }

                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            _srcImage.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        public void setResouceImage(System.Drawing.Bitmap bitmap)
        {
            _srcImage = bitmap;
        }
    }
}

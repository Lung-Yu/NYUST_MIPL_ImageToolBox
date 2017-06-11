using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class ImageHighLight : IImageProcess
    {
        private Bitmap _srcImage;
        private Point[] _targets;

        public ImageHighLight(Point[] targets)
        {
            _targets = targets;
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


                        bool isHightLight = false;
                        foreach (Point item in _targets)
                        {
                            if (x == item.X && y == item.Y)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    int offset = i * 3;
                                    dstP[0 + offset] = 245;
                                    dstP[1 + offset] = 255;
                                    dstP[2 + offset] = 255;
                                }
                                isHightLight = true;
                            }
                        }

                        if (!isHightLight)
                        {
                            
                            dstP[0] = srcP[0];
                            dstP[1] = srcP[1];
                            dstP[2] = srcP[2];
                        }
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

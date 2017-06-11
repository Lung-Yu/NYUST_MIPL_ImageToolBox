using ImageProcessToolBox.Analysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class MakeImageFrame
    {
        private Bitmap _srcImage;
        private Bitmap _frameImg;

        private byte[,] _frameImageMap;
        private byte[,] _srcImageMap;


        public MakeImageFrame(Bitmap src, Bitmap frame)
        {
            IImageProcess action = new Laplacian();
            action.setResouceImage(frame);
            _frameImg = action.Process();


            _frameImageMap = new byte[src.Width, src.Height];
            _srcImageMap = new byte[src.Width, src.Height];
            _srcImage = src;
            _extraFrameImage();
        }

        private void _extraSrcImage()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            System.IntPtr srcScan;
            BitmapData srcBmData;
            ImageExtract.InitPonitMethod(_srcImage, width, height, out srcScan, out srcBmData);

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        _srcImageMap[x, y] = srcP[2];
                    }
                    srcP += srcOffset;
                }
            }
            _srcImage.UnlockBits(srcBmData);
        }

        private void _extraFrameImage()
        {
            int width = _frameImg.Width;
            int height = _frameImg.Height;

            System.IntPtr srcScan;
            BitmapData srcBmData;
            ImageExtract.InitPonitMethod(_frameImg, width, height, out srcScan, out srcBmData);

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        //byte gray = (byte)(.299 * srcP[2] + .587 * srcP[1] + .114 * srcP[0]);
                        _frameImageMap[x, y] = srcP[2];
                    }
                    srcP += srcOffset;
                }
            }
            _frameImg.UnlockBits(srcBmData);
        }

        public Bitmap Process()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_srcImage, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);


            unsafe
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = dstBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3, dstP += 3)
                    {
                        if (_frameImageMap[x, y] > 0)
                        {
                            dstP[0] = 45;
                            dstP[1] = 45;
                            dstP[2] = 255;
                        }
                        else
                        {
                            dstP[0] = dstP[1] = dstP[2] = srcP[ImageExtract.COLOR_R];
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
    }
}

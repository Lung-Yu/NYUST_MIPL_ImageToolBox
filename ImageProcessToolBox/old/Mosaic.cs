using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Mosaic : IImageProcess
    {
        private Bitmap _SourceImage;
        private int _EffectWidth = 5;
        public Mosaic()
        {

        }

        public Mosaic(int effectWidth)
        {
            _EffectWidth = effectWidth;
        }

        public Mosaic(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Mosaic(int effectWidth, Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _EffectWidth = effectWidth;
        }

        public Bitmap Process()
        {
            //return AdjustTobMosaic(_SourceImage, _EffectWidth);
            return mosaic(_SourceImage, _EffectWidth);
            //return test(_EffectWidth);
        }


        private Bitmap test(int effect)
        {
            int width = _SourceImage.Width;
            int height = _SourceImage.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_SourceImage, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);

            int offset_width = effect ;
            int offset_height = effect;
            int mask_size = effect * effect;
            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = dstBmData.Stride - width * 3;



                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //*(dstP + ImageExtract.COLOR_R) = processColorR(srcP[ImageExtract.COLOR_R], srcP[ImageExtract.COLOR_G], srcP[ImageExtract.COLOR_B]);
                        if ((x < offset_width) || (x == (width - offset_width)) || (y < offset_height) || (y >= (height - offset_height)))
                            continue;

                        int sum_r = 0, sum_g = 0, sum_b = 0;

                        for (int my = 0; my < effect; my++)
                        {
                            for (int mx = 0; mx < effect; mx++)
                            {
                                int cu_y = (my - offset_height);
                                int cu_x = (mx - offset_width) * 3;

                                sum_r += *(srcP + (cu_x) + (cu_y * (width * 3 + srcOffset)) + ImageExtract.COLOR_B);
                                sum_g += *(srcP + (cu_x) + (cu_y * (width * 3 + srcOffset)) + ImageExtract.COLOR_G);
                                sum_b += *(srcP + (cu_x) + (cu_y * (width * 3 + srcOffset)) + ImageExtract.COLOR_R);
                            }
                        }

                        for (int my = 0; my < effect; my++)
                        {
                            for (int mx = 0; mx < effect; mx++)
                            {
                                int cu_y = (my - offset_height);
                                int cu_x = (mx - offset_width) * 3;

                                *(dstP + ImageExtract.COLOR_B) = (byte)(sum_r / mask_size);
                                *(dstP + ImageExtract.COLOR_B) = (byte)(sum_g / mask_size);
                                *(dstP + ImageExtract.COLOR_B) = (byte)(sum_b / mask_size);
                            }
                        }

                        srcP += (effect * 3);
                        dstP += (effect * 3);
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            _SourceImage.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }
        private Bitmap mosaic(Bitmap bitmap, int effect)
        {
            int width = bitmap.Width, height = bitmap.Height, count = effect * effect, offset = (effect / 2) + (effect % 2);
            Bitmap dstBitmap = new Bitmap(bitmap);

            byte[,] pix = ImageExtract.getimageArray(bitmap);
            byte[,] resPix = new byte[3, width * height];
            for (int y = offset; y < (height - offset); y += effect)
            {
                for (int x = offset; x < (width - offset); x += effect)
                {
                    //mask
                    int current = x + y * width;
                    int[] sum = { 0, 0, 0 };
                    for (int my = 0; my < effect; my++)
                        for (int mx = 0; mx < effect; mx++)
                        {
                            int pos = current + (mx - 1) + ((my - 1) * width);
                            sum[0] += pix[0, pos];
                            sum[1] += pix[1, pos];
                            sum[2] += pix[2, pos];
                        }

                    sum[0] = (byte)(sum[0] / count);
                    sum[1] = (byte)(sum[1] / count);
                    sum[2] = (byte)(sum[2] / count);
                    for (int my = 0; my < effect; my++)
                        for (int mx = 0; mx < effect; mx++)
                        {
                            int pos = current + (mx - 1) + ((my - 1) * width);
                            resPix[0, pos] = (byte)sum[0];
                            resPix[1, pos] = (byte)sum[1];
                            resPix[2, pos] = (byte)sum[2];
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

        private Bitmap AdjustTobMosaic(Bitmap bitmap, int effectWidth)
        {
            // 差異最多的就是以照一定範圍取樣 玩之後直接去下一個範圍
            for (int heightOfffset = 0; heightOfffset < bitmap.Height; heightOfffset += effectWidth)
            {
                for (int widthOffset = 0; widthOffset < bitmap.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 計算範圍平均
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;


                    // 所有範圍內都設定此值
                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
            }
            return bitmap;
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}
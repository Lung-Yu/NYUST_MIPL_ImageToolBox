using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class NoiseGaussian : IImageProcess
    {
        private Bitmap _ImageSource;
        private int _width;
        private int _height;
        private int _mean = 100;
        private int _sd = 50;
        private int MAX_RANDOM_NUM = 256;

        private Random random;
        private int _noiseCount;



        public NoiseGaussian(int mean, int sd)
        {
            _mean = mean;
            _sd = sd;
            random = new Random();
        }

        private int randemIndex = 0;
        private double getNoise()
        {
            int Size = 10000;
            double u = random.Next() % Size;
            double v = random.Next() % Size;

            double cos = Math.Cos(2 * Math.PI * v / Size);
            double sqrt = Math.Sqrt(-2 * Math.Log(random.Next() / Size));

            double res = sqrt * cos * _sd + _mean;

            return res;

            //if (++randemIndex % 2 == 0)
            //    return Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v) * _sd + _mean;
            //else
            //    return Math.Sqrt(-2 * Math.Log(u)) * Math.Sin(2 * Math.PI * v) * _sd + _mean;
        }


        public Bitmap Process()
        {
            byte[, ,] image = ImageExtract.getimageMartix(_ImageSource);
            int[,] noiseMap = getNoiseMap();
            return superimposedNoise(noiseMap);
        }

        private int[,] getNoiseMap()
        {
            int[,] noiseMap = new int[_width, _height];

            int noiseCount = 0;

            do
            {
                int x = random.Next(0, _width);
                int y = random.Next(0, _height);

                if (noiseMap[x, y] == 0)
                {
                    double noise = getNoise();
                    noiseMap[x, y] = (int)noise;
                    noiseCount++;
                }
            } while (noiseCount < _noiseCount);

            return noiseMap;
        }

        private Bitmap superimposedNoise(int[,] noise)
        {
            int width = _ImageSource.Width;
            int height = _ImageSource.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_ImageSource, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);


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
                        int r = srcP[0] + noise[x, y];
                        int b = srcP[1] + noise[x, y];
                        int g = srcP[2] + noise[x, y];

                        *(dstP) = (byte)((r > 255) ? 255 : (r < 0) ? 0 : r);
                        *(dstP + 1) = (byte)((b > 255) ? 255 : (b < 0) ? 0 : b);
                        *(dstP + 2) = (byte)((g > 255) ? 255 : (g < 0) ? 0 : g);
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            _ImageSource.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }


        private void setNoiseCount(Bitmap bitmap)
        {
            int imageSize = bitmap.Width * bitmap.Height;
            _noiseCount = (imageSize / 10) * 3;
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
            setNoiseCount(bitmap);
            _width = _ImageSource.Width;
            _height = _ImageSource.Height;
        }
    }
}

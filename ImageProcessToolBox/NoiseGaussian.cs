using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class NoiseGaussian : PointTemplate, IImageProcess
    {
        private Bitmap _ImageSource;
        private int _mean = 100;
        private int _sd = 50;
        private Random random;
        private int MAX_RANDOM_NUM = 256;

        public NoiseGaussian(int mean, int sd)
        {
            _mean = mean;
            _sd = sd;
            random = new Random();
        }

        private int randemIndex = 0;
        private double getNoise()
        {
            
            double u = random.NextDouble() / MAX_RANDOM_NUM;
            double v = random.NextDouble() / MAX_RANDOM_NUM;

            if (++randemIndex % 2 == 0)
                return Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v) * _sd + _mean;
            else
                return Math.Sqrt(-2 * Math.Log(u)) * Math.Sin(2 * Math.PI * v) * _sd + _mean;
        }


        public Bitmap Process()
        {
            byte[, ,] image = ImageExtract.getimageMartix(_ImageSource);
            Bitmap dstBitmap = new Bitmap(_ImageSource);


            ImageExtract.writeImageByMartix(image, dstBitmap);
            return base.process(_ImageSource);
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }

        private double val = 0;
        protected override byte processColorR(byte r, byte g, byte b)
        {
            val = ((double)r + getNoise());
            return (byte)((val > MAX_RANDOM_NUM) ? MAX_RANDOM_NUM : (val < 0) ? 0 : val);
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return (byte)((val > MAX_RANDOM_NUM) ? MAX_RANDOM_NUM : (val < 0) ? 0 : val);
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return (byte)((val > MAX_RANDOM_NUM) ? MAX_RANDOM_NUM : (val < 0) ? 0 : val);
        }
    }
}

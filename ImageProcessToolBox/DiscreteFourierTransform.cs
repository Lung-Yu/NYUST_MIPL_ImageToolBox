using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class DiscreteFourierTransform : IImageProcess
    {
        private Bitmap _SourceImage;
        private Complex[,] _pixels;
        private Complex[,] _output;
        private Complex[,] _shifted;
        private int _ImageWidth;
        private int _ImageHeight;

        private static int FFT_Forward = 1;
        private static int FFT_Inverst = -1;
        private static int _LOG_C = 1;


        public Bitmap Process()
        {
            initPixels();
            getPixelsFromImage();
            FFT2D();
            //FFTShift();
            return makeBitmapFromPixels();
        }

        public void FFTShift()
        {
            int yHelf = _ImageHeight / 2;
            int xHelf = _ImageWidth / 2;

            for (int i = 0; i < yHelf; i++)
            {
                for (int j = 0; j < xHelf; j++)
                {
                    _shifted[xHelf + j, yHelf + i] = _output[i, j]; //左上至右下
                    _shifted[i, j] = _output[xHelf + j, yHelf + i]; //右下至左上
                    _shifted[j, i + yHelf] = _output[xHelf + j, i]; //右上至左下
                    _shifted[xHelf + j, i] = _output[j, i + yHelf]; //左下至右上
                }
            }

            //for (int i = 0; i <= (_ImageWidth / 2) - 1; i++)
            //    for (int j = 0; j <= (_ImageHeight / 2) - 1; j++)
            //    {
            //        _shifted[i + (_ImageWidth / 2), j + (_ImageHeight / 2)] = _output[i, j];
            //        _shifted[i, j] = _output[i + (_ImageWidth / 2), j + (_ImageHeight / 2)];
            //        _shifted[i + (_ImageWidth / 2), j] = _output[i, j + (_ImageHeight / 2)];
            //        _shifted[i, j + (_ImageWidth / 2)] = _output[i + (_ImageWidth / 2), j];
            //    }
        }

        private void FFT2D()
        {
            for (int i = 0; i < _ImageHeight; i++)
            {
                Complex[] row = new Complex[_ImageWidth];
                for (int j = 0; j < _ImageWidth; j++)
                    row[j] = _pixels[j, i];

                Complex[] outRow = FFT(row, FFT_Forward);
                for (int j = 0; j < _ImageWidth; j++)
                {
                    _output[j, i] = outRow[j];
                    _shifted[j, i] = outRow[j];
                }
            }


            for (int i = 0; i < _ImageWidth; i++)
            {
                Complex[] row = new Complex[_ImageHeight];
                for (int j = 0; j < _ImageHeight; j++)
                    row[j] = _pixels[i, j];

                Complex[] outRow = FFT(row, FFT_Forward);
                for (int j = 0; j < _ImageHeight; j++)
                {
                    _output[i, j] = outRow[j];
                    _shifted[i, j] = outRow[j];
                }
            }
        }


        private static Complex[] FFT(Complex[] inputs, int dir)
        {
            int size = inputs.Length;

            Complex[] output = new Complex[size];

            for (int i = 0; i < size; i++)
            {
                output[i] = new Complex();
                for (int j = 0; j < size; j++)
                {
                    double angleTerm = (Math.PI * 2 * i * j * dir) / size;
                    double cosineA = Math.Cos(angleTerm);
                    double sineA = Math.Sin(angleTerm);

                    output[i].Real += inputs[j].Real * cosineA;
                    output[i].Image -= inputs[j].Real * sineA;

                    //output[i].Real += inputs[j].Real * cosineA - inputs[j].Image * sineA;
                    //output[i].Image += inputs[j].Real * sineA + inputs[j].Image * cosineA;

                }
                //Console.Write(String.Format("({0},{1})\n", output[i].Real, output[i].Image));
            }


            if (dir == 1)
            {
                for (int i = 0; i < size; i++)
                {
                    output[i].Real = output[i].Real / size;
                    output[i].Image = output[i].Image / size;
                }
            }
            return output;
        }

        private void initPixels()
        {
            _ImageHeight = _SourceImage.Height;
            _ImageWidth = _SourceImage.Width;
            _pixels = new Complex[_ImageWidth, _ImageHeight];
            _output = new Complex[_ImageWidth, _ImageHeight];
            _shifted = new Complex[_ImageWidth, _ImageHeight];
        }

        private Bitmap makeBitmapFromPixels()
        {
            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_SourceImage, _ImageWidth, _ImageHeight, out srcScan, out dstScan, out srcBmData, out dstBmData);


            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - _ImageWidth * 3;
                int dstOffset = dstBmData.Stride - _ImageWidth * 3;

                for (int y = 0; y < _ImageHeight; y++)
                {
                    for (int x = 0; x < _ImageWidth; x++, srcP += 3, dstP += 3)
                    {
                        //*(dstP) = *(dstP + 1) = *(dstP + 2) = (byte)(_LOG_C * Math.Log(_shifted[x, y].Magnitude()));
                        *(dstP) = *(dstP + 1) = *(dstP + 2) = (byte)_shifted[x, y].Magnitude();
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            _SourceImage.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        private void getPixelsFromImage()
        {
            System.IntPtr srcScan;
            BitmapData srcBmData;
            ImageExtract.InitPonitMethod(_SourceImage, _ImageWidth, _ImageHeight, out srcScan, out srcBmData);

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - _ImageWidth * 3;

                for (int y = 0; y < _ImageHeight; y++)
                {
                    for (int x = 0; x < _ImageWidth; x++, srcP += 3)
                    {
                        byte gray = (byte)(.299 * srcP[2] + .587 * srcP[1] + .114 * srcP[0]);
                        double val = gray;
                        _pixels[x, y] = new Complex(val);
                    }
                    srcP += srcOffset;
                }
            }

            _SourceImage.UnlockBits(srcBmData);
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }

    class Complex
    {
        double _real;
        double _image;
        public Complex()
        {
            _real = 0.0;
            _image = 0.0;
        }
        public Complex(double real)
        {
            _real = real;
            _image = 0.0;
        }
        public Complex(double real, double image)
        {
            _real = real;
            _image = image;
        }

        public double Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public double Real
        {
            get { return _real; }
            set { _real = value; }
        }

        public double Magnitude()
        {
            return ((float)Math.Sqrt(_real * _real + _image * _image));
        }

        public double Phase()
        {
            return ((float)Math.Atan(_image / _real));
        }
        public double spectralDensity()
        {
            return _real * _real + _image * _image;
        }

        public void Add(Complex complex)
        {
            _image += complex.Image;
            _real += complex.Real;
        }

    }
}

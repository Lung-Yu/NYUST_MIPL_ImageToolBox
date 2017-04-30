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
        private int _ImageWidth;
        private int _ImageHeight;

        private static int FFT_Forward = 1;
        private static int FFT_Inverst = -1;
        private static int _LOG_C = 1;

        private double _MaxVal = 0;

        public Bitmap Process()
        {
            initPixels();
            getPixelsFromImage();
            //showPixels();
            FFT2D();
            Bitmap result = makeBitmapFromPixels();

            return result;
        }

        private void showPixels()
        {
            for (int i = 0; i < _ImageHeight; i++)
            {
                for (int j = 0; j < _ImageWidth; j++)
                {
                    Console.Write(String.Format("{0}\t", _pixels[j, i].Real));
                }
                Console.WriteLine();
            }
        }
        private void RowFFT(ref Complex[,] inputs)
        {
            for (int i = 0; i < _ImageHeight; i++)
            {
                Complex[] row = new Complex[_ImageWidth];
                for (int j = 0; j < _ImageWidth; j++)
                {
                    row[j] = inputs[j, i];    
                }

                Complex[] outRow = FFT(row, FFT_Forward);
                for (int j = 0; j < _ImageWidth; j++)
                {
                    _output[j, i] = outRow[j];
                    updateMaxVal(outRow[j].Real);
                }
            }
        }
        private void ColFFT(ref Complex[,] inputs)
        {
            for (int i = 0; i < _ImageWidth; i++)
            {
                Complex[] row = new Complex[_ImageHeight];
                for (int j = 0; j < _ImageHeight; j++)
                {
                    row[j] = inputs[i, j];
                    //Console.Write(row[j].Real + ",");
                }
                //Console.WriteLine();

                Complex[] outRow = FFT(row, FFT_Forward);
                for (int j = 0; j < _ImageHeight; j++)
                {
                    _output[i, j] = outRow[j];
                    updateMaxVal(outRow[j].Real);
                }
            }
        }
        private void copyArray()
        {
            for (int i = 0; i < _ImageWidth; i++)
            {
                for (int j = 0; j < _ImageHeight; j++)
                {
                    _pixels[i, j] = _output[i, j];
                }
            }

        }
        private void FFT2D()
        {
            RowFFT(ref _pixels);
            ColFFT(ref _output);   
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

                    //output[i].Real += inputs[j].Real * cosineA;
                    //output[i].Image -= inputs[j].Real * sineA;

                    output[i].Real += inputs[j].Real * cosineA - inputs[j].Image * sineA;
                    output[i].Image += inputs[j].Real * sineA + inputs[j].Image * cosineA;

                }
                //Console.Write(String.Format(">> ({0},{1})\n", output[i].Real, output[i].Image));
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
        }
        private Bitmap makeBitmapFromPixels()
        {
            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(_SourceImage, _ImageWidth, _ImageHeight, out srcScan, out dstScan, out srcBmData, out dstBmData);

            double maxVal = 0;
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
                        //*(dstP) = *(dstP + 1) = *(dstP + 2) = (byte)(_LOG_C * Math.Log(Math.Abs(_shifted[x, y].spectralDensity())));
                        double val = Math.Abs(_output[x, y].Magnitude());
                        *(dstP) = *(dstP + 1) = *(dstP + 2) = (byte)val;

                        if (maxVal < val)
                            maxVal = val;

                        //Console.WriteLine(val);
                        //*(dstP) = *(dstP + 1) = *(dstP + 2) = (byte)val;
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }
            Console.WriteLine(maxVal);
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
                        byte gray = calGray(srcP[2], srcP[1], srcP[0]);
                        double val = gray;
                        //val *= ((x + y) % 2 == 0) ? -1 : 1;
                        _pixels[x, y] = new Complex(val);
                        updateMaxVal(val);
                    }
                    srcP += srcOffset;
                }
            }

            _SourceImage.UnlockBits(srcBmData);
        }

        private byte calGray(byte arv1, byte arv2, byte arv3)
        {
            double val = (0.299 * arv1 + 0.587 * arv2 + 0.114 * arv3);
            if (val > 255)
                return 255;
            return (byte)val;
        }

        private void updateMaxVal(double val)
        {
            if (_MaxVal < val)
                _MaxVal = val;
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

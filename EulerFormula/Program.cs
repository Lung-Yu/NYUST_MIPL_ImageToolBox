using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerFormula
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] x_1 = { 0, 0.3, 0.6, 0.8, 1, 1, 0.9, 0.7, 0.5, 0.2, 0.2, 0.5, 0.7, 0.9, 1, 1, 0.8, 0.6, 0.3, 0 };

            Complex[] inputs = new Complex[x_1.Length];
            for (int i = 0; i < x_1.Length; i++)
                inputs[i] = new Complex(x_1[i]);
            FFT(inputs);

            Console.ReadKey();
        }


        private static Complex[] FFT(Complex[] inputs)
        {
            int size = inputs.Length;

            Complex[] output = new Complex[size];

            for (int i = 0; i < size; i++)
            {
                output[i] = new Complex();
                for (int j = 0; j < size; j++)
                {
                    double angleTerm = Math.PI * 2 * i * j;
                    double cosineA = Math.Cos(angleTerm / size);
                    double sineA = Math.Sin(angleTerm / size);

                    //output[i].Real += inputs[j].Real * cosineA;
                    //output[i].Image -= inputs[j].Real * sineA;

                    output[i].Real += inputs[j].Real * cosineA - inputs[j].Image * sineA;
                    output[i].Image += inputs[j].Real * sineA + inputs[j].Image * cosineA;

                }
                Console.Write(String.Format("({0},{1})\n", output[i].Real , output[i].Image));
            }
            return output;
        }

        //double pi2 = 2.0 * M_PI;
        //double angleTerm,cosineA,sineA;
        //double invs = 1.0 / size;
        //for(unsigned int y = 0;y < size;y++) {
        //output_seq[y] = 0;
        //for(unsigned int x = 0;x < size;x++) {
        //    angleTerm = pi2 * y * x * invs;
        //    cosineA = cos(angleTerm);
        //    sineA = sin(angleTerm);
        //    output_seq[y].real += input_seq[x].real * cosineA - input_seq[x].imag * sineA;
        //    output_seq[y].imag += input_seq[x].real * sineA + input_seq[x].imag * cosineA;
        //}
        //output_seq[y] *= invs;
        //}

        static void factorial()
        {
            double e = 1;
            int i = 1;
            while (1 / GetFactorial(i) > Math.Pow(10, -6))
            {
                e = e + 1 / GetFactorial(i);
                i++;
            }
            Console.WriteLine(e);
            Console.ReadKey();
        }



        static double GetFactorial(int a)
        {
            double result = 1;
            for (int i = 1; i < a + 1; i++)
                result = result * i;
            return result;
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

            public static void Euler(Complex complex)
            {
                double sin1 = Math.Sin(-1 * complex.Phase());
                double cos1 = Math.Cos(-1 * complex.Phase());

                double sin2 = Math.Sin(2 * complex.Phase());
                double cos2 = Math.Cos(2 * complex.Phase());

                Complex a1 = new Complex(cos1, sin1);
                Complex a2 = new Complex(cos2, sin2);




                //complex<T>(2 * cos1 * a1.real() - a2.real(), 2 * cos1 * a1.imag() - a2.imag());
            }
            private void Euler(int angle)
            {
                _real = Math.Cos(angle);
                _image = Math.Sin(angle);
            }
        }
    }
}

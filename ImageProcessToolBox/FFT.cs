using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Fast_Fourier_Transform
{
    /// <summary>
    /// Defining Structure for Complex Data type  N=R+Ii
    /// </summary>
    struct COMPLEX
    {
        public double real, imag;
        public COMPLEX(double x, double y)
        {
            real = x;
            imag = y;
        }
        public float Magnitude()
        {
            return ((float)Math.Sqrt(real * real + imag * imag));
        }
        public float Phase()
        {            
             return ((float)Math.Atan(imag / real));
        }
    }
    
    class FFT
    {
        public Bitmap Obj;               // Input Object Image
        public Bitmap FourierPlot;       // Generated Fouruer Magnitude Plot
        public Bitmap PhasePlot;         // Generated Fourier Phase Plot

        public int[,] GreyImage;         //GreyScale Image Array Generated from input Image
        public float[,] FourierMagnitude;
        public float[,] FourierPhase;

        float[,] FFTLog;                 // Log of Fourier Magnitude
        float[,] FFTPhaseLog;            // Log of Fourier Phase
        public int[,] FFTNormalized;     // Normalized FFT Magnitude : Scale 0-1
        public int[,] FFTPhaseNormalized;// Normalized FFT Phase : Scale 0-1
        int nx, ny;                      //Number of Points in Width & height
        int Width, Height;
        COMPLEX[,] Fourier;              //Fourier Magnitude  Array Used for Inverse FFT
        public COMPLEX[,] FFTShifted;    // Shifted FFT 
        public COMPLEX[,] Output;        // FFT Normal
        public COMPLEX[,] FFTNormal;     // FFT Shift Removed - required for Inverse FFT 
        
        /// <summary>
        /// Parameterized Constructor for FFT Reads Input Bitmap to a Greyscale Array
        /// </summary>
        /// <param name="Input">Input Image</param>
        public FFT(Bitmap Input)
        {
            Obj = Input;
            Width = nx = Input.Width;
            Height= ny = Input.Height;
            ReadImage();
        }
        /// <summary>
        /// Parameterized Constructor for FFT
        /// </summary>
        /// <param name="Input">Greyscale Array</param>
        public FFT(int[,] Input)
        {
            GreyImage = Input;
            Width = nx = Input.GetLength(0);
            Height = ny = Input.GetLength(1);
        }
        /// <summary>
        /// Constructor for Inverse FFT
        /// </summary>
        /// <param name="Input"></param>
        public FFT(COMPLEX[,] Input)
        {
            nx = Width = Input.GetLength(0);
            ny = Height = Input.GetLength(1);
            Fourier = Input;

        }
        /// <summary>
        /// Function to Read Bitmap to greyscale Array
        /// </summary>
        private void ReadImage()
        {
            int i, j;
            GreyImage = new int[Width, Height];  //[Row,Column]
            Bitmap image = Obj;
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        GreyImage[j, i] = (int)((imagePointer1[0] + imagePointer1[1] + imagePointer1[2]) / 3.0);
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }//end for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return;
        }
        public Bitmap Displayimage()
        {
            int i, j;
            Bitmap image = new Bitmap(Width, Height);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, Width, Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {

                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        imagePointer1[0] = (byte)GreyImage[j, i];
                        imagePointer1[1] = (byte)GreyImage[j, i];
                        imagePointer1[2] = (byte)GreyImage[j, i];
                        imagePointer1[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j

                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return image;// col;
        }
        public Bitmap Displayimage(int[,] image)
        {
            int i, j;
            Bitmap output = new Bitmap(image.GetLength(0), image.GetLength(1));
            BitmapData bitmapData1 = output.LockBits(new Rectangle(0, 0, image.GetLength(0), image.GetLength(1)),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        imagePointer1[0] = (byte)image[j, i];
                        imagePointer1[1] = (byte)image[j, i];
                        imagePointer1[2] = (byte)image[j, i];
                        imagePointer1[3] = 255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            output.UnlockBits(bitmapData1);
            return output;// col;

        }
        /// <summary>
        /// Calculate Fast Fourier Transform of Input Image
        /// </summary>
        public void ForwardFFT()
        {
            //Initializing Fourier Transform Array
            int i,j;
            Fourier =new COMPLEX [Width,Height];
            Output = new COMPLEX[Width, Height];
            //Copy Image Data to the Complex Array
            for (i=0;i<=Width -1;i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    Fourier[i, j].real =(double) GreyImage[i, j];
                    Fourier[i, j].imag = 0;
                }
            //Calling Forward Fourier Transform
            Output= FFT2D( Fourier, nx, ny, 1);
            return;
        }
        /// <summary>
        /// Shift The FFT of the Image
        /// </summary>
        public void FFTShift()
        {
            int i, j;
            FFTShifted = new COMPLEX[nx, ny];

            for(i=0;i<=(nx/2)-1;i++)
                for (j = 0; j <= (ny / 2) - 1; j++)
                {
                    FFTShifted[i + (nx / 2), j + (ny / 2)] = Output[i, j];
                    FFTShifted[i, j] = Output[i + (nx / 2), j + (ny / 2)];
                    FFTShifted[i + (nx / 2), j] = Output[i , j + (ny / 2)];
                    FFTShifted[i, j + (nx / 2)] = Output[i + (nx / 2), j ];
                }

            return;
        }
        /// <summary>
        /// Removes FFT Shift for FFTshift Array
        /// </summary>
        public void RemoveFFTShift()
        {
            int i, j;
            FFTNormal = new COMPLEX[nx, ny];

            for (i = 0; i <= (nx / 2) - 1; i++)
                for (j = 0; j <= (ny / 2) - 1; j++)
                {
                    FFTNormal[i + (nx / 2), j + (ny / 2)] = FFTShifted[i, j];
                    FFTNormal[i, j] = FFTShifted[i + (nx / 2), j + (ny / 2)];
                    FFTNormal[i + (nx / 2), j] = FFTShifted[i, j + (ny / 2)];
                    FFTNormal[i, j + (nx / 2)] = FFTShifted[i + (nx / 2), j];
                }
            return;
        }
        /// <summary>
        /// FFT Plot Method for Shifted FFT
        /// </summary>
        /// <param name="Output"></param>
        public void FFTPlot(COMPLEX[,]Output)
        {
            int i, j;
            float max;
       
            FFTLog = new float[nx, ny];
            FFTPhaseLog = new float[nx, ny];

            FourierMagnitude = new float[nx, ny];
            FourierPhase = new float[nx, ny];

            FFTNormalized = new int[nx, ny];
            FFTPhaseNormalized = new int[nx, ny];

            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FourierMagnitude[i, j] = Output[i, j].Magnitude();
                    FourierPhase[i, j] = Output[i, j].Phase();
                    FFTLog[i, j] = (float)Math.Log(1 + FourierMagnitude[i, j]);
                    FFTPhaseLog[i, j] = (float)Math.Log(1 + Math.Abs(FourierPhase[i, j]));
                }
            //Generating Magnitude Bitmap
            max = FFTLog[0, 0];
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    if (FFTLog[i, j] > max)
                        max = FFTLog[i, j];
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTLog[i, j] = FFTLog[i, j] / max;
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTNormalized[i, j] = (int)(2000 * FFTLog[i, j]);
                }
            //Transferring Image to Fourier Plot
            FourierPlot = Displayimage(FFTNormalized);

            //generating phase Bitmap
            FFTPhaseLog[0, 0] = 0;
            max = FFTPhaseLog[1, 1];
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    if (FFTPhaseLog[i, j] > max)
                        max = FFTPhaseLog[i, j];
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTPhaseLog[i, j] = FFTPhaseLog[i, j] / max;
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTPhaseNormalized[i, j] = (int)(255 * FFTPhaseLog[i, j]);
                }
            //Transferring Image to Fourier Plot
            PhasePlot = Displayimage(FFTPhaseNormalized);


        }
        /// <summary>
        /// generate FFT Image for Display Purpose
        /// </summary>
        public void FFTPlot()
        {
            int i, j;
            float max;
            FFTLog = new float [nx,ny];
            FFTPhaseLog = new float[nx, ny];

            FourierMagnitude = new float[nx, ny];
            FourierPhase = new float[nx, ny];

            FFTNormalized = new int[nx, ny];
            FFTPhaseNormalized = new int[nx, ny];

            for(i=0;i<=Width-1;i++)
                for (j = 0; j <= Height-1; j++)
                {
                    FourierMagnitude[i, j] = Output[i, j].Magnitude();
                    FourierPhase[i, j] = Output[i, j].Phase();
                    FFTLog[i, j] = (float)Math.Log(1 + FourierMagnitude[i, j]);
                    FFTPhaseLog[i, j] = (float)Math.Log(1 + Math.Abs(FourierPhase[i, j]));
                }
            //Generating Magnitude Bitmap
            max = FFTLog[0, 0];
            for(i=0;i<=Width-1;i++)
                for (j = 0; j <= Height-1; j++)
                {
                    if (FFTLog[i, j] > max)
                        max = FFTLog[i, j];
                }
            for(i=0;i<=Width-1;i++)
                for (j = 0; j <= Height-1; j++)
                {
                    FFTLog[i, j] = FFTLog[i, j] / max;
                }
            for(i=0;i<=Width-1;i++)
                for (j = 0; j <= Height-1; j++)
                {
                    FFTNormalized [i,j]=(int)(1000*FFTLog[i,j]);
                }
            //Transferring Image to Fourier Plot
            FourierPlot = Displayimage(FFTNormalized);
            
            //generating phase Bitmap

            max = FFTPhaseLog[0, 0];
            for (i = 0; i <= Width-1; i++)
                for (j = 0; j <= Height-1; j++)
                {
                    if (FFTPhaseLog[i, j] > max)
                        max = FFTPhaseLog[i, j];
                }
            for (i = 0; i <= Width-1; i++)
                for (j = 0; j <= Height-1; j++)
                {
                    FFTPhaseLog[i, j] = FFTPhaseLog[i, j] / max;
                }
            for (i = 0; i <= Width-1; i++)
                for (j = 0; j <= Height-1; j++)
                {
                    FFTPhaseNormalized[i, j] = (int)(2000 * FFTLog[i, j]);
                }
            //Transferring Image to Fourier Plot
            PhasePlot = Displayimage(FFTPhaseNormalized);


        }
        /// <summary>
        /// Calculate Inverse from Complex [,]  Fourier Array
        /// </summary>
        public void InverseFFT()
        {
            //Initializing Fourier Transform Array
            int i, j;
           
            //Calling Forward Fourier Transform
            Output =new COMPLEX [nx,ny];
            Output = FFT2D(Fourier, nx, ny, -1);

            Obj = null;  // Setting Object Image to Null
            //Copying Real Image Back to Greyscale
            //Copy Image Data to the Complex Array
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    GreyImage[i, j] = (int)Output[i, j].Magnitude(); 
                    
                }
            Obj = Displayimage(GreyImage);
            return;

        }
        /// <summary>
        /// Generates Inverse FFT of Given Input Fourier
        /// </summary>
        /// <param name="Fourier"></param>
        public void InverseFFT(COMPLEX [,] Fourier)
        {
            //Initializing Fourier Transform Array
            int i, j;

            //Calling Forward Fourier Transform
            Output = new COMPLEX[nx, ny];
            Output = FFT2D(Fourier, nx, ny, -1);


            //Copying Real Image Back to Greyscale
            //Copy Image Data to the Complex Array
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    GreyImage[i, j] = (int)Output[i, j].Magnitude();

                }
            Obj = Displayimage(GreyImage);
            return;

        }
        /*-------------------------------------------------------------------------
            Perform a 2D FFT inplace given a complex 2D array
            The direction dir, 1 for forward, -1 for reverse
            The size of the array (nx,ny)
            Return false if there are memory problems or
            the dimensions are not powers of 2
        */
        public COMPLEX [,] FFT2D(COMPLEX[,] c, int nx, int ny, int dir)
          {
            int i,j;
            int m;//Power of 2 for current number of points
            double []real;
            double []imag;
            COMPLEX [,] output;//=new COMPLEX [nx,ny];
            output = c; // Copying Array
            // Transform the Rows 
            real = new double[nx] ;
            imag = new double[nx];
            
            for (j=0;j<ny;j++) 
            {
              for (i=0;i<nx;i++) 
               {
                 real[i] = c[i,j].real;
                 imag[i] = c[i,j].imag;
               }
            // Calling 1D FFT Function for Rows
            m = (int)Math.Log((double)nx, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
            FFT1D(dir,m,ref real,ref imag);

              for (i=0;i<nx;i++) 
               {
               //  c[i,j].real = real[i];
               //  c[i,j].imag = imag[i];
                   output[i, j].real = real[i];
                   output[i, j].imag = imag[i];
               }
            }
            // Transform the columns  
            real = new double[ny];
            imag = new double[ny];
                  
            for (i=0;i<nx;i++) 
            {
              for (j=0;j<ny;j++) 
               {
                //real[j] = c[i,j].real;
                //imag[j] = c[i,j].imag;
                   real[j] = output[i, j].real;
                   imag[j] = output[i, j].imag;
               }
           // Calling 1D FFT Function for Columns
           m = (int)Math.Log((double)ny, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
           FFT1D(dir,m,ref real,ref imag);
             for (j=0;j<ny;j++) 
               {
                //c[i,j].real = real[j];
                //c[i,j].imag = imag[j];
                output[i, j].real = real[j];
                output[i, j].imag = imag[j];
               }
            }
          
           // return(true);
            return(output);
        }
        /*-------------------------------------------------------------------------
            This computes an in-place complex-to-complex FFT
            x and y are the real and imaginary arrays of 2^m points.
            dir = 1 gives forward transform
            dir = -1 gives reverse transform
            Formula: forward
                     N-1
                      ---
                    1 \         - j k 2 pi n / N
            X(K) = --- > x(n) e                  = Forward transform
                    N /                            n=0..N-1
                      ---
                     n=0
            Formula: reverse
                     N-1
                     ---
                     \          j k 2 pi n / N
            X(n) =    > x(k) e                  = Inverse transform
                     /                             k=0..N-1
                     ---
                     k=0
            */
        private void FFT1D(int dir, int m, ref double[] x, ref double[] y )
            {
                long nn, i, i1, j, k, i2, l, l1, l2;
                double c1, c2, tx, ty, t1, t2, u1, u2, z;
                /* Calculate the number of points */
                nn = 1;
                for (i = 0; i < m; i++)
                    nn *= 2;
                /* Do the bit reversal */
                i2 = nn >> 1;
                j = 0;
                for (i = 0; i < nn - 1; i++)
                {
                    if (i < j)
                    {
                        tx = x[i];
                        ty = y[i];
                        x[i] = x[j];
                        y[i] = y[j];
                        x[j] = tx;
                        y[j] = ty;
                    }
                    k = i2;
                    while (k <= j)
                    {
                        j -= k;
                        k >>= 1;
                    }
                    j += k;
                }
                /* Compute the FFT */
                c1 = -1.0;
                c2 = 0.0;
                l2 = 1;
                for (l = 0; l < m; l++)
                {
                    l1 = l2;
                    l2 <<= 1;
                    u1 = 1.0;
                    u2 = 0.0;
                    for (j = 0; j < l1; j++)
                    {
                        for (i = j; i < nn; i += l2)
                        {
                            i1 = i + l1;
                            t1 = u1 * x[i1] - u2 * y[i1];
                            t2 = u1 * y[i1] + u2 * x[i1];
                            x[i1] = x[i] - t1;
                            y[i1] = y[i] - t2;
                            x[i] += t1;
                            y[i] += t2;
                        }
                        z = u1 * c1 - u2 * c2;
                        u2 = u1 * c2 + u2 * c1;
                        u1 = z;
                    }
                    c2 = Math.Sqrt((1.0 - c1) / 2.0);
                    if (dir == 1)
                        c2 = -c2;
                    c1 = Math.Sqrt((1.0 + c1) / 2.0);
                }
                /* Scaling for forward transform */
                if (dir == 1)
                {
                    for (i = 0; i < nn; i++)
                    {
                        x[i] /= (double)nn;
                        y[i] /= (double)nn;
                       
                    }
                }
                


              //  return(true) ;
                return;
            }
        
    }
}

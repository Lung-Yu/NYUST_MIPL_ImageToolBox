using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Laplacian : FilterTemplate , IImageProcess 
    {
        private Bitmap _SourceImage;
        public Laplacian(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
        public Laplacian()
        {
           
        }

        public Bitmap Process()
        {
            return laplacianFilter(_SourceImage);
        }

        private Bitmap laplacianFilter(Bitmap bitmap)
        {
            return base.convolute(bitmap, 3, 3);
        }
        protected override byte maskFilter(byte[] gate)
        {
            int[] mask ={
                         -1,-1,-1,
                         -1,8,-1,
                         -1,-1,-1,
                    };
            double result = 0;

            for (int i = 0; i < gate.Length; i++)
                result += (double)((gate[i] * mask[i]));
            return (byte)((result > 255) ? 255 : (result < 0) ? 0 : result);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

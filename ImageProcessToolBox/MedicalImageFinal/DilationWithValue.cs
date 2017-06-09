using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class DilationWithValue :FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private int _efficWidth;
        private int _efficHeight;
        private int _targetVal = 0;
        public DilationWithValue(byte val, int efficWidth, int efficHeight)
        {
            _targetVal = val;
            _efficWidth = efficWidth;
            _efficHeight = efficHeight;
        }

        public Bitmap Process()
        {
            return base.convolute(_SourceImage, _efficWidth, _efficHeight);
        }

        
        protected override byte maskFilter(byte[] gate)
        {
            bool Is = false;  //假設其符合條件

            foreach (byte item in gate)
                if (item == _targetVal)
                {
                    Is = true;
                    break;
                }
                    
            //return (byte)((Is) ? 0 : gate[gate.Length / 2]);
            return (byte)((Is) ? 255 : 0);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.MedicalImageFinal
{
    class ErosionWithValue:FilterTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private int _efficWidth;
        private int _efficHeight;
        private int _targetVal = 0;

        public ErosionWithValue(byte val, int efficWidth, int efficHeight)
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
            bool Is = true;  //假設其符合條件

            foreach (byte item in gate)
                if (item != _targetVal)
                {
                    Is = false;
                    break;
                }
            return (byte)((Is) ? _targetVal : 0);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

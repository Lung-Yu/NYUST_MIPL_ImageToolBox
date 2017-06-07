using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Analysis
{
    class Bandpass :PointTemplate, IImageProcess
    {
        private Bitmap _srcImage;
        private byte _passVal;
        private byte _fillVal;
        public Bandpass(byte passVal, byte fillVal)
        {
            _passVal = passVal;
            _fillVal = fillVal;
        }

        public Bitmap Process()
        {
            return process(_srcImage);
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _srcImage = bitmap;
        }

        protected override byte processColorR(byte r, byte g, byte b)
        {
            return (r == _passVal) ? r : _fillVal;
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return (g == _passVal) ? g : _fillVal;
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return (b == _passVal) ? b : _fillVal;
        }
    }
}

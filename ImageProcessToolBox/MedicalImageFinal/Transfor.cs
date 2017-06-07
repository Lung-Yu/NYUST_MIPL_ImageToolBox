using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Analysis
{
    class Transfor: PointTemplate, IImageProcess
    {
        public Transfor(int t)
        {
            _t = t;
        }
        private int _t;
        private byte tempVal = 0;
        private Bitmap _src;
        protected override byte processColorR(byte r, byte g, byte b)
        {
            if (r < _t)
                tempVal = 0;
            else
                tempVal = r;

            return tempVal;
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return tempVal;
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return tempVal;
        }

        public Bitmap Process()
        {
            return base.process(_src);
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _src = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class ColorRGBModel
    {
        private byte _R;


        public byte R
        {
            get { return _R; }
            set { _R = value; }
        }
        private byte _G;

        public byte G
        {
            get { return _G; }
            set { _G = value; }
        }
        private byte _B;

        public byte B
        {
            get { return _B; }
            set { _B = value; }
        }
    }
}

using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.PoingProcessing
{
    class TransformPowerLaw : ImageBasic
    {
        private byte[] _values = new byte[256];
        private double _pow = 1.5;

        private void initValues()
        {
            for (int i = 0; i < _values.Length; i++)
            {
                double val = Math.Pow(i + 1, _pow);
                _values[i] = (byte)((val > 255) ? 255 : val);
            }
        }
        public byte[] Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public double Pow
        {
            get { return _pow; }
            set { _pow = value; }
        }
        public override void process()
        {
            initValues();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int r = _imgMap[x, y, IMAGE_RED_INDEX];
                    int g = _imgMap[x, y, IMAGE_Green_INDEX];
                    int b = _imgMap[x, y, IMAGE_Blue_INDEX];
                    _resultMap[x, y, IMAGE_RED_INDEX] = _values[r];
                    _resultMap[x, y, IMAGE_Green_INDEX] = _values[g];
                    _resultMap[x, y, IMAGE_Blue_INDEX] = _values[b];
                }
            }
        }
    }
}

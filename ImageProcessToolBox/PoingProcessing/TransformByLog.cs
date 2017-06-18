using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.PoingProcessing
{
    class TransformByLog : ImageBasic
    {
        private int _C = 100;
        private double _baseNum = 10;
        private byte[] _logValues = new byte[256];

        private void initLogValue()
        {
            for (int i = 0; i < _logValues.Length; i++)
                _logValues[i] = (byte)(_C * Math.Log(i + 1, _baseNum));
        }
        public int C
        {
            get { return _C; }
            set { _C = value; }
        }
        public double BaseNum
        {
            get { return _baseNum; }
            set { _baseNum = value; }
        }
        public override void process()
        {
            initLogValue();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int r = _imgMap[x, y, IMAGE_RED_INDEX];
                    int g = _imgMap[x, y, IMAGE_Green_INDEX];
                    int b = _imgMap[x, y, IMAGE_Blue_INDEX];
                    _resultMap[x, y, IMAGE_RED_INDEX] = _logValues[r];
                    _resultMap[x, y, IMAGE_Green_INDEX] = _logValues[g];
                    _resultMap[x, y, IMAGE_Blue_INDEX] = _logValues[b];
                }
            }
        }
    }
}

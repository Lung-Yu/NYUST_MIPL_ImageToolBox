using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.PoingProcessing
{
    class BitOf8PlaneSlicing : ImageBasic
    {
        private static readonly byte[] planes = { 1, 2, 4, 8, 16, 32, 64, 128 };
        private int _bitNumber = 1;

        public int BitNumber
        {
            get { return _bitNumber; }
            set {
                if (value > 8 || value < 1)
                    throw new IndexOutOfRangeException("bit number only range in 1~8");
                _bitNumber = value;
            }
        }

        public override void process()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int plane = planes[_bitNumber - 1];
                    _resultMap[x, y, IMAGE_RED_INDEX] = (byte)(((_imgMap[x, y, IMAGE_RED_INDEX] & plane) == plane) ? 255 : 0);
                    _resultMap[x, y, IMAGE_Green_INDEX] = (byte)(((_imgMap[x, y, IMAGE_Green_INDEX] & plane) == plane) ? 255 : 0);
                    _resultMap[x, y, IMAGE_Blue_INDEX] = (byte)(((_imgMap[x, y, IMAGE_Blue_INDEX] & plane) == plane) ? 255 : 0);
                }
            }
        }
    }
}

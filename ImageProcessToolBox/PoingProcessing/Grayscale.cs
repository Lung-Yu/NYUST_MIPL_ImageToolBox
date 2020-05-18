using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.PoingProcessing
{
    class Grayscale : ImageBasic
    {
        public override void process()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    //red *0.299 + green*0.587 + blue*0.114
                    byte gray = (byte)(_imgMap[x, y, IMAGE_RED_INDEX] * 0.299 + _imgMap[x, y, IMAGE_Green_INDEX] * 0.587 + _imgMap[x, y, IMAGE_Blue_INDEX] * 0.114);

                    _resultMap[x, y, IMAGE_RED_INDEX] = gray;
                    _resultMap[x, y, IMAGE_Green_INDEX] = gray;
                    _resultMap[x, y, IMAGE_Blue_INDEX] = gray;
                }
            }
        }
    }
}

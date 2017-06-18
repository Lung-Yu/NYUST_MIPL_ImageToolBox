using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Filter
{
    class FilterMax : ImageBasicFilter
    {
        public override void process()
        {
            initMaskVal();

            for (int imgY = 0; imgY < _height; imgY++)
                for (int imgX = 0; imgX < _width; imgX++)
                {

                    //mask processing
                    byte[] temporary = new byte[] { 0, 0, 0 };
                    for (int y = _mask_y_start; y < _mask_y_end; y++)
                        for (int x = _mask_x_start; x < _mask_x_end; x++)
                        {
                            int new_x = x + imgX;
                            int new_y = y + imgY;

                            if (IsOutOfIndex(new_x, new_y))
                            {
                                temporary[IMAGE_RED_INDEX] = 0;
                                temporary[IMAGE_Green_INDEX] = 0;
                                temporary[IMAGE_Blue_INDEX] = 0;
                                break;
                            }
                            else
                            {
                                if (temporary[IMAGE_RED_INDEX] < _imgMap[new_x, new_y, IMAGE_RED_INDEX])
                                    temporary[IMAGE_RED_INDEX] = _imgMap[new_x, new_y, IMAGE_RED_INDEX];
                                if (temporary[IMAGE_Green_INDEX] < _imgMap[new_x, new_y, IMAGE_Green_INDEX])
                                    temporary[IMAGE_Green_INDEX] = _imgMap[new_x, new_y, IMAGE_Green_INDEX];
                                if (temporary[IMAGE_Blue_INDEX] < _imgMap[new_x, new_y, IMAGE_Blue_INDEX])
                                    temporary[IMAGE_Blue_INDEX] = _imgMap[new_x, new_y, IMAGE_Blue_INDEX];
                            }
                        }

                    _resultMap[imgX, imgY, IMAGE_RED_INDEX] = temporary[IMAGE_RED_INDEX];
                    _resultMap[imgX, imgY, IMAGE_Green_INDEX] = temporary[IMAGE_Green_INDEX];
                    _resultMap[imgX, imgY, IMAGE_Blue_INDEX] = temporary[IMAGE_Blue_INDEX];
                }
        }
    }
}

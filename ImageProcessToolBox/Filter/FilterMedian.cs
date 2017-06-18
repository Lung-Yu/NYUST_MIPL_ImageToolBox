using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Filter
{
    class FilterMedian : ImageBasicFilter
    {
        public override void process()
        {
            initMaskVal();
            int medainIndex = (_efficWidth * _efficHeigh) / 2;

            for (int imgY = 0; imgY < _height; imgY++)
                for (int imgX = 0; imgX < _width; imgX++)
                {

                    //mask processing
                    byte default_val = 0;
                    List<byte>[] temporary = new List<byte>[] { new List<byte>(), new List<byte>(), new List<byte>() };

                    for (int y = _mask_y_start; y <= _mask_y_end; y++)
                        for (int x = _mask_x_start; x <= _mask_x_end; x++)
                        {
                            int new_x = x + imgX;
                            int new_y = y + imgY;

                            if (IsOutOfIndex(new_x, new_y))
                            {
                                temporary[IMAGE_RED_INDEX].Add(default_val);
                                temporary[IMAGE_Green_INDEX].Add(default_val);
                                temporary[IMAGE_Blue_INDEX].Add(default_val);
                            }
                            else
                            {
                                temporary[IMAGE_RED_INDEX].Add(_imgMap[new_x, new_y, IMAGE_RED_INDEX]);
                                temporary[IMAGE_Green_INDEX].Add(_imgMap[new_x, new_y, IMAGE_Green_INDEX]);
                                temporary[IMAGE_Blue_INDEX].Add(_imgMap[new_x, new_y, IMAGE_Blue_INDEX]);
                            }
                        }

                    Array.Sort(temporary[IMAGE_RED_INDEX].ToArray());
                    Array.Sort(temporary[IMAGE_Green_INDEX].ToArray());
                    Array.Sort(temporary[IMAGE_Blue_INDEX].ToArray());

                    _resultMap[imgX, imgY, IMAGE_RED_INDEX] = temporary[IMAGE_Blue_INDEX][medainIndex];
                    _resultMap[imgX, imgY, IMAGE_Green_INDEX] = temporary[IMAGE_Blue_INDEX][medainIndex];
                    _resultMap[imgX, imgY, IMAGE_Blue_INDEX] = temporary[IMAGE_Blue_INDEX][medainIndex];
                }
        }
    }
}

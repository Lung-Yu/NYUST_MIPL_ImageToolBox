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
            int maskSize = (_efficWidth * _efficHeigh);
            int medainIndex =maskSize / 2;

            for (int imgY = 0; imgY < _height; imgY++)
                for (int imgX = 0; imgX < _width; imgX++)
                {

                    //mask processing
                    int index =0;
                    byte default_val = 0;
                    byte[] temporary_r = new byte[maskSize];
                    byte[] temporary_g = new byte[maskSize];
                    byte[] temporary_b = new byte[maskSize];
                    for (int y = _mask_y_start; y <= _mask_y_end; y++)
                        for (int x = _mask_x_start; x <= _mask_x_end; x++)
                        {
                            int new_x = x + imgX;
                            int new_y = y + imgY;

                            if (IsOutOfIndex(new_x, new_y))
                            {
                                temporary_r[index] = default_val;
                                temporary_g[index] = default_val;
                                temporary_b[index] = default_val;
                            }
                            else
                            {
                                temporary_r[index] = _imgMap[new_x, new_y, IMAGE_RED_INDEX];
                                temporary_g[index] = _imgMap[new_x, new_y, IMAGE_Green_INDEX];
                                temporary_b[index] = _imgMap[new_x, new_y, IMAGE_Blue_INDEX];
                            }
                            index++;
                        }

                    Array.Sort(temporary_r);
                    Array.Sort(temporary_g);
                    Array.Sort(temporary_b);

                    _resultMap[imgX, imgY, IMAGE_RED_INDEX] = temporary_r[medainIndex];
                    _resultMap[imgX, imgY, IMAGE_Green_INDEX] = temporary_g[medainIndex];
                    _resultMap[imgX, imgY, IMAGE_Blue_INDEX] = temporary_b[medainIndex];
                }
        }
    }
}

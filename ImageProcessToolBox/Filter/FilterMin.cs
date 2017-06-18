using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Filter
{
    class FilterMin : ImageBasicFilter
    {
        public override void process()
        {
            initMaskVal();

            for (int imgY = 0; imgY < _height; imgY++)
                for (int imgX = 0; imgX < _width; imgX++)
                {
                    
                    //mask processing
                    byte[] minTemporary = new byte[] { 255, 255, 255 };
                    for (int y = _mask_y_start; y < _mask_y_end; y++)
                        for (int x = _mask_x_start; x < _mask_x_end; x++)
                        {
                            int new_x = x + imgX;
                            int new_y = y + imgY;

                            if (IsOutOfIndex(new_x, new_y))
                            {
                                minTemporary[IMAGE_RED_INDEX] = 0;
                                minTemporary[IMAGE_Green_INDEX] = 0;
                                minTemporary[IMAGE_Blue_INDEX] = 0;
                                break;
                            }
                            else
                            {
                                if (minTemporary[IMAGE_RED_INDEX] > _imgMap[new_x, new_y, IMAGE_RED_INDEX])
                                    minTemporary[IMAGE_RED_INDEX] = _imgMap[new_x, new_y, IMAGE_RED_INDEX];
                                if (minTemporary[IMAGE_Green_INDEX] > _imgMap[new_x, new_y, IMAGE_Green_INDEX])
                                    minTemporary[IMAGE_Green_INDEX] = _imgMap[new_x, new_y, IMAGE_Green_INDEX];
                                if (minTemporary[IMAGE_Blue_INDEX] > _imgMap[new_x, new_y, IMAGE_Blue_INDEX])
                                    minTemporary[IMAGE_Blue_INDEX] = _imgMap[new_x, new_y, IMAGE_Blue_INDEX];
                            }
                        }

                    _resultMap[imgX, imgY, IMAGE_RED_INDEX] = minTemporary[IMAGE_RED_INDEX];
                    _resultMap[imgX, imgY, IMAGE_Green_INDEX] = minTemporary[IMAGE_Green_INDEX];
                    _resultMap[imgX, imgY, IMAGE_Blue_INDEX] = minTemporary[IMAGE_Blue_INDEX];
                }
        }

        
    }
}

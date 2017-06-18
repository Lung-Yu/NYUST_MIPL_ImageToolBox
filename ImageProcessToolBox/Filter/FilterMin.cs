using ImageProcessToolBox.BasicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Filter
{
    class FilterMin : ImageBasic
    {
        private int _efficWidth = 3;
        private int _efficHeigh = 3;

        int _mask_x_start;
        int _mask_x_end;

        int _mask_y_start;
        int _mask_y_end;

        private void init()
        {
            _mask_x_start = (_efficWidth / 2) * -1;
            _mask_x_end = _efficWidth / 2;

            _mask_y_start = (_efficHeigh / 2) * -1;
            _mask_y_end = _efficHeigh / 2;
        }

        public override void process()
        {
            init();

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
                            if (isOutOfIndex(new_x, new_y))
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

        protected bool isOutOfIndex(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
                return true;
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.BasicModel
{
    abstract class ImageBasicFilter : ImageBasic
    {
        protected int _efficWidth = 3;
        protected int _efficHeigh = 3;

        protected int _mask_x_start;
        protected int _mask_x_end;
        protected int _mask_y_start;
        protected int _mask_y_end;

        public int EfficHeigh
        {
            get { return _efficHeigh; }
            set { _efficHeigh = value; }
        }
        public int EfficWidth
        {
            get { return _efficWidth; }
            set { _efficWidth = value; }
        }

        protected void initMaskVal()
        {
            _mask_x_start = (_efficWidth / 2) * -1;
            _mask_x_end = _efficWidth / 2;

            _mask_y_start = (_efficHeigh / 2) * -1;
            _mask_y_end = _efficHeigh / 2;
        }

        protected bool IsOutOfIndex(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
                return true;
            else
                return false;
        }
    }
}

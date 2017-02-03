using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class ImageProcess
    {
        public static bool IsFilterOnSide(ref byte[,] pix, ref byte[,] resPix, int width, int height,int xOffset,int yOffset,int x, int y,int pos)
        {
            bool IsVild = false;
            if (x < xOffset || y < yOffset || x >= (width - xOffset) || y >= (height - yOffset))
            {
                resPix[0, pos] = pix[0, pos];
                resPix[1, pos] = pix[1, pos];
                resPix[2, pos] = pix[2, pos];
                IsVild = true;
            }
            return IsVild;
        }
       
        public static bool IsFilterOnSide(ref byte[,] pix, ref byte[,] resPix, int width, int height, int x, int y, int pos)
        {
            bool IsVild = false;
            if (x < 1 || y < 1 || x > (width - 1) || y > (height - 1))
            {
                resPix[0, pos] = pix[0, pos];
                resPix[1, pos] = pix[1, pos];
                resPix[2, pos] = pix[2, pos];
                IsVild = true;
            }
            return IsVild;
        }
    }
}

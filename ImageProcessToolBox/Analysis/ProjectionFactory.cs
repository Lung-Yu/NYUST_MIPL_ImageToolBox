using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Analysis
{
    class ProjectionFactory
    {
        Bitmap _srcImage;
        private int[] _horizontal;
        private int _MaxHorizontal;
        private int[] _vertical;
        private int _MaxVertical;
        private int _Th = 0;
        private byte[,] _imageMap;

        public ProjectionFactory(Bitmap src)
        {
            init(src);
            _Process();
        }
        public ProjectionFactory(Bitmap src, int t)
        {
            init(src);
            t = 10;
            _Process();
        }
        private void init(Bitmap src)
        {
            _srcImage = src;
            _imageMap = new byte[src.Width, src.Height];
            _horizontal = new int[src.Width];
            _vertical = new int[src.Height];
        }

        private void _Process()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            System.IntPtr srcScan;
            BitmapData srcBmData;
            ImageExtract.InitPonitMethod(_srcImage, width, height, out srcScan, out srcBmData);

            _MaxVertical = 0;
            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        //byte gray = (byte)(.299 * srcP[2] + .587 * srcP[1] + .114 * srcP[0]);
                        _imageMap[x, y] = srcP[2];
                    }
                    srcP += srcOffset;
                }
            }
            _srcImage.UnlockBits(srcBmData);
        }
        private void verticalProcess()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            int count = 0;
            for (int y = 0; y < height; y++)
            {
                count = 0;
                for (int x = 0; x < width; x++)
                {
                    if (_imageMap[x, y] > _Th)
                        count++;
                }
                _vertical[y] = count;
                if (count > _MaxVertical)
                    _MaxVertical = count;

            }
        }
        private void horizontalProcess()
        {
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            int hCount = 0;
            for (int x = 0; x < width; x++)
            {
                hCount = 0;
                for (int y = 0; y < height; y++)
                {
                    if (_imageMap[x, y] > _Th)
                        hCount++;
                }
                _horizontal[x] = hCount;
                if (hCount > _MaxHorizontal)
                    _MaxHorizontal = hCount;

            }
        }
        public int[] getVerticalProject()
        {
            verticalProcess();
            if (_MaxVertical == 0)
                return _vertical;

            for (int i = 0; i < _vertical.Length; i++)
                _vertical[i] = (int)((((float)_vertical[i]) / _MaxVertical) * 100);
            return _vertical;
        }

        public int[] getHorizontalProject()
        {
            horizontalProcess();
            if(_MaxHorizontal == 0)
                return _horizontal;

            for (int i = 0; i < _horizontal.Length; i++)
                _horizontal[i] = (int)((((float)_horizontal[i]) / _MaxHorizontal) * 100);
            return _horizontal;
        }
    }

}

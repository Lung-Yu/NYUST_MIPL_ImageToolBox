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
        private int[] _vertical;

        private int Max_horizontal = 0;
        private int Max_vertical = 0;
        private int _Threshold = 127;

        
        private byte[,] _imageMap;
        private bool _IsInverse = false;

        
        public ProjectionFactory(Bitmap src)
        {
            init(src);
            _Process();
        }
        public ProjectionFactory(Bitmap src, int t)
        {
            init(src);
            _Threshold = t;
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
            Max_vertical = 0;
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            int count = 0;
            for (int y = 0; y < height; y++)
            {
                count = 0;
                for (int x = 0; x < width; x++)
                {
                    if (!_IsInverse)
                    {
                        if (_imageMap[x, y] > _Threshold)
                            count++;
                    }
                    else
                    {
                        if (_imageMap[x, y] < _Threshold)
                            count++;
                    }

                }
                _vertical[y] = count;

                if (count > Max_vertical)
                    Max_vertical = count;
            }
        }
        private void horizontalProcess()
        {
            Max_horizontal = 0;
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            int count = 0;
            for (int x = 0; x < width; x++)
            {
                count = 0;
                for (int y = 0; y < height; y++)
                {
                    if (!_IsInverse)
                    {
                        if (_imageMap[x, y] > _Threshold)
                            count++;
                    }
                    else
                    {
                        if (_imageMap[x, y] < _Threshold)
                            count++;
                    }
                }
                _horizontal[x] = count;

                if (count > Max_horizontal)
                    Max_horizontal = count;
            }
        }
        public int[] getVerticalProject()
        {
            verticalProcess();

            for (int i = 0; i < _vertical.Length; i++)
                _vertical[i] = (int)((((float)_vertical[i]) / Max_vertical) * 100);
            return _vertical;
        }

        public int[] getHorizontalProject()
        {
            horizontalProcess();

            for (int i = 0; i < _horizontal.Length; i++)
                _horizontal[i] = (int)((((float)_horizontal[i]) / Max_horizontal) * 100);
            return _horizontal;
        }

        public bool IsInverse
        {
            get { return _IsInverse; }
            set { _IsInverse = value; }
        }
        public int Threshold
        {
            get { return _Threshold; }
            set { _Threshold = value; }
        }
    }

}

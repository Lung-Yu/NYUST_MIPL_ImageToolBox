using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Analysis
{
    class ProjectionWithValue
    {
        private Bitmap _srcImage;
        private int[] _vertical;
        private int[] _horizontal;
        private byte[,] _imageMap;
        private byte _rejectVal;

        public ProjectionWithValue(Bitmap srcImage, byte rejectVal)
        {
            _rejectVal = rejectVal;
            init(srcImage);
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
            int width = _srcImage.Width;
            int height = _srcImage.Height;

            int count = 0;
            for (int y = 0; y < height; y++)
            {
                count = 0;
                for (int x = 0; x < width; x++)
                {
                    if (_imageMap[x, y] == _rejectVal)
                        count++;
                }
                _vertical[y] = count;
            }
        }

        public int[] getVerticalProject()
        {
            verticalProcess();

            //for (int i = 0; i < _vertical.Length; i++)
            //    _vertical[i] = (int)((((float)_vertical[i]) / Max_vertical) * 100);
            return _vertical;
        }
    }
}

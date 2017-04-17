using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class TransformByLog : PointTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private int _C = 100;
        private static byte[] logs = null;
        public TransformByLog(int c)
        {
            _C = c;
            init(_C);
        }

        private static void init(int c)
        {
            logs = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                logs[i] = (byte)(c * Math.Log(i + 1, 10));
            }
        }

        public TransformByLog(int c, Bitmap bitmap)
        {
            _SourceImage = bitmap;
            _C = c;
            init(_C);
        }
        public TransformByLog(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            //return log(_SourceImage, _C);
            return base.process(_SourceImage);
        }

        protected override byte processColorR(byte r, byte g, byte b)
        {
            return logs[r];
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return logs[g];
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return logs[b];
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

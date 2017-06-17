using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class OldStyleFilter : PointTemplate, IImageProcess
    {
        //https://www.kancloud.cn/trent/hotoimagefilter/102798

        private Bitmap _SourceImage;
        public OldStyleFilter()
        {

        }
        public OldStyleFilter(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
        public Bitmap Process()
        {
            return base.process(_SourceImage);
        }

        protected override byte processColorR(byte r, byte g, byte b)
        {
            int gray = (393 * r + 769 * g + 189 * b) / 1000;
            return (byte)(Math.Min(255, Math.Max(0, gray)));
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            int gray = (349 * r + 686 * g + 168 * b) / 1000;
            return (byte)(Math.Min(255, Math.Max(0, gray)));
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            int gray = (272 * r + 534 * g + 131 * b) / 1000; ;
            return (byte)(Math.Min(255, Math.Max(0, gray)));
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MedianFilter : FilterTemplate, IImageProcess
    {
        private Bitmap _ImageSource;
        private int _MaskWidth=3;
        private int _MaskHeight=3;

        public MedianFilter()
        {
        }
        public MedianFilter(int w, int h)
        {
            _MaskWidth = w;
            _MaskHeight = h;
        }

        public MedianFilter(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }

        public MedianFilter(Bitmap bitmap,int w,int h)
        {
            _ImageSource = bitmap;
            _MaskWidth = w;
            _MaskHeight = h;
        }

        public Bitmap Process()
        {
            return base.convolute(_ImageSource, _MaskWidth, _MaskHeight);
        }
        
        protected override byte maskFilter(byte[] gate)
        {
            //Heap heap = new Heap(gate, gate.Length);
            //heap.heapsort();
            //return (byte)heap.get()[gate.Length / 2];
            //Console.Write((byte)heap.get()[gate.Length / 2]);

            Array.Sort(gate);
            //Console.Write((byte)(gate[gate.Length / 2]));
            return gate[gate.Length / 2];
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
    }
}

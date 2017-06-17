using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class MinFilter :FilterTemplate, IImageProcess
    {
        private Bitmap _ImageSource;
        private int _MaskWidth=3;
        private int _MaskHeight=3;

        public MinFilter()
        {
        }
        public MinFilter(int w, int h)
        {
            _MaskWidth = w;
            _MaskHeight = h;
        }

        public MinFilter(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }

        public MinFilter(Bitmap bitmap, int w, int h)
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
            Heap heap = new Heap(gate, gate.Length);
            heap.heapsort();
            return (byte)heap.get()[0];
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }
    }
    
}

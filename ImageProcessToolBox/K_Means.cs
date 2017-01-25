using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class K_Means : IImageProcess
    {
        private Bitmap _SourceImage;
        private int _K = 1;
        private int _IterationLevel = 10;
        public K_Means(int k, int iterationLevel, Bitmap bitmap)
        {
            _K = k;
            _IterationLevel = iterationLevel;
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            ImageClusteringAlgorithms.K_Means(_K, _IterationLevel, _SourceImage);
            return _SourceImage;
        }

    }
}

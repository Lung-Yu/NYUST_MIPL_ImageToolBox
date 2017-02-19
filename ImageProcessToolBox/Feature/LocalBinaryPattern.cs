using ImageProcessToolBox.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox.Feature
{
    class LocalBinaryPattern : FilterTemplate, IImageProcess , IFeatureExtract
    {
        // ref http://dataunion.org/20584.html
        private List<int> _Features = new List<int>();
        private StringBuilder _FeaturesString = new StringBuilder();
        private Bitmap _ImageSource;

        public Bitmap Process()
        {
            return base.convolute(_ImageSource, 3, 3);
        }

        public void setResouceImage(Bitmap bitmap)
        {
            _ImageSource = bitmap;
        }

        private byte medianValue;
        private int medianposition;
        private byte result;
        private double pow;
        protected override byte maskFilter(byte[] gate)
        {
            result = 0;
            medianposition = gate.Length / 2;
            medianValue = gate[medianposition];

            for (int i = 0; i < gate.Length; i++)
            {
                if (i == medianposition)
                    continue;
                if (gate[i] > medianValue)
                {
                    pow = (i - (i / medianposition));
                    result += (byte)Math.Pow(2, pow);
                }
            }

            _Features.Add(result);
            _FeaturesString.Append(string.Format("{0},",result));
            return result;
        }


        public List<int> getFeatures()
        {
            return _Features;
        }


        public string getFeaturesString()
        {
            return _FeaturesString.ToString();
        }
    }
}

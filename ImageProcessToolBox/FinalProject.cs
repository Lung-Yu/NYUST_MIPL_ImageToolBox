using ImageProcessToolBox.Analysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class FinalProject : IImageProcess
    {

        private Bitmap _srcImage;


        public Bitmap Process()
        {
            IImageProcess process1 = new Transfor(25);
            process1.setResouceImage(_srcImage);
            Bitmap res1 = process1.Process();

            IImageProcess process2 = new SpiltImage();
            process2.setResouceImage(res1);
            Bitmap res2 = process2.Process();

            IImageProcess process3 = new LaplacianBG();
            process3.setResouceImage(res2);
            Bitmap res3 = process3.Process();

            return res3;
        }




        public void setResouceImage(System.Drawing.Bitmap bitmap)
        {
            _srcImage = bitmap;
        }


    }    
}

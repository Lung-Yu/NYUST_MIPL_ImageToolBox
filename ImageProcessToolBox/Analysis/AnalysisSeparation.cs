using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class AnalysisSeparation
    {
        private Bitmap _ImageSouce;
        private Bitmap _ImageOfR;
        private Bitmap _ImageOfG;
        private Bitmap _ImageOfB;
        private Bitmap _ImageGray;

       
        public void Process()
        {
            _process(_ImageSouce);
        }

        private void _process(Bitmap srcBitmap)
        {
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;

            _ImageOfR = new Bitmap(srcBitmap);
            _ImageOfG = new Bitmap(srcBitmap);
            _ImageOfB = new Bitmap(srcBitmap);


            System.IntPtr srcScan, scan_R, scan_G, scan_B, scan_Gray;
            BitmapData srcBmData, BmData_R, BmData_G, BmData_B, BmData_Gray;
            
            _ImageGray = ImageExtract.InitPonitMethod(srcBitmap, width, height, out srcScan, out scan_Gray, out srcBmData, out BmData_Gray);
            ImageExtract.InitPonitMethod(_ImageOfR, width, height, out scan_R, out BmData_R);
            ImageExtract.InitPonitMethod(_ImageOfG, width, height, out scan_G, out BmData_G);
            ImageExtract.InitPonitMethod(_ImageOfB, width, height, out scan_B, out BmData_B);

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dst_R = (byte*)scan_R;
                byte* dst_G = (byte*)scan_G;
                byte* dst_B = (byte*)scan_B;
                byte* dst_Gray = (byte*)scan_Gray;


                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = BmData_Gray.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3, dst_R += 3,dst_G += 3,dst_B += 3,dst_Gray += 3)
                    {

                        byte gray = (byte)(.299 * srcP[2] + .587 * srcP[1] + .114 * srcP[0]);
                        dst_Gray[0] = dst_Gray[1] = dst_Gray[2] = gray;

                        dst_R[0] = dst_R[1] = dst_R[2] = srcP[2];
                        //dst_R[2] = srcP[2];
                        //dst_R[1] = dst_R[0] = 0;

                        dst_G[0] = dst_G[1] = dst_G[2] = srcP[1];
                        //dst_G[1] = srcP[1];
                        //dst_G[0] = dst_G[2] = 0;

                        dst_B[0] = dst_B[1] = dst_B[2] = srcP[0];
                        //dst_B[0] = srcP[0];
                        //dst_B[1] = dst_B[2] = 0;
                    }

                    srcP += srcOffset;
                    dst_R += dstOffset;
                    dst_G += dstOffset;
                    dst_B += dstOffset;
                    dst_Gray += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            _ImageGray.UnlockBits(BmData_Gray);
            _ImageOfR.UnlockBits(BmData_R);
            _ImageOfG.UnlockBits(BmData_G);
            _ImageOfB.UnlockBits(BmData_B);
        }
        

        public Bitmap ImageSouce
        {
            get { return _ImageSouce; }
            set { _ImageSouce = value; }
        }
        public Bitmap ImageOfG
        {
            get { return _ImageOfG; }
            set { _ImageOfG = value; }
        }
        public Bitmap ImageGray
        {
            get { return _ImageGray; }
            set { _ImageGray = value; }
        }
        public Bitmap ImageOfB
        {
            get { return _ImageOfB; }
            set { _ImageOfB = value; }
        }
        public Bitmap ImageOfR
        {
            get { return _ImageOfR; }
            set { _ImageOfR = value; }
        }
    }
}

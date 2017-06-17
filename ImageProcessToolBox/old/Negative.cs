using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Negative : PointTemplate, IImageProcess
    {
        private Bitmap _SourceImage;
        private readonly static int COLOR_SIZE_RANGE = 256;

        private static byte[] negatives = new byte[] { 
            255, 254, 253, 252, 251, 250, 249, 248, 247, 246,
            245, 244, 243, 242, 241, 240, 239, 238, 237, 236,
            235, 234, 233, 232, 231, 230, 229, 228, 227, 226,
            225, 224, 223, 222, 221, 220, 219, 218, 217, 216,
            215, 214, 213, 212, 211, 210, 209, 208, 207, 206,
            205, 204, 203, 202, 201, 200, 199, 198, 197, 196,
            195, 194, 193, 192, 191, 190, 189, 188, 187, 186,
            185, 184, 183, 182, 181, 180, 179, 178, 177, 176,
            175, 174, 173, 172, 171, 170, 169, 168, 167, 166,
            165, 164, 163, 162, 161, 160, 159, 158, 157, 156,
            155, 154, 153, 152, 151, 150, 149, 148, 147, 146,
            145, 144, 143, 142, 141, 140, 139, 138, 137, 136,
            135, 134, 133, 132, 131, 130, 129, 128, 127, 126,
            125, 124, 123, 122, 121, 120, 119, 118, 117, 116,
            115, 114, 113, 112, 111, 110, 109, 108, 107, 106, 
            105, 104, 103, 102, 101, 100, 99, 98, 97, 96,
            95,94, 93, 92, 91, 90, 89, 88, 87, 86,
            85,84, 83, 82, 81, 80, 79, 78, 77, 76,
            75,74, 73, 72, 71, 70, 69, 68, 67, 66,
            65,64, 63, 62, 61, 60, 59, 58, 57, 56,
            55,54, 53, 52, 51, 50, 49, 48, 47, 46,
            45,44, 43, 42, 41, 40, 39, 38, 37, 36,
            35,34, 33, 32, 31, 30, 29, 28, 27, 26,
            25,24, 23, 22, 21, 20, 19, 18, 17, 16,
            15,14, 13, 12, 11, 10, 9, 8, 7, 6,
            5,4, 3, 2, 1, 0 };

        public Negative()
        {

        }

        public Negative(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }

        public Bitmap Process()
        {
            return negative(_SourceImage);
        }

        private static Bitmap negative(Bitmap srcBitmap)
        {
            int width = srcBitmap.Width;
            int height = srcBitmap.Height;

            System.IntPtr srcScan, dstScan;
            BitmapData srcBmData, dstBmData;
            Bitmap dstBitmap = ImageExtract.InitPonitMethod(srcBitmap, width, height, out srcScan, out dstScan, out srcBmData, out dstBmData);


            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                byte* dstP = (byte*)dstScan;
                int srcOffset = srcBmData.Stride - width * 3;
                int dstOffset = dstBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3, dstP += 3)
                    {
                        //*dstP = (byte)(COLOR_SIZE_RANGE - srcP[0]);  //blue
                        //*(dstP + 1) = (byte)(COLOR_SIZE_RANGE - srcP[1]);  //green
                        //*(dstP + 2) = (byte)(COLOR_SIZE_RANGE - srcP[2]);  //red
                        
                        *dstP = negatives[srcP[0]];  //blue
                        *(dstP + 1) = negatives[srcP[1]];  //green
                        *(dstP + 2) = negatives[srcP[2]];  //red
                    }
                    srcP += srcOffset;
                    dstP += dstOffset;
                }
            }

            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }

        protected override byte processColorR(byte r, byte g, byte b)
        {
            return (byte)(COLOR_SIZE_RANGE - r);
        }

        protected override byte processColorG(byte r, byte g, byte b)
        {
            return (byte)(COLOR_SIZE_RANGE - g);
        }

        protected override byte processColorB(byte r, byte g, byte b)
        {
            return (byte)(COLOR_SIZE_RANGE - b);
        }


        public void setResouceImage(Bitmap bitmap)
        {
            _SourceImage = bitmap;
        }
    }
}

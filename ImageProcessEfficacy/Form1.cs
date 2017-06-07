using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessEfficacy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            int width = bitmap.Width;
            int height = bitmap.Height;
            string imageSize  = String.Format("{0}*{1}", width, height);
            label1.Text = imageSize;

            Stopwatch sw = TimeCountStart();
            pixcel(bitmap, width, height);
            string pixTime = TimeCountStop(sw);
            label2.Text = pixTime + "(ms)";
            pictureBox2.Image = bitmap;

            sw = TimeCountStart();
            interal(bitmap, width, height);
            string interalTime = TimeCountStop(sw);
            label3.Text = interalTime + "(ms)";
            pictureBox3.Image = bitmap;

            sw = TimeCountStart();
            point(bitmap, width, height);
            string pointTime = TimeCountStop(sw);
            label4.Text = pointTime + "(ms)";
            pictureBox4.Image = bitmap;
        }

        private void pixcel(Bitmap bitmap, int width, int height)
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    double r = bitmap.GetPixel(i, j).R;
                    double g = bitmap.GetPixel(i, j).G;
                    double b = bitmap.GetPixel(i, j).B;

                    byte gray = (byte)(.299 * r + .587 * g + .114 * b);

                    bitmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
        }

        private void interal(Bitmap bitmap, int width, int height)
        {
            int length = height * width * 3;
            byte[] colors = new byte[length];

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr scan0 = data.Scan0;
            Marshal.Copy(scan0, colors, 0, length);
            double gray = 0;
            for (int i = 0; i < length; i += 3){
                gray = colors[i + 2] * .299 + colors[i + 1] * 0.587 + colors[i] * 0.114;
                colors[i + 2] = colors[i + 1] = colors[i] = (byte)gray;
            }
            Marshal.Copy(colors, 0, scan0, length);
            bitmap.UnlockBits(data);
        }

        private void point(Bitmap bitmap, int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData BmData;
            IntPtr srcScan;

            //將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            BmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標srcPtr，為srcBmData的掃描行的開始位置
            srcScan = BmData.Scan0;

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;

                int srcOffset = BmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        byte gray = (byte)(.299 * *(srcP + 2) + .587 * *(srcP + 1) + .114 * *srcP);
                        *(srcP + 2) = *(srcP + 1) = *(srcP) = gray;
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(BmData);
        }


        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfDialog = new OpenFileDialog();
            opfDialog.Filter = @"(*.bmp,*.jpg)|*.bmp;*.jpg;*.png";
            opfDialog.FilterIndex = 3;
            opfDialog.RestoreDirectory = true;
            opfDialog.InitialDirectory = @"C:\\";
            if (opfDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = opfDialog.FileName;
            }
        }

        private Stopwatch TimeCountStart()
        {
            Stopwatch sw = new Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時
            return sw;
        }

        private string TimeCountStop(Stopwatch sw)
        {
            sw.Stop();//碼錶停止
            //印出所花費的總豪秒數
            return sw.Elapsed.TotalMilliseconds.ToString();
        }


    }
}

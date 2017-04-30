using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageSimilarity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile1_Click(object sender, EventArgs e)
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

        private void btnOpenFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfDialog = new OpenFileDialog();
            opfDialog.Filter = @"(*.bmp,*.jpg)|*.bmp;*.jpg;*.png";
            opfDialog.FilterIndex = 3;
            opfDialog.RestoreDirectory = true;
            opfDialog.InitialDirectory = @"C:\\";


            if (opfDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = opfDialog.FileName;
            }
        }

        public static int[] GetGrayImagePixels(Bitmap img)
        {
            BitmapData data = img.LockBits(new System.Drawing.Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int[] histogram = new int[256];
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 3;
                for (int i = 0; i < histogram.Length; i++)
                    histogram[i] = 0;
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        int mean = ptr[0] + ptr[1] + ptr[2];
                        mean /= 3;
                        histogram[mean]++;
                        ptr += 3;
                    }
                    ptr += remain;
                }
            }
            img.UnlockBits(data);
            return histogram;
        }

        private static float GetAbs(int firstNum, int secondNum)
        {
            float abs = Math.Abs((float)firstNum - (float)secondNum);
            float result = Math.Max(firstNum, secondNum);
            if (result == 0)
                result = 1;
            return abs / result;
        }

        //最終計算結果
        public static float GetResult(int[] firstNum, int[] scondNum)
        {
            if (firstNum.Length != scondNum.Length)
                return 0;

            float result = 0;
            int length = Math.Max(firstNum.Length, scondNum.Length);
            for (int i = 0; i < length; i++)
                result += 1 - GetAbs(firstNum[i], scondNum[i]);
            return result / length;
        }

        private void btnSimilarity_Click(object sender, EventArgs e)
        {
            int[] image1 = GetGrayImagePixels(new Bitmap(pictureBox1.Image, 400, 400));
            int[] image2 = GetGrayImagePixels(new Bitmap(pictureBox2.Image, 400, 400));

            float result = GetResult(image1, image2);

            label1.Text = "similarity : " + result;
        }
    }
}

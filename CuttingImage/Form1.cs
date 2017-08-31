using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuttingImage
{
    public partial class Form1 : Form
    {
        private Bitmap _img;
        private byte[, ,] _imgMap;
        private double _width;
        private double _height;

        private static readonly int pixelSizeWidth = 512;
        private static readonly int pixelSizeHeight = 512;
        int SIZE_WIDTH = 100;
        int SIZE_HEIGHT = 200;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
                return;


            Bitmap bitmap = new Bitmap(pictureBox2.Image);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = @"(*.bmp,*.jpg)|*.bmp;*.jpg;*.png";

            sfd.FilterIndex = 3;
            sfd.RestoreDirectory = true;
            if (DialogResult.OK == sfd.ShowDialog())
            {
                ImageFormat format = ImageFormat.Jpeg;
                switch (Path.GetExtension(sfd.FileName).ToLower())
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    default:
                        MessageBox.Show(this, "Unsupported image format was specified", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                try
                {
                    bitmap.Save(sfd.FileName, format);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Failed writing image file", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfDialog = new OpenFileDialog();
            opfDialog.Filter = @"(*.bmp,*.jpg,*.tif)|*.bmp;*.jpg;*.png;*.tif";
            opfDialog.FilterIndex = 3;
            opfDialog.RestoreDirectory = true;
            opfDialog.InitialDirectory = @"C:\\";

            if (opfDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = opfDialog.FileName;
                //_img = new Bitmap(opfDialog.FileName);
                initBitmap(opfDialog.FileName);
            }

        }


        private void initBitmap(string fileAddr)
        {
            _img = new Bitmap(fileAddr);
            _imgMap = pre(_img);

            _width = _img.Width;
            _height = _img.Height;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_img == null)
            {
                MessageBox.Show("無圖片");
                return;
            }

            //顯示滑鼠座標
            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();

            relocate(e.X, e.Y);
        }



        private void relocate(int x, int y)
        {
            //bool isWidth = false;
            //if (_width > _height)
            //    isWidth = true;

            //if (isWidth)
            //{
            //double wUnit = (double)pixelSizeWidth / _width;
            //double hUnit = (double)pixelSizeHeight / _height;


            //double offset = (_height / 2) * hUnit;
            //double footerSide = pixelSizeHeight - offset;

            //if (y < offset || y > footerSide)
            //{
            //    MessageBox.Show("超出範圍");
            //    return;
            //}
            //else
            //{
            try
            {
                byte[, ,] map = new byte[SIZE_WIDTH, SIZE_HEIGHT, 3];

                for (int ix = 0; ix < SIZE_WIDTH; ix++)
                {
                    for (int iy = 0; iy < SIZE_HEIGHT; iy++)
                    {
                        byte t = _imgMap[ix, iy, 0];
                        int ny = iy + y - SIZE_HEIGHT / 2;
                        int nx = ix + x - SIZE_WIDTH / 2;
                        map[ix, iy, 0] = _imgMap[nx, ny, 0];
                        map[ix, iy, 1] = _imgMap[nx, ny, 1];
                        map[ix, iy, 2] = _imgMap[nx, ny, 2];
                    }
                }


                Bitmap dst = new Bitmap(SIZE_WIDTH, SIZE_HEIGHT);
                writeBitmap(dst, map);

                pictureBox2.Image = dst;
            }
            catch (Exception)
            {

            }
            
            //}

            //}
            //else
            //{

            //}
        }

        private static byte[, ,] pre(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[, ,] resMatrix = new byte[width, height, 4];  //回傳矩陣 

            Rectangle rect = new Rectangle(0, 0, width, height);

            //將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            BitmapData srcBmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

            //位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃描行
            //目的是設兩個起始旗標srcPtr、dstPtr，為srcBmData、dstBmData的掃描行的開始位置
            System.IntPtr srcScan = srcBmData.Scan0;

            unsafe //啟動不安全代碼
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        resMatrix[x, y, 0] = *(srcP + 2);
                        resMatrix[x, y, 1] = *(srcP + 1);
                        resMatrix[x, y, 2] = *(srcP);
                    }
                    srcP += srcOffset;
                }
            }

            bitmap.UnlockBits(srcBmData);

            return resMatrix;
        }

        private static void writeBitmap(Bitmap bitmap, byte[, ,] martix)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData srcBmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            System.IntPtr srcScan = srcBmData.Scan0;

            unsafe
            {
                byte* srcP = (byte*)srcScan;
                int srcOffset = srcBmData.Stride - width * 3;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        //要修改
                        *(srcP + 2) = martix[x, y, 0];
                        *(srcP + 1) = martix[x, y, 1];
                        *(srcP) = martix[x, y, 2];
                    }
                    srcP += srcOffset;
                }
            }
            bitmap.UnlockBits(srcBmData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


}

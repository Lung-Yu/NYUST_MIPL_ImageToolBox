using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessToolBox
{
    public partial class Form1 : Form
    {
        private List<Button> Buttons = new List<Button>();

        public Form1()
        {
            InitializeComponent();

            Button[] btns = {
                                btnOpenFile,
                                btnSaveFile,
                                btnReplace,
                                btnDrawSource,
                                //point
                                btnGrayscale,
                                btnNegative,
                                btnPowerLaw,
                                btnOtus,
                                btn8BitPlaneSlicing,
                                btnBinarization,
                                btnLog,
                                btnExp,
                                //color
                                btnColouring,
                                btnColorFunction,

                                //space
                                btnAvgFiliter,
                                btnMeanFilter,
                                btnGaussianFilter,
                                btnLowPassFilters,
                                btnHighPassFilters,
                                btnFourierTransform,
                                btnSobel,
                                btnSpatialFilter,
                                btnLaplacian,
                                btnHistogramEqualization,
                                
                                //morphology
                                btnErosion,
                                btnSwell,
                                btnOpening,
                                btnClosing,

                                //segment
                                btnK_Means
                            };

            foreach (Button button in btns)
                Buttons.Add(button);
        }
        
        #region Menu
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

                labelconsle.Text += String.Format(">> open file : {0}\n", opfDialog.FileName);
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromResource();

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

        private void btnReplace_Click(object sender, EventArgs e)
        {
            Stopwatch sw = TimeCountStart();
            if (pictureBox2.Image == null)
            {
                UIMessage("Replace Image error", sw);
                return;
            }

            Bitmap bitmap = new Bitmap(pictureBox2.Image);
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bitmap;

            UIMessage("Replace Image", sw);
            drawSource(bitmap);
        }

        private void btnDrawSource_Click(object sender, EventArgs e)
        {
            drawSource(bitmapFromSource());
        }

        #endregion

        #region 共用函數
        private Bitmap execute(IImageProcess action, String actionString)
        {
            Stopwatch sw = TimeCountStart();
            Bitmap resBitmap = action.Process();
            UIMessage(actionString, sw);
            return resBitmap;
        }

        private void ButtonsEnable()
        {
            foreach (Button button in Buttons)
                button.Enabled = true;
        }

        private void ButtonDisable()
        {
            foreach (Button button in Buttons)
                button.Enabled = false;
        }

        private Stopwatch TimeCountStart()
        {
            Stopwatch sw = new Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            ButtonDisable();    //按鈕關閉
            sw.Start();//碼表開始計時
            return sw;
        }

        private string TimeCountStop(Stopwatch sw)
        {
            sw.Stop();//碼錶停止
            ButtonsEnable();    //按鈕開啟
            //印出所花費的總豪秒數
            return sw.Elapsed.TotalMilliseconds.ToString();
        }

        private Bitmap bitmapFromSource()
        {
            Image img = pictureBox1.Image;
            if (img == null) return new Bitmap(5, 5);



            Bitmap bitmap = new Bitmap(img);
            string labelStr = String.Format("{0}*{1}", img.Width, img.Height);
            labelImageSize.Text = labelStr;

            return bitmap;
        }

        private Bitmap bitmapFromResource()
        {
            Image img = pictureBox2.Image;
            if (img == null) return null;

            Bitmap bitmap = new Bitmap(img);
            return bitmap;
        }

        private void UIMessage(String action, Stopwatch sw)
        {
            String time = TimeCountStop(sw);
            labelconsle.Text = ">> Image " + action + ". cost: " + time + "(ms)\n" + labelconsle.Text;
        }

        private void setResultBitmap(Bitmap bitmap)
        {
            if (pictureBox2.Image != null)
                pictureBox2.Image.Dispose();

            pictureBox2.Image = bitmap;


            string labelStr = String.Format("{0}*{1}", bitmap.Width, bitmap.Height);
            resultImageSize.Text = labelStr;

            drawResult(bitmap);
        }

        private readonly static int DRAW_INTERVAL = 2;
        private void draw(Graphics[] graphicses, Bitmap bitmap)
        {
            int[,] statistics = MyColouring.Statistics(bitmap);
            
            // find Max value
            int MAX = 0;
            foreach (int value in statistics)
                if (value > MAX)
                    MAX = value;

            Pen pen = new Pen(Color.Black, 1);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;  //EndCap設定 這支筆的結尾會是個箭頭 ArrowAnchor
            for (int c = 0; c < graphicses.Length; c++)
            {
                graphicses[c].Clear(Color.White);
                int value = 0;
                for (int x = 0; x < 256; x++)
                {
                    if (x % DRAW_INTERVAL == 0)
                    {
                        value += (int)(((double)(MAX - statistics[c, x]) / MAX * 100));
                        graphicses[c].DrawLine(pen, x / DRAW_INTERVAL, 100, x / 2, value / DRAW_INTERVAL);
                    }
                    else
                    {
                        value = (int)(((double)(MAX - statistics[c, x]) / MAX * 100));
                    }

                }
            }
        }

        private void drawSource(Bitmap bitmap)
        {
            Graphics[] graphicses = {sourceGraphicsB.CreateGraphics(),
                                    sourceGraphicsG.CreateGraphics(),
                                    sourceGraphicsR.CreateGraphics()};
            draw(graphicses, bitmap);
        }

        private void drawResult(Bitmap bitmap)
        {
            Graphics[] graphicses = {ResultGraphicsB.CreateGraphics(),
                                    ResultGraphicsG.CreateGraphics(),
                                    ResultGraphicsR.CreateGraphics()};
            draw(graphicses, bitmap);
        }

        #endregion
        
        #region My Colors 
        private void btnColouring_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap coluring = new Bitmap(pictureBox3.Image);
            Bitmap resBitmap = execute(new MyColouring(bitmap, coluring), "MyColouring");
            setResultBitmap(resBitmap);
        }

        private void btnColorFunction_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MyColorFunction(bitmap), "MyColorFunction");
            setResultBitmap(resBitmap);
        }
        #endregion

        #region Point Processing
        private void btnGrayscale_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MeanFilter(bitmap), "Grayscale");
            setResultBitmap(resBitmap);
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new Negative(bitmap), "Negative");
            setResultBitmap(resBitmap);
        }

        private void btnOtus_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();

            Stopwatch sw = TimeCountStart();
            int colorR = ImagePretreatment.ThresholdingIterativeWithR(bitmap);
            int colorG = ImagePretreatment.ThresholdingIterativeWithG(bitmap);
            int colorB = ImagePretreatment.ThresholdingIterativeWithB(bitmap);
            string labeStr = String.Format("({0},{1},{2})", colorR, colorG, colorB);
            labelOtus.Text = labeStr;
            UIMessage("Otus : " + labeStr, sw);
        }

        private void btn8BitPlaneSlicing_Click(object sender, EventArgs e)
        {
            int value = (int)numeric8BitPlaneSlicing.Value;
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new BitOf8PlaneSlicing(value, bitmap), "8 Bit Plane Slicing by " + value);
            setResultBitmap(resBitmap);
        }

        private void btnBinarization_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtBinarization.Text);

            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new Binarization(value, bitmap), "binarized Image by " + value);
            setResultBitmap(resBitmap);
        }

        private void btnPowerLaw_Click(object sender, EventArgs e)
        {
            double c = double.Parse(txtLog.Text);
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new TransformPowerLaw(c, bitmap), "PowLaw");
            setResultBitmap(resBitmap);
        }

        #endregion

        #region 空間轉換 Space filter
        private void btnMeanFilter_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MedianFilter(bitmap), "Median Filter");
            setResultBitmap(resBitmap);
        }

        private void btnAvgFiliter_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MeanFilter(bitmap), "Mean Filter");
            setResultBitmap(resBitmap);
        }

        private void btnGaussianFilter_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new GaussianFilter(bitmap), "GaussianFilter");
            setResultBitmap(resBitmap);
        }

        private void btnLowPassFilters_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new LowPassFilter(bitmap), "Low Pass Filters");
            setResultBitmap(resBitmap);
        }

        private void btnHighPassFilters_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new HighPassFilter(bitmap), "High Pass Filters");
            setResultBitmap(resBitmap);
        }

        private void btnFourierTransform_Click(object sender, EventArgs e)
        {
        }

        private void btnSobel_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new Sobel(bitmap), "Sobel");
            setResultBitmap(resBitmap);
        }

        private void btnSpatialFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnLaplacian_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new Sobel(bitmap), "Laplacian");
            setResultBitmap(resBitmap);
        }

        private void btnHistogramEqualization_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new HistogramEqualization(bitmap), "Histogram Equalization");
            setResultBitmap(resBitmap);
        }
        #endregion

        #region 空間轉換 Space filter

        private void btnErosion_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MeanFilter(bitmap), "Erosion");
            setResultBitmap(resBitmap);
        }

        private void btnSwell_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MorphologyDilation(bitmap), "Mean Filter");
            setResultBitmap(resBitmap);
        }

        private void btnOpening_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MorphologyOpening(bitmap), "Opening");
            setResultBitmap(resBitmap);
        }

        private void btnClosing_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MorphologyClosing(bitmap), "Closing");
            setResultBitmap(resBitmap);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            int c = (int)double.Parse(txtLog.Text);
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new TransformByLog(c, bitmap), "Log Transform");
            setResultBitmap(resBitmap);
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            int c = (int)double.Parse(txtLog.Text);
            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new TransformByExp(c, bitmap), "Exp Transform");
            setResultBitmap(resBitmap);
        }

        #endregion

        #region K Means
        private void btnK_Means_Click(object sender, EventArgs e)
        {
            int k = int.Parse(txtKValue.Text);
            int iterationLevel = int.Parse(txtIterationLevel.Text);

            Bitmap bitmap = bitmapFromSource();
            Bitmap resBitmap = execute(new MachineLearing_KMeans(k, iterationLevel, bitmap), "K Means");
            setResultBitmap(resBitmap);
        }
        #endregion

    }
}

using ImageProcessToolBox.Feature;
using ImageProcessToolBox.Interface;
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
                                btnRestore,
                                btnAnalysis,

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
                                btnSobel,
                                btnLaplacian,
                                btnHistogramEqualization,
                                btnMosaic,
                                btnRippleEffect,
                                btnInstagram1977,
                                btnLomo,
                                btnSketch,
                                btnSwirl,
                                btnOldStyle,
                                btnSpherize,
                                btnRelief,
                                btnMinFilter,
                                btnN45Degree,
                                btn45Degree,
                                btnVertical,
                                btnHorizontal,
                                btnPrewittFilter,
                                btnMaxFilter,
                                
                                //morphology
                                btnErosion,
                                btnSwell,
                                btnOpening,
                                btnClosing,

                                //segment
                                btnK_Means,

                                btnDWT,
                                btnDFT,
                                btnIDFT,
                                btnLBP
                            };

            foreach (Button button in btns)
                Buttons.Add(button);
        }

        #region Menu
        private string temporary_fileName = "";

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfDialog = new OpenFileDialog();
            opfDialog.Filter = @"(*.bmp,*.jpg,*.tif)|*.bmp;*.jpg;*.png;*.tif";
            opfDialog.FilterIndex = 3;
            opfDialog.RestoreDirectory = true;
            opfDialog.InitialDirectory = @"C:\\";


            if (opfDialog.ShowDialog() == DialogResult.OK)
            {
                temporary_fileName = opfDialog.FileName;
                pictureBox1.ImageLocation = opfDialog.FileName;

                labelconsle.Text += String.Format(">> open file : {0}\n", opfDialog.FileName);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(temporary_fileName))
            {
                MessageBox.Show("尚無開啟紀錄");
            }
            else
            {
                pictureBox1.ImageLocation = temporary_fileName;
                UIMessage(String.Format(">> restore file : {0}", temporary_fileName));
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
        private void btnAnalysis_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("請先開啟圖片方可進行分析");
                return;
            }

            Bitmap imageSource = new Bitmap(pictureBox1.Image);
            Form form = new FormAnalysis(imageSource);
            form.Show();
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
        private IImageProcess actions(IImageProcess action, String actionString)
        {
            Bitmap bitmap = bitmapFromSource();

            Stopwatch sw = TimeCountStart();
            action.setResouceImage(bitmap);
            Bitmap resBitmap = action.Process();

            UIMessage(actionString, sw);
            setResultBitmap(resBitmap);

            return action;
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

        private void UIMessage(String action)
        {
            labelconsle.Text = action + "\n" + labelconsle.Text;
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
        private readonly static int GraphicsHieght = 100;
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
                        value += (int)(((double)(MAX - statistics[c, x]) / MAX * GraphicsHieght));
                        graphicses[c].DrawLine(pen, x / DRAW_INTERVAL, GraphicsHieght, x / 2, value / DRAW_INTERVAL);
                    }
                    else
                    {
                        value = (int)(((double)(MAX - statistics[c, x]) / MAX * GraphicsHieght));
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
            Bitmap coluring = new Bitmap(pictureBox3.Image);
            actions(new MyColouring(coluring), "MyColouring");
        }

        private void btnColorFunction_Click(object sender, EventArgs e)
        {
            actions(new MyColorFunction(), "MyColorFunction");
        }
        #endregion

        #region Point Processing
        private void btnGrayscale_Click(object sender, EventArgs e)
        {
            actions(new Grayscale(), "Grayscale");
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            actions(new Negative(), "Negative");
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
            actions(new BitOf8PlaneSlicing(value), "8 Bit Plane Slicing by " + value);
        }

        private void btnBinarization_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtBinarization.Text);
            actions(new Binarization(value), "binarized Image by " + value);
        }

        private void btnPowerLaw_Click(object sender, EventArgs e)
        {
            double c = double.Parse(txtLog.Text);
            actions(new TransformPowerLaw(c), "PowLaw");
        }

        #endregion

        #region 空間轉換 Space filter
        private void btnMeanFilter_Click(object sender, EventArgs e)
        {
            actions(new MedianFilter((int)numericUpDownX.Value, (int)numericUpDownY.Value), "Median Filter");
        }

        private void btnMinFilter_Click(object sender, EventArgs e)
        {
            actions(new MinFilter((int)numericUpDownX.Value, (int)numericUpDownY.Value), "Min Filter");
        }

        private void btnMaxFilter_Click(object sender, EventArgs e)
        {
            actions(new MaxFilter((int)numericUpDownX.Value, (int)numericUpDownY.Value), "Max Filter");
        }

        private void btnAvgFiliter_Click(object sender, EventArgs e)
        {
            actions(new MeanFilter((int)numericUpDownX.Value, (int)numericUpDownY.Value), "Mean Filter");
        }

        private void btnGaussianFilter_Click(object sender, EventArgs e)
        {
            actions(new GaussianFilter(), "GaussianFilter");
        }

        private void btnLowPassFilters_Click(object sender, EventArgs e)
        {
            actions(new LowPassFilter(), "Low Pass Filters");
        }

        private void btnHighPassFilters_Click(object sender, EventArgs e)
        {
            actions(new HighPassFilter(), "High Pass Filters");
        }

        private void btnSobel_Click(object sender, EventArgs e)
        {
            actions(new Sobel(), "Sobel");
        }

        private void btnLaplacian_Click(object sender, EventArgs e)
        {
            actions(new Laplacian(), "Laplacian");
        }

        private void btnHistogramEqualization_Click(object sender, EventArgs e)
        {
            actions(new HistogramEqualization(), "Histogram Equalization");
        }

        private void btnMosaic_Click(object sender, EventArgs e)
        {
            actions(new Mosaic(), "Mosaic Filter");
        }

        private void btnRippleEffect_Click(object sender, EventArgs e)
        {
            actions(new RippleEffect(), "Ripple Effect Filter");
        }
        private void btnSpherize_Click(object sender, EventArgs e)
        {
            actions(new SpherizeFilter(), "Spherize Filter");
        }

        private void btnInstagram1977_Click(object sender, EventArgs e)
        {
            actions(new Instagram1977(), "Spherize Filter");
        }
        private void btnLomo_Click(object sender, EventArgs e)
        {
            actions(new LomoFilter(), "Spherize Filter");
        }

        private void btnSketch_Click(object sender, EventArgs e)
        {
            actions(new SketchFilter(), "Sketch Filter");
        }
        private void btnSwirl_Click(object sender, EventArgs e)
        {
            actions(new SwirlFilter(), "Swirl Filter");
        }
        private void btnOldStyle_Click(object sender, EventArgs e)
        {
            actions(new OldStyleFilter(), "OldStyle Filter");
        }

        private void btnRelief_Click(object sender, EventArgs e)
        {
            actions(new ReliefFilter(), "Relief Filter");
        }

        #endregion

        #region 空間轉換 Space filter

        private void btnErosion_Click(object sender, EventArgs e)
        {
            actions(new MorphologyErosion(), "Erosion");
        }

        private void btnSwell_Click(object sender, EventArgs e)
        {
            actions(new MorphologyDilation(), "Dilation");
        }

        private void btnOpening_Click(object sender, EventArgs e)
        {
            actions(new MorphologyOpening(), "Opening");
        }

        private void btnClosing_Click(object sender, EventArgs e)
        {
            actions(new MorphologyClosing(), "Closing");
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            int c = (int)double.Parse(txtLog.Text);
            actions(new TransformByLog(c), "Log Transform");
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            int c = (int)double.Parse(txtLog.Text);
            actions(new TransformByExp(c), "Exp Transform");
        }

        #endregion

        #region K Means
        private void btnK_Means_Click(object sender, EventArgs e)
        {
            int k = int.Parse(txtKValue.Text);
            int iterationLevel = int.Parse(txtIterationLevel.Text);
            actions(new MachineLearing_KMeans(k, iterationLevel), "K Means");
        }
        #endregion

        private void btnTry_Click(object sender, EventArgs e)
        {
            //actions(new DiscreteFourierTransform(), "DiscreteFourierTransform");
            //actions(new FinalProject(), "Final Project");
            //new ReaderTextFile();

            //if (pictureBox1.Image == null)
            //{
            //    MessageBox.Show("請先開啟圖片方可進行分析");
            //    return;
            //}

            //Bitmap imageSource = new Bitmap(pictureBox1.Image);
            //Form form = new FormMedicalImageFinal(imageSource);
            //form.Show();

            actions(new CutHW(), "CutHW Left");
        }

        private void btnVertical_Click(object sender, EventArgs e)
        {
            actions(new VerticalFilter(), "Vertical Filter");
        }

        private void btnHorizontal_Click(object sender, EventArgs e)
        {
            actions(new HorizontalFilter(), "Horizontal Filter");
        }

        private void btn45Degree_Click(object sender, EventArgs e)
        {
            actions(new Degree45Filter(), "45 degree Filter");
        }

        private void btnN45Degree_Click(object sender, EventArgs e)
        {
            actions(new DegreesNegative45Filter(), "-45 degree Filter");
        }

        private void btnPrewittFilter_Click(object sender, EventArgs e)
        {
            actions(new PrewittFilter(), "Prewitt Filter");
        }

        private void btnDWT_Click(object sender, EventArgs e)
        {
            actions(new DiscreteWaveletTransformation(), "Discrete Wavelet Transformation");
        }

        private void btnLBP_Click(object sender, EventArgs e)
        {
            Object actionObj = actions(new LocalBinaryPattern(), "Local Binary Pattern");
            if (actionObj is IFeatureExtract)
            {
                IFeatureExtract extractor = (IFeatureExtract)actionObj;
                //string str =  extractor.getFeaturesString();
            }
        }

        private void btnGaussianNoise_Click(object sender, EventArgs e)
        {
            decimal mean = numericUpDown1.Value;
            decimal sd = numericUpDown2.Value;

            actions(new NoiseGaussian((int)mean, (int)sd), "Gaussian Noise");
        }

        private void btnDFT_Click(object sender, EventArgs e)
        {
            actions(new DiscreteFourierTransform(), "Discrete Fourier Transform");
        }

        private void btnIDFT_Click(object sender, EventArgs e)
        {
            DiscreteFourierTransform dft = new DiscreteFourierTransform();
            dft.setInverse(true);
            actions(new DiscreteFourierTransform(), "Discrete Fourier Transform");
        }

        private void btnMeanShift_Click(object sender, EventArgs e)
        {
            //int d = int.Parse(textBox1.Text);
            //actions(new MachineLearing_MeanShift(d), "Mean Shift");
        }

        private void btnThyroid_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("請先開啟圖片方可進行分析");
                return;
            }

            Bitmap imageSource = new Bitmap(pictureBox1.Image);
            Form form = new FormMedicalImageFinal(imageSource);
            form.Show();
        }
    }
}

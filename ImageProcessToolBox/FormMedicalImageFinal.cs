using ImageProcessToolBox.Analysis;
using ImageProcessToolBox.MedicalImageFinal;
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

namespace ImageProcessToolBox
{
    public partial class FormMedicalImageFinal : Form
    {
        private Bitmap _imageSource;
        public FormMedicalImageFinal(Bitmap imgSrc)
        {
            InitializeComponent();
            _imageSource = imgSrc;
            pictureBoxOriginal.Image = _imageSource;
            init();
            process();
        }
        private Label[] labels;
        private PictureBox[] imageShow;
        private String[] labelsStr = { "Step1:", "Step2:", "Step3:", "Step4:" };

        private IImageProcess[] iProcess = { new Transfor(25), new SpiltImage(), new LaplacianBG() };
        private static int STEP_SIZE = 3;

        private void init()
        {
            labels = new Label[] { label1, label2, label3, label4, label5, label6, label7 };
            imageShow = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 };
        }
        private void process()
        {
            //Bitmap src = _imageSource;
            //for (int i = 0; i < STEP_SIZE; i++)
            //{
            //    labels[i].Text = labelsStr[i];
            //    src = Process(src, iProcess[i], imageShow[i]);
            //}
            //step4(src);
            test6(_imageSource);
        }

        private void test6(Bitmap src)
        {
            label5.Text = "Step 6 : Ans";

            //step 1
            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            IImageProcess ipro1 = new Transfor(th);
            ipro1.setResouceImage(src);
            Bitmap res1 = ipro1.Process();

            IImageProcess ipro1_1 = new TransformPowerLaw(1.2);
            ipro1_1.setResouceImage(res1);
            Bitmap res1_1 = ipro1_1.Process();

            IImageProcess ipro2 = new SpiltImage();
            ipro2.setResouceImage(res1_1);
            Bitmap res2 = ipro2.Process();
            //pictureBox1.Image = res2;
            pictureBox1.Image = new Mosaic(5, res2).Process();


            //step 2
            IImageProcess ipro3 = new MachineLearing_KMeans(3, 10);
            ipro3.setResouceImage(res2);
            Bitmap res3 = ipro3.Process();
            pictureBox2.Image = res3;



            //step 3
            byte[,] ks = ((MachineLearing_KMeans)ipro3).CenterPoints;
            int maxIndex = rejectIndexByRow(res3, ks[2, 2]);
            IImageProcess ipro3_1 = new BandRejectByRowIndex(0, maxIndex);
            ipro3_1.setResouceImage(res3);
            Bitmap res3_1 = ipro3_1.Process();

            byte passVal = ks[1, 2];
            IImageProcess ipro4 = new Bandpass(passVal, 0);
            ipro4.setResouceImage(res3_1);
            Bitmap res4 = ipro4.Process();
            pictureBox3.Image = res4;

            //step 4
            Bitmap res2_2 = rejectStains(res4);
            pictureBox4.Image = res2_2;


            //step 5
            Bitmap run2_input = rectangleofInterested(src, res2_2);
            pictureBox5.Image = run2_input;

            Mosaic mosaic = new Mosaic(20, run2_input);
            Bitmap moscaicImage = mosaic.Process();

            IImageProcess k_mean = new MachineLearing_KMeans(3, 10);
            k_mean.setResouceImage(moscaicImage);
            Bitmap k_mean_result = k_mean.Process();
            byte[,] ks2 = ((MachineLearing_KMeans)k_mean).CenterPoints;

            IImageProcess bandPass = new Bandpass(ks2[1, 2], 0);
            bandPass.setResouceImage(k_mean_result);
            Bitmap bandPassAns = bandPass.Process();

            //step 7
            Bitmap ans = new MakeImageFrame(src, bandPassAns).Process();
            pictureBox6.Image = ans;
        }


        private void test5(Bitmap src)
        {
            label5.Text = "Step 5 : Test";

            //step 1
            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            IImageProcess ipro1 = new Transfor(th);
            ipro1.setResouceImage(src);
            Bitmap res1 = ipro1.Process();

            IImageProcess ipro2 = new SpiltImage();
            ipro2.setResouceImage(res1);
            Bitmap res2 = ipro2.Process();
            pictureBox1.Image = res2;


            //step 2
            IImageProcess ipro3 = new MachineLearing_KMeans(3, 10);
            ipro3.setResouceImage(res2);
            Bitmap res3 = ipro3.Process();

            byte[,] ks = ((MachineLearing_KMeans)ipro3).CenterPoints;

            pictureBox2.Image = res3;


            //step 3
            int maxIndex = rejectIndexByRow(res3, ks[2, 2]);
            IImageProcess ipro3_1 = new BandRejectByRowIndex(0, maxIndex);
            ipro3_1.setResouceImage(res3);
            Bitmap res3_1 = ipro3_1.Process();

            byte passVal = ks[1, 2];
            IImageProcess ipro4 = new Bandpass(passVal, 0);
            ipro4.setResouceImage(res3_1);
            Bitmap res4 = ipro4.Process();
            pictureBox3.Image = res4;

            //step 4
            Bitmap res2_2 = rejectStains(res4);
            pictureBox4.Image = res2_2;


            //step 5
            Bitmap run2_input = rectangleofInterested(src, res2_2);
            pictureBox5.Image = run2_input;


        }

        private Bitmap rectangleofInterested(Bitmap src, Bitmap refImg)
        {
            ProjectionFactory factory = new ProjectionFactory(refImg, 1);
            int[] hps = factory.getHorizontalProject();
            int[] vps = factory.getVerticalProject();

            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            for (int i = 0; i < hps.Length; i++)
                if (hps[i] != 0)
                {
                    x1 = i;
                    break;
                }
            for (int i = hps.Length - 1; i >= 0; i--)
                if (hps[i] != 0)
                {
                    x2 = i;
                    break;
                }


            for (int i = 0; i < vps.Length; i++)
                if (vps[i] != 0)
                {
                    y1 = i;
                    break;
                }

            for (int i = vps.Length - 1; i >= 0; i--)
                if (vps[i] != 0)
                {
                    y2 = i;
                    break;
                }

            Console.Write(string.Format("x1={0},x2={1},y1={2},y2={3}", x1, x2, y1, y2));

            IImageProcess iprc = new BandPassByColIndex(y1, y2);
            iprc.setResouceImage(src);
            Bitmap res1 = iprc.Process();

            IImageProcess iprc2 = new BandPassByRowIndex(x1, x2);
            iprc2.setResouceImage(res1);
            Bitmap res2 = iprc2.Process();

            return res2;
        }

        private Bitmap rejectStains(Bitmap src)
        {
            ProjectionFactory factory = new ProjectionFactory(src);
            factory.Threshold = 1;
            int[] h = factory.getHorizontalProject();
            int x1 = 0, x2 = 0, ix1 = 0, ix2 = 0;
            int median = h.Length / 2;

            int t = 0;
            for (int i = 0; i < median; i++)
            {
                if (t == 0 && h[i] != 0)
                {
                    t = 1;
                    x1 = i - 1;
                }
                if (t == 1 && h[i] == 0)
                {
                    x2 = i;
                    break;
                }
            }

            t = 0;
            for (int i = h.Length - 1; i >= median; i--)
            {
                if (t == 0 && h[i] != 0)
                {
                    t = 1;
                    ix2 = i + 1;
                }
                if (t == 1 && h[i] == 0)
                {
                    ix1 = i;
                    break;
                }
            }

            IImageProcess iproc = new BandRejectByColIndex(x1, x2);
            iproc.setResouceImage(src);
            Bitmap res = iproc.Process();

            IImageProcess iproc2 = new BandRejectByColIndex(ix1, ix2);
            iproc2.setResouceImage(res);
            Bitmap res2 = iproc2.Process();

            return res2;

            //Console.WriteLine(string.Format("x1={0},x2={1}", x1, x2));
            //Console.WriteLine(string.Format("ix1={0},ix2={1}", ix1, ix2));
        }

        private int rejectIndexByRow(Bitmap src, byte rejectVal)
        {
            ProjectionWithValue factory = new ProjectionWithValue(src, rejectVal);
            int[] v = factory.getVerticalProject();
            int range = v.Length / 4;
            int Max = 0;
            int Max_Index = 0;
            for (int i = 100; i < range; i++)
                if (Max < v[i])
                {
                    Max = v[i];
                    Max_Index = i;
                }
            return Max_Index;
        }

        private Bitmap step4(Bitmap src)
        {
            label4.Text = "Step 4 : ";

            IImageProcess process = new MorphologyDilation();
            process.setResouceImage(src);
            return Process(src, process, pictureBox4); ;
        }


        private Bitmap Process(Bitmap src, IImageProcess Iprocess, PictureBox show)
        {
            Iprocess.setResouceImage(src);
            Bitmap res = Iprocess.Process();
            show.Image = res;
            return res;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

            if (((PictureBox)sender).Image == null)
            {
                MessageBox.Show("請先開啟圖片方可進行分析");
                return;
            }

            Bitmap imageSource = new Bitmap(((PictureBox)sender).Image);
            Form form = new FormAnalysis(imageSource);
            form.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox5.Image);
            saveFile(bitmap);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox6.Image);
            saveFile(bitmap);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox7.Image);
            saveFile(bitmap);
        }
        private void saveFile(Bitmap bitmap)
        {
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




    }
}

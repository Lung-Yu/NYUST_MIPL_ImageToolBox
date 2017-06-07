using ImageProcessToolBox.Analysis;
using ImageProcessToolBox.MedicalImageFinal;
using ImageProcessToolBox.MedicalImageFinal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            Bitmap src = _imageSource;
            for (int i = 0; i < STEP_SIZE; i++)
            {
                labels[i].Text = labelsStr[i];
                src = Process(src, iProcess[i], imageShow[i]);
            }
            step4(src);
            test5(_imageSource);
        }

        private void test5(Bitmap src)
        {
            label5.Text = "Step 5 : Test";

            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            IImageProcess ipro1 = new Transfor(th);
            ipro1.setResouceImage(src);
            Bitmap res1 = ipro1.Process();

            IImageProcess ipro2 = new SpiltImage();
            ipro2.setResouceImage(res1);
            Bitmap res2 = ipro2.Process();

            IImageProcess ipro3 = new MachineLearing_KMeans(3, 10);
            ipro3.setResouceImage(res2);
            Bitmap res3 = ipro3.Process();

            byte[,] ks = ((MachineLearing_KMeans)ipro3).CenterPoints;

            pictureBox6.Image = res3;

            int maxIndex = rejectIndexByRow(res3, ks[2, 2]);
            IImageProcess ipro3_1 = new BandRejectByRowIndex(0, maxIndex);
            ipro3_1.setResouceImage(res3);
            Bitmap res3_1 = ipro3_1.Process();

            byte passVal = ks[1, 2];
            IImageProcess ipro4 = new Bandpass(passVal, 0);
            ipro4.setResouceImage(res3_1);
            Bitmap res4 = ipro4.Process();

            IImageProcess ipro5 = new MorphologyDilation();
            ipro5.setResouceImage(res4);
            Bitmap res5 = ipro5.Process();

            pictureBox5.Image = res5;
        }

        private int rejectIndexByRow(Bitmap src, byte rejectVal)
        {
            ProjectionWithValue factory = new ProjectionWithValue(src, rejectVal);
            int[] v = factory.getVerticalProject();
            int range = v.Length / 4;
            int Max = 0;
            int Max_Index = 0;
            for (int i = 0; i < range; i++)
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
    }
}

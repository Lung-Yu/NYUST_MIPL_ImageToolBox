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
            //test6_ok(_imageSource);
            test8(_imageSource);
        }
        private void test8(Bitmap src)
        {
            IImageProcess action;
            Bitmap res;
            #region MedianFilter
            action = new MedianFilter(15, 15);
            action.setResouceImage(src);
            res = action.Process();

            pictureBox1.Image = res;
            label1.Text = "Filters";
            #endregion


            #region k - means + bandPass
            action = new MachineLearing_KMeans(3, 10);
            action.setResouceImage(new Bitmap(res));
            res = action.Process();

            byte[,] cp = ((MachineLearing_KMeans)action).CenterPoints;
            action = new Bandpass(cp[1, 2], 0);
            action.setResouceImage(res);
            res = action.Process();

            pictureBox2.Image = res;
            label2.Text = "means + bandPass";
            #endregion


            #region
            int th = ImagePretreatment.ThresholdingIterativeWithR(src);

            ProjectionFactory factory = new ProjectionFactory(src, th);
            int[] v = factory.getVerticalProject();
            int vEnd = 0, vStart = 0;
            for (int i = v.Length - 1; i >= 300; --i)
                if (v[i] == 0)
                    vEnd = i;

            factory = new ProjectionFactory(src, th);

            v = factory.getVerticalProject();
            for (int i = v.Length - 1; i >= 300; --i)
                if (v[i] == 0)
                    vEnd = i;

            IImageProcess pre_action = new BitOf8PlaneSlicing(8);
            pre_action.setResouceImage(src);
            Bitmap pre_res = pre_action.Process();


            factory = new ProjectionFactory(pre_res, 10);
            v = factory.getVerticalProject();

            int tmax = 0;
            for (int i = 0; i < v.Length / 2; ++i)
                if (v[i] >= tmax)
                {
                    tmax = v[i];
                    vStart = i;
                }
            #endregion

            #region mean-shift

            action = new Transfor(ImagePretreatment.ThresholdingIterativeWithR(res));
            action.setResouceImage(res);
            res = action.Process();

            Point[] initCenters = { new Point(src.Width / 4, vStart + (vEnd - vStart) / 2), new Point((src.Width / 4) * 3, vStart + (vEnd - vStart) / 2) };
            List<Point> targetCenters = new List<Point>();

            foreach (Point center in initCenters)
            {
                MachineLearing_MeanShift meanShit = new MachineLearing_MeanShift(50, cp[1, 2]);
                meanShit.Center = center;
                meanShit.setResouceImage(res);
                meanShit.Process();
                targetCenters.Add(meanShit.Center);
            }

            action = new ImageHighLight(targetCenters.ToArray());
            //action = new ImageHighLight(initCenters);
            action.setResouceImage(res);
            Bitmap tt = action.Process();

            pictureBox3.Image = tt;
            label3.Text = "Mean-shit Hight liht";

            #endregion
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
                MessageBox.Show("無法開啟影像");
                return;
            }

            Bitmap imageSource = new Bitmap(((PictureBox)sender).Image);
            Form form = new FormShowImage(imageSource);
            form.Show();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            btnCalc.Enabled = false;

            finalProcess(_imageSource);

            btnCalc.Enabled = true;
            MessageBox.Show("完成");
        }

        private void finalProcess(Bitmap src)
        {
            IImageProcess bitPlane = new BitOf8PlaneSlicing(8);
            bitPlane.setResouceImage(src);
            Bitmap bitPlansRes = bitPlane.Process();
            label1.Text = "meanFilter && 8-bit";
            pictureBox1.Image = bitPlansRes;


            int vStart = getBandPassVerticalStart(bitPlansRes);
            if (vStart > src.Height / 2)
                vStart = 0;
            int vEnd = getBandPassVerticalEnd(src);

            Bitmap bandpassRes = src;
            foreach (IImageProcess bandPassByColIndex in new IImageProcess[] { new BandRejectByColIndex(0, vStart), new BandRejectByColIndex(vEnd, src.Height - 1) })
            {
                bandPassByColIndex.setResouceImage(bandpassRes);
                bandpassRes = bandPassByColIndex.Process();
            }
            pictureBox2.Image = bandpassRes;
            label2.Text = "Band Pass";

            IImageProcess medianFilter = new GaussianFilter();
            medianFilter.setResouceImage(bandpassRes);
            Bitmap MedianRes = medianFilter.Process();

            MachineLearing_KMeans kmean = new MachineLearing_KMeans(3, 10);
            kmean.setResouceImage(new Bitmap(MedianRes));
            Bitmap kmean_Result = kmean.Process();
            label3.Text = "k-Means";
            pictureBox3.Image = kmean_Result;


            Point[] initCenters = { new Point(src.Width / 2, src.Height / 2) };
            List<Point> targetCenters = new List<Point>();

            foreach (Point center in initCenters)
            {
                MachineLearing_MeanShift meanShit = new MachineLearing_MeanShift(100, kmean.CenterPoints[1, 2]);
                meanShit.Center = center;
                meanShit.setResouceImage(kmean_Result);
                meanShit.Process();
                targetCenters.Add(meanShit.Center);
            }

            ImageHighLight imgHighLiht = new ImageHighLight(targetCenters.ToArray());
            imgHighLiht.setResouceImage(kmean_Result);
            Bitmap meanShiftVisz = imgHighLiht.Process();
            pictureBox4.Image = meanShiftVisz;
            label4.Text = "Mean-shift";


            RegionGrowpIn growpIn = new RegionGrowpIn(targetCenters, kmean.CenterPoints[1, 2]);
            growpIn.setResouceImage(kmean_Result);
            Bitmap grownRes = growpIn.Process();

            Binarization binarization = new Binarization(254);
            binarization.setResouceImage(grownRes);
            Bitmap finalFrame = binarization.Process();
            pictureBox5.Image = finalFrame;
            label5.Text = "RegionGrowpIn";


            IImageProcess morphology = new MorphologyOpening();
            for (int i = 0; i < 10; i++)
            {
                morphology.setResouceImage(finalFrame);
                finalFrame = morphology.Process();
            }

            IImageProcess regionFill = new RegionFill();
            regionFill.setResouceImage(finalFrame);
            Bitmap finalFrameWithRegionFill = regionFill.Process();
            pictureBox6.Image = finalFrameWithRegionFill;
            label6.Text = "Region Fill";

            MakeImageFrame makeImageFrame = new MakeImageFrame(src, finalFrameWithRegionFill);
            pictureBox7.Image = makeImageFrame.Process();
            label7.Text = "Ans";
        }

        private int getBandPassVerticalStart(Bitmap src)
        {
            ProjectionFactory factory = new ProjectionFactory(src);
            int[] v = factory.getVerticalProject();

            int countMax = 0;
            int resIndex = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] >= countMax)
                {
                    countMax = v[i];
                    resIndex = i;
                }
            }
            return resIndex;
        }

        private int getBandPassVerticalEnd(Bitmap src)
        {
            int res = src.Height / 2;
            int vMin = src.Width;
            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            IImageProcess tranfor = new Transfor(th);
            tranfor.setResouceImage(src);
            Bitmap tranforImg = tranfor.Process();

            ProjectionFactory factory = new ProjectionFactory(src);
            int[] v = factory.getVerticalProject();

            for (int i = v.Length - 1; i >= (src.Height / 2); i--)
            {
                if (v[i] <= vMin)
                {
                    vMin = v[i];
                    res = i;
                }
            }
            return res;
        }
    }
}

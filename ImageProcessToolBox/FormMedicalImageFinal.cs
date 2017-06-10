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



        private void test6_ok(Bitmap src)
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

            //test7(_imageSource);

            final(_imageSource);
            btnCalc.Enabled = true;
            MessageBox.Show("完成");
        }

        private void final(Bitmap src)
        {
            BitOf8PlaneSlicing bitPlane = new BitOf8PlaneSlicing(8);
            bitPlane.setResouceImage(src);
            Bitmap bitPlansRes = bitPlane.Process();
            label1.Text = "meanFilter && 8-bit";
            pictureBox1.Image = bitPlansRes;

            int vStart = getBandPassVerticalStart(bitPlansRes);
            BandRejectByColIndex bandPassByColIndex = new BandRejectByColIndex(0,vStart);
            bandPassByColIndex.setResouceImage(src);
            Bitmap bandpassRes = bandPassByColIndex.Process();
            pictureBox2.Image = bandpassRes;
            label2.Text = "Band Pass";

            MedianFilter medianFilter = new MedianFilter(5, 15);
            medianFilter.setResouceImage(bandpassRes);
            Bitmap MedianRes = medianFilter.Process();
            pictureBox3.Image = MedianRes;
            label3.Text = "MedianFilter";


            MachineLearing_KMeans kmean = new MachineLearing_KMeans(3, 10);
            kmean.setResouceImage(new Bitmap(MedianRes));
            Bitmap kmean_Result = kmean.Process();
            label4.Text = "k-Means";
            pictureBox4.Image = kmean_Result;

            

            //label4.Text = "(Dilation + Erosion)*5";
            //pictureBox4.Image = closeRes;

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
            pictureBox5.Image = meanShiftVisz;
            label5.Text = "Mean-shift";


            RegionGrowpIn growpIn = new RegionGrowpIn(targetCenters);
            growpIn.setResouceImage(kmean_Result);
            Bitmap grownRes = growpIn.Process();

            Binarization binarization = new Binarization(254);
            binarization.setResouceImage(grownRes);
            Bitmap finalFrame = binarization.Process();
            pictureBox6.Image = finalFrame;
            label6.Text = "RegionGrowpIn";


            MakeImageFrame makeImageFrame = new MakeImageFrame(src, finalFrame);
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

        private void test7(Bitmap src)
        {
            //step 1
            IImageProcess action;
            Bitmap res;

            #region BandPassByCol

            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            ProjectionFactory factory = new ProjectionFactory(src, th);
            int[] v = factory.getVerticalProject();
            int vEnd = 0, vStart = 0;
            for (int i = v.Length - 1; i >= 300; --i)
                if (v[i] == 0)
                    vEnd = i;

            action = new BitOf8PlaneSlicing(8);
            action.setResouceImage(src);
            res = action.Process();


            factory = new ProjectionFactory(res, 10);
            v = factory.getVerticalProject();

            int tmax = 0;
            for (int i = 0; i < v.Length / 2; ++i)
                if (v[i] >= tmax)
                {
                    tmax = v[i];
                    vStart = i;
                }

            action = new BandPassByColIndex(vStart, vEnd);
            action.setResouceImage(src);
            res = action.Process();

            #endregion

            #region k - means + bandPass
            action = new MachineLearing_KMeans(3, 10);
            action.setResouceImage(new Bitmap(res));
            res = action.Process();

            byte[,] cp = ((MachineLearing_KMeans)action).CenterPoints;
            action = new Bandpass(cp[1, 2], 0);
            action.setResouceImage(res);
            res = action.Process();


            #endregion

            #region Horizontal Projection
            th = ImagePretreatment.ThresholdingIterativeWithR(src);
            factory = new ProjectionFactory(res, th);
            int[] h = factory.getHorizontalProject();

            int hStart = 0, hEnd = 0;
            for (int i = 50; i >= 0; i--)
                if (h[i] == 0)
                {
                    hStart = i;
                    break;
                }
            if (h.Length > 300)
            {
                for (int i = 600; i < h.Length; i++)
                    if (h[i] == 0)
                    {
                        hEnd = i;
                        break;
                    }
            }
            else
                hEnd = h.Length - 1;

            action = new BandPassByRowIndex(hStart, hEnd);
            action.setResouceImage(res);
            res = action.Process();
            label1.Text = "vertical projection \n 8Bit of PlaneSlicing \n K-means + band-pass \n Horizontal Projection";
            pictureBox1.Image = res;


            #endregion

            #region rectangleof Interested
            res = rectangleofInterested(src, res);
            pictureBox2.Image = res;
            label2.Text = "rectangleof Interested";
            #endregion

            #region filter

            action = new MedianFilter(25, 25);
            //action = new Mosaic(5, res);
            action.setResouceImage(res);
            res = action.Process();

            pictureBox3.Image = res;
            label3.Text = "Filters";


            #endregion


            #region 3-means
            action = new MachineLearing_KMeans(3, 10);
            action.setResouceImage(new Bitmap(res));
            res = action.Process();

            cp = ((MachineLearing_KMeans)action).CenterPoints;
            action = new Bandpass(cp[1, 2], 0);
            action.setResouceImage(res);
            res = action.Process();

            MakeImageFrame ans = new MakeImageFrame(src, res);
            pictureBox6.Image = ans.Process();
            label6.Text = "3-means";

            #endregion

            IImageProcess tAction = new DilationWithValue(cp[1, 2], 21, 21);
            tAction.setResouceImage(new Bitmap(pictureBox2.Image));


            pictureBox5.Image = new MakeImageFrame(src, tAction.Process()).Process();
            label5.Text = "Test Dilation ";


            #region high light
            //action = new Transfor(ImagePretreatment.ThresholdingIterativeWithR(res));
            //action.setResouceImage(res);
            //res = action.Process();

            Point[] initCenters = { new Point((hEnd - hStart) / 4, vStart + (vEnd - vStart) / 2), new Point(((hEnd - hStart) / 4) * 3, vStart + (vEnd - vStart) / 2) };
            List<Point> targetCenters = new List<Point>();

            foreach (Point center in initCenters)
            {
                MachineLearing_MeanShift meanShit = new MachineLearing_MeanShift(150, cp[1, 2]);
                meanShit.Center = center;
                meanShit.setResouceImage(res);
                meanShit.Process();
                targetCenters.Add(meanShit.Center);
            }

            action = new ImageHighLight(targetCenters.ToArray());
            //action = new ImageHighLight(initCenters);
            action.setResouceImage(res);
            Bitmap tt = action.Process();

            pictureBox4.Image = tt;
            label4.Text = "Mean-shit Hight";
            #endregion
            RegionGrowpIn growpIn = new RegionGrowpIn(targetCenters);
            MachineLearing_KMeans MachineLearing_KMeans = new MachineLearing_KMeans(3, 10);
            MachineLearing_KMeans.setResouceImage(new Bitmap(pictureBox3.Image));

            growpIn.setResouceImage(MachineLearing_KMeans.Process());
            Bitmap grownRes = growpIn.Process();

            Binarization binarization = new Binarization(254);
            binarization.setResouceImage(grownRes);
            Bitmap finalFrame = binarization.Process();

            MakeImageFrame makeImageFrame = new MakeImageFrame(src, finalFrame);
            pictureBox7.Image = makeImageFrame.Process();
            label6.Text = "K-mean + Mean-shift";
            #region growp in

            #endregion
        }

    }
}

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

        private void onStart()
        {
            btnCalc.Enabled = false;
            btnMedianFilter.Enabled = false;
        }

        private void onEnd()
        {
            btnCalc.Enabled = true;
            btnMedianFilter.Enabled = true;
            MessageBox.Show("完成");
        }


        private void btnCalc_Click(object sender, EventArgs e)
        {
            onStart();
            finalProcess(_imageSource);
            onEnd();
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

        private void test7(Bitmap src)
        {
            //step 1
            #region BandPassByCol

            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            ProjectionFactory factory = new ProjectionFactory(src, th);
            int[] v = factory.getVerticalProject();
            int vEnd = 0, vStart = 0;
            for (int i = v.Length - 1; i >= 300; --i)
                if (v[i] == 0)
                    vEnd = i;

            IImageProcess bitOf8PlaneSlicing = new BitOf8PlaneSlicing(8);
            bitOf8PlaneSlicing.setResouceImage(src);
            Bitmap res_bitOf8PlaneSlicing = bitOf8PlaneSlicing.Process();


            factory = new ProjectionFactory(res_bitOf8PlaneSlicing, 10);
            v = factory.getVerticalProject();

            int tmax = 0;
            for (int i = 0; i < v.Length / 2; ++i)
                if (v[i] >= tmax)
                {
                    tmax = v[i];
                    vStart = i;
                }

            IImageProcess bandPassByColIndex = new BandPassByColIndex(vStart, vEnd);
            bandPassByColIndex.setResouceImage(src);
            Bitmap res_bandPassByColIndex = bandPassByColIndex.Process();

            #endregion

            #region k - means + bandPass
            MachineLearing_KMeans kmeans_3 = new MachineLearing_KMeans(3, 10);
            kmeans_3.setResouceImage(new Bitmap(res_bandPassByColIndex));
            Bitmap res_kmeans = kmeans_3.Process();

            byte[,] cp = kmeans_3.CenterPoints;
            IImageProcess bandpass = new Bandpass(cp[1, 2], 0);
            bandpass.setResouceImage(res_kmeans);
            Bitmap res_bandpass = bandpass.Process();
            #endregion

            #region Horizontal Projection
            th = ImagePretreatment.ThresholdingIterativeWithR(src);
            factory = new ProjectionFactory(res_bandpass, th);
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

            IImageProcess bandPassByRowIndex = new BandPassByRowIndex(hStart, hEnd);
            bandPassByRowIndex.setResouceImage(res_bandpass);
            Bitmap res_bandPassByRowIndex = bandPassByRowIndex.Process();
            label1.Text = "vertical projection \n 8Bit of PlaneSlicing \n K-means + band-pass \n Horizontal Projection";
            pictureBox1.Image = res_bandPassByRowIndex;


            #endregion

            #region rectangleof Interested
            Bitmap res_roi = rectangleofInterested(src, res_bandPassByRowIndex);
            pictureBox2.Image = res_roi;
            label2.Text = "ROI";
            #endregion

            #region filter

            IImageProcess filterAction = new MedianFilter(25, 25);
            filterAction.setResouceImage(res_roi);
            Bitmap res_filterAction = filterAction.Process();

            pictureBox3.Image = res_filterAction;
            label3.Text = "Filters";


            #endregion


            #region 3-means
            MachineLearing_KMeans kmean_afterFilter = new MachineLearing_KMeans(3, 10);
            kmean_afterFilter.setResouceImage(new Bitmap(res_filterAction));
            Bitmap res_3means_afterFilter = kmean_afterFilter.Process();

            cp = kmean_afterFilter.CenterPoints;
            bandpass = new Bandpass(cp[1, 2], 0);
            bandpass.setResouceImage(res_3means_afterFilter);
            Bitmap res_keams_afterBandpass = bandpass.Process();

            #endregion

            #region high light
            Point[] initCenters = { new Point((hEnd - hStart) / 4, vStart + (vEnd - vStart) / 2), new Point(((hEnd - hStart) / 4) * 3, vStart + (vEnd - vStart) / 2) };
            List<Point> targetCenters = new List<Point>();

            foreach (Point center in initCenters)
            {
                MachineLearing_MeanShift meanShit = new MachineLearing_MeanShift(150, cp[1, 2]);
                meanShit.Center = center;
                meanShit.setResouceImage(res_keams_afterBandpass);
                meanShit.Process();
                targetCenters.Add(meanShit.Center);
            }

            IImageProcess imageHighLight = new ImageHighLight(targetCenters.ToArray());
            imageHighLight.setResouceImage(res_keams_afterBandpass);
            pictureBox4.Image = imageHighLight.Process(); ;
            label4.Text = "Mean-shit Hight";
            #endregion

            #region growpIn
            RegionGrowpIn growpIn = new RegionGrowpIn(targetCenters[0]);

            growpIn.setResouceImage(res_keams_afterBandpass);
            Bitmap grownRes = growpIn.Process();

            Binarization binarization = new Binarization(254);
            binarization.setResouceImage(grownRes);
            Bitmap finalFrame = binarization.Process();

            pictureBox5.Image = finalFrame;
            label5.Text = "Growp In";
            #endregion

            #region region fill
            IImageProcess region_fill = new RegionFill();
            region_fill.setResouceImage(finalFrame);
            Bitmap res_region_fill = region_fill.Process();
            pictureBox6.Image = res_region_fill;
            label6.Text = "Region fill";
            #endregion

            #region Make Frame (Ans)
            MakeImageFrame makeImageFrame = new MakeImageFrame(src, res_region_fill);
            pictureBox7.Image = makeImageFrame.Process();
            label7.Text = "Ans";
            #endregion
        }

        private void test8(Bitmap src)
        {
            //step 1
            #region BandPassByCol

            int th = ImagePretreatment.ThresholdingIterativeWithR(src);
            ProjectionFactory factory = new ProjectionFactory(src, th);
            int[] v = factory.getVerticalProject();
            int vEnd = 0, vStart = 0;
            for (int i = v.Length - 1; i >= 300; --i)
                if (v[i] == 0)
                    vEnd = i;

            IImageProcess bitOf8PlaneSlicing = new BitOf8PlaneSlicing(8);
            bitOf8PlaneSlicing.setResouceImage(src);
            Bitmap res_bitOf8PlaneSlicing = bitOf8PlaneSlicing.Process();


            factory = new ProjectionFactory(res_bitOf8PlaneSlicing, 10);
            v = factory.getVerticalProject();

            int tmax = 0;
            for (int i = 0; i < v.Length / 2; ++i)
                if (v[i] >= tmax)
                {
                    tmax = v[i];
                    vStart = i;
                }

            IImageProcess bandPassByColIndex = new BandPassByColIndex(vStart, vEnd);
            bandPassByColIndex.setResouceImage(src);
            Bitmap res_bandPassByColIndex = bandPassByColIndex.Process();

            #endregion

            #region k - means + bandPass
            MachineLearing_KMeans kmeans_3 = new MachineLearing_KMeans(3, 10);
            kmeans_3.setResouceImage(new Bitmap(res_bandPassByColIndex));
            Bitmap res_kmeans = kmeans_3.Process();

            byte[,] cp = kmeans_3.CenterPoints;
            IImageProcess bandpass = new Bandpass(cp[1, 2], 0);
            bandpass.setResouceImage(res_kmeans);
            Bitmap res_bandpass = bandpass.Process();
            #endregion

            #region Horizontal Projection
            th = ImagePretreatment.ThresholdingIterativeWithR(src);
            factory = new ProjectionFactory(res_bandpass, th);
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

            IImageProcess bandPassByRowIndex = new BandPassByRowIndex(hStart, hEnd);
            bandPassByRowIndex.setResouceImage(res_bandpass);
            Bitmap res_bandPassByRowIndex = bandPassByRowIndex.Process();
            label1.Text = "vertical projection \n 8Bit of PlaneSlicing \n K-means + band-pass \n Horizontal Projection";
            pictureBox1.Image = res_bandPassByRowIndex;


            #endregion

            #region rectangleof Interested
            Bitmap res_roi = rectangleofInterested(src, res_bandPassByRowIndex);
            pictureBox2.Image = res_roi;
            label2.Text = "ROI";
            #endregion

            #region filter

            IImageProcess filterAction = new MedianFilter(25, 25);
            filterAction.setResouceImage(res_roi);
            Bitmap res_filterAction = filterAction.Process();

            pictureBox3.Image = res_filterAction;
            label3.Text = "Filters";


            #endregion


            #region 3-means
            MachineLearing_KMeans kmean_afterFilter = new MachineLearing_KMeans(3, 10);
            kmean_afterFilter.setResouceImage(new Bitmap(res_filterAction));
            Bitmap res_3means_afterFilter = kmean_afterFilter.Process();

            cp = kmean_afterFilter.CenterPoints;
            bandpass = new Bandpass(cp[1, 2], 0);
            bandpass.setResouceImage(res_3means_afterFilter);
            Bitmap res_keams_afterBandpass = bandpass.Process();

            #endregion

            #region high light
            Point[] initCenters = { new Point((hEnd - hStart) / 4, vStart + (vEnd - vStart) / 2), new Point(((hEnd - hStart) / 4) * 3, vStart + (vEnd - vStart) / 2) };
            List<Point> targetCenters = new List<Point>();

            foreach (Point center in initCenters)
            {
                MachineLearing_MeanShift meanShit = new MachineLearing_MeanShift(150, cp[1, 2]);
                meanShit.Center = center;
                meanShit.setResouceImage(res_keams_afterBandpass);
                meanShit.Process();
                targetCenters.Add(meanShit.Center);
            }

            IImageProcess imageHighLight = new ImageHighLight(targetCenters.ToArray());
            imageHighLight.setResouceImage(res_keams_afterBandpass);
            pictureBox4.Image = imageHighLight.Process(); ;
            label4.Text = "Mean-shit Hight";
            #endregion

            #region growpIn
            RegionGrowpIn growpIn = new RegionGrowpIn(targetCenters[0]);

            growpIn.setResouceImage(res_keams_afterBandpass);
            Bitmap grownRes = growpIn.Process();

            Binarization binarization = new Binarization(254);
            binarization.setResouceImage(grownRes);
            Bitmap finalFrame = binarization.Process();

            pictureBox5.Image = finalFrame;
            label5.Text = "Growp In";
            #endregion

            #region region fill
            IImageProcess region_fill = new RegionFillIn();
            region_fill.setResouceImage(finalFrame);

            Bitmap res_region_fill = region_fill.Process();
            
            binarization = new Binarization(128);
            binarization.setResouceImage(res_region_fill);
            res_region_fill = binarization.Process();

            pictureBox6.Image = res_region_fill;
            label6.Text = "Region fill";
            #endregion

            #region Make Frame (Ans)
            MakeImageFrame makeImageFrame = new MakeImageFrame(src, res_region_fill);
            pictureBox7.Image = makeImageFrame.Process();
            label7.Text = "Ans";
            #endregion
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

        private void btnMedianFilter_Click(object sender, EventArgs e)
        {
            onStart();
            test8(_imageSource);
            onEnd();
        }
    }
}

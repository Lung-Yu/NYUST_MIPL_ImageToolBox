﻿using ImageProcessToolBox.Analysis;
using ImageProcessToolBox.PoingProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessToolBox
{
    public partial class FormAnalysis : Form
    {

        private static readonly int WIDTH_PROJECTION_PANEL = 256;
        private static readonly int HEIGHT_PROJECTION_PANEL = 100;
        public FormAnalysis(Bitmap source)
        {
            InitializeComponent();
            try
            {
                pictureBox1.Image = source;
                Analysis();
            }
            catch (Exception e)
            {
                MessageBox.Show("目前無法分析處理此影像。\n" + e.Message);
            }
        }



        private void Analysis()
        {
            AnalysisSeparation separtion = new AnalysisSeparation();
            separtion.ImageSouce = new Bitmap(pictureBox1.Image);
            separtion.Process();

            grayProcess(separtion);
            RedProcess(separtion);
            GeenProcess(separtion);
            BlueProcess(separtion);
        }

        private void grayProcess(AnalysisSeparation separtion)
        {
            Bitmap img = separtion.ImageGray;
            pictureBox5.Image = img;
            projectionTest(verticalGrayChart, horizontalGrayChart, img);
        }

        private void projectionTest(Chart VerticalChart, Chart HorizontalChart, Bitmap img)
        {
            HorizontalChart.Series["Series1"].IsVisibleInLegend = false;
            HorizontalChart.Series["Series1"].IsValueShownAsLabel = false;
            VerticalChart.Series["Series1"].IsVisibleInLegend = false;
            VerticalChart.Series["Series1"].IsValueShownAsLabel = false;


            ProjectionFactory factory = new ProjectionFactory(img);
            factory.Threshold = (int)numericUpDown1.Value;
            int[] horizontalProjection = factory.getHorizontalProject();
            int[] verticalProjection = factory.getVerticalProject();
            for (int i = 0; i < horizontalProjection.Length;i++ )
                HorizontalChart.Series["Series1"].Points.AddXY(i, horizontalProjection[i]);
            for (int i = 0; i < verticalProjection.Length; i++)
                VerticalChart.Series["Series1"].Points.AddXY(i, verticalProjection[i]);
        }

        private void RedProcess(AnalysisSeparation separtion)
        {
            pictureBox2.Image = separtion.ImageOfR;
            projectionTest(chart1, chart2, separtion.ImageOfR);
        }

        private void GeenProcess(AnalysisSeparation separtion)
        {
            pictureBox3.Image = separtion.ImageOfG;
            projectionTest(chart3, chart4, separtion.ImageOfG);
        }

        private void BlueProcess(AnalysisSeparation separtion)
        {
            pictureBox4.Image = separtion.ImageOfB;
            projectionTest(chart5, chart6, separtion.ImageOfB);
        }

        private List<int> getResouce(Bitmap bitmap, out int oMax)
        {
            List<int> values = new List<int>();
            int value = 0;
            oMax = 0;

            for (int y = 0; y < bitmap.Height; y++)
            {
                value = 0;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    value += bitmap.GetPixel(x, y).R;
                }
                if (value > oMax)
                    oMax = value;

                values.Add(value);
            }

            return values;
        }

        private void projection(Bitmap img, Graphics horizontal, Graphics vertical)
        {

            ProjectionFactory factory = new ProjectionFactory(img);
            factory.Threshold = (int)numericUpDown1.Value;

            drawHorizontal(horizontal, factory.getHorizontalProject());
            drawVertical(vertical, factory.getVerticalProject());
        }

        private void drawHorizontal(Graphics graphics, int[] projection)
        {

            Pen pen = new Pen(Color.Black, 1);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;  //EndCap設定 這支筆的結尾會是個箭頭 ArrowAnchor

            int remainder = (projection.Length / WIDTH_PROJECTION_PANEL) + 1;
            int valTemp = 0;

            graphics.Clear(Color.White);
            for (int x = 0; x < projection.Length; x++)
            {
                if ((x % remainder) == 0)
                {
                    if ((x - remainder) > 0)
                        for (int ix = x - remainder; ix < x; ix++)
                            graphics.DrawLine(pen, ix, HEIGHT_PROJECTION_PANEL, ix, valTemp / remainder);
                    else
                        graphics.DrawLine(pen, x, HEIGHT_PROJECTION_PANEL, x, valTemp / remainder);

                    valTemp = 0;
                }
                else
                    valTemp += projection[x];
            }



        }


        private void drawVertical(Graphics graphics, int[] projection)
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;  //EndCap設定 這支筆的結尾會是個箭頭 ArrowAnchor

            graphics.Clear(Color.White);
            int remainder = (projection.Length / WIDTH_PROJECTION_PANEL) + 1;
            int valTemp = 0;
            for (int y = 0; y < projection.Length; y++)
            {
                if ((y % remainder) == 0)
                {
                    if ((y - remainder) > 0)
                        for (int iy = y - remainder; iy < y; iy++)
                            graphics.DrawLine(pen, HEIGHT_PROJECTION_PANEL, iy, valTemp / remainder, iy);
                    else
                        graphics.DrawLine(pen, valTemp / remainder, y, HEIGHT_PROJECTION_PANEL, y);

                    valTemp = 0;
                }
                else
                    valTemp += projection[y];
            }

        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            Negative negative = new Negative();
            negative.setImage(new Bitmap(pictureBox1.Image));
            negative.process();
            pictureBox1.Image = negative.getImage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form action = new FormShowImage(new Bitmap(pictureBox1.Image));
            action.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Analysis();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }






    }
}

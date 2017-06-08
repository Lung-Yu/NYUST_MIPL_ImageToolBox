using ImageProcessToolBox.Analysis;
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
            catch (Exception)
            {
                MessageBox.Show("目前無法分析處理此影像");
            }
        }



        private void button1_Click(object sender, EventArgs e)
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
            projection(img, GrayHorizontalProjection.CreateGraphics(), GrayVerticalProjection.CreateGraphics());
        }



        private void RedProcess(AnalysisSeparation separtion)
        {
            pictureBox2.Image = separtion.ImageOfR;
            projection(separtion.ImageOfR, RedHorizontalProjection.CreateGraphics(), RedVerticalProjection.CreateGraphics());
        }

        private void GeenProcess(AnalysisSeparation separtion)
        {
            pictureBox3.Image = separtion.ImageOfG;
            projection(separtion.ImageOfG, GreenHorizontalProjection.CreateGraphics(), GreenVerticalProjection.CreateGraphics());
        }

        private void BlueProcess(AnalysisSeparation separtion)
        {
            pictureBox4.Image = separtion.ImageOfB;
            projection(separtion.ImageOfG, BlueHorizontalProjection.CreateGraphics(), BlueVerticalProjection.CreateGraphics());
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
            pictureBox1.Image = new Negative(new Bitmap(pictureBox1.Image)).Process();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form action = new FormShowImage(new Bitmap(pictureBox1.Image));
            action.Show();
        }






    }
}

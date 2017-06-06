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
    public partial class AnalysisForm : Form
    {

        public AnalysisForm(Bitmap source)
        {
            InitializeComponent();

            pictureBox1.Image = source;

            Analysis();
            
        }

        private void Analysis()
        {
            AnalysisSeparation separtion = new AnalysisSeparation();
            separtion.ImageSouce = new Bitmap(pictureBox1.Image);
            separtion.Process();

            pictureBox2.Image = separtion.ImageOfR;
            pictureBox3.Image = separtion.ImageOfG;
            pictureBox4.Image = separtion.ImageOfB;
            pictureBox5.Image = separtion.ImageGray;
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
    }
}

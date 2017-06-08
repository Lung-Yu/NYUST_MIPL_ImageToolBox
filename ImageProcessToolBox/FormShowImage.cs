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
    public partial class FormShowImage : Form
    {
        
        public FormShowImage(Bitmap img)
        {
            InitializeComponent();

            pictureBox1.Image = img;
        }
    }
}

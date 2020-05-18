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
    public partial class FormShowImage : Form
    {

        public FormShowImage(Bitmap img)
        {
            InitializeComponent();

            pictureBox1.Image = img;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = new Bitmap(pictureBox1.Image);

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

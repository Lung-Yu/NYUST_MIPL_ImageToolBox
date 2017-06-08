namespace ImageProcessToolBox
{
    partial class FormAnalysis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GrayHorizontalProjection = new System.Windows.Forms.Panel();
            this.GrayVerticalProjection = new System.Windows.Forms.Panel();
            this.RedHorizontalProjection = new System.Windows.Forms.Panel();
            this.RedVerticalProjection = new System.Windows.Forms.Panel();
            this.GreenHorizontalProjection = new System.Windows.Forms.Panel();
            this.GreenVerticalProjection = new System.Windows.Forms.Panel();
            this.BlueHorizontalProjection = new System.Windows.Forms.Panel();
            this.BlueVerticalProjection = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNegative = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelThreshold = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(6, 468);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(419, 468);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(256, 256);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(807, 468);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(256, 256);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(419, 74);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(256, 256);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label2.Location = new System.Drawing.Point(415, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Gray";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label1.Location = new System.Drawing.Point(12, 446);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Red";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label3.Location = new System.Drawing.Point(415, 446);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Green";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label4.Location = new System.Drawing.Point(803, 446);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Blue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label5.Location = new System.Drawing.Point(8, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Original";
            // 
            // GrayHorizontalProjection
            // 
            this.GrayHorizontalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GrayHorizontalProjection.Location = new System.Drawing.Point(419, 330);
            this.GrayHorizontalProjection.Name = "GrayHorizontalProjection";
            this.GrayHorizontalProjection.Size = new System.Drawing.Size(256, 110);
            this.GrayHorizontalProjection.TabIndex = 12;
            // 
            // GrayVerticalProjection
            // 
            this.GrayVerticalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GrayVerticalProjection.Location = new System.Drawing.Point(676, 74);
            this.GrayVerticalProjection.Name = "GrayVerticalProjection";
            this.GrayVerticalProjection.Size = new System.Drawing.Size(100, 256);
            this.GrayVerticalProjection.TabIndex = 13;
            // 
            // RedHorizontalProjection
            // 
            this.RedHorizontalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RedHorizontalProjection.Location = new System.Drawing.Point(6, 724);
            this.RedHorizontalProjection.Name = "RedHorizontalProjection";
            this.RedHorizontalProjection.Size = new System.Drawing.Size(256, 110);
            this.RedHorizontalProjection.TabIndex = 13;
            // 
            // RedVerticalProjection
            // 
            this.RedVerticalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RedVerticalProjection.Location = new System.Drawing.Point(262, 468);
            this.RedVerticalProjection.Name = "RedVerticalProjection";
            this.RedVerticalProjection.Size = new System.Drawing.Size(100, 256);
            this.RedVerticalProjection.TabIndex = 14;
            // 
            // GreenHorizontalProjection
            // 
            this.GreenHorizontalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GreenHorizontalProjection.Location = new System.Drawing.Point(419, 724);
            this.GreenHorizontalProjection.Name = "GreenHorizontalProjection";
            this.GreenHorizontalProjection.Size = new System.Drawing.Size(256, 110);
            this.GreenHorizontalProjection.TabIndex = 14;
            // 
            // GreenVerticalProjection
            // 
            this.GreenVerticalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GreenVerticalProjection.Location = new System.Drawing.Point(675, 468);
            this.GreenVerticalProjection.Name = "GreenVerticalProjection";
            this.GreenVerticalProjection.Size = new System.Drawing.Size(100, 256);
            this.GreenVerticalProjection.TabIndex = 15;
            // 
            // BlueHorizontalProjection
            // 
            this.BlueHorizontalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BlueHorizontalProjection.Location = new System.Drawing.Point(807, 724);
            this.BlueHorizontalProjection.Name = "BlueHorizontalProjection";
            this.BlueHorizontalProjection.Size = new System.Drawing.Size(256, 110);
            this.BlueHorizontalProjection.TabIndex = 14;
            // 
            // BlueVerticalProjection
            // 
            this.BlueVerticalProjection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BlueVerticalProjection.Location = new System.Drawing.Point(1063, 468);
            this.BlueVerticalProjection.Name = "BlueVerticalProjection";
            this.BlueVerticalProjection.Size = new System.Drawing.Size(100, 256);
            this.BlueVerticalProjection.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnNegative);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.labelThreshold);
            this.panel1.Location = new System.Drawing.Point(807, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 350);
            this.panel1.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.button1.Location = new System.Drawing.Point(265, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 33);
            this.button1.TabIndex = 25;
            this.button1.Text = "投影";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNegative
            // 
            this.btnNegative.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.btnNegative.Location = new System.Drawing.Point(265, 27);
            this.btnNegative.Name = "btnNegative";
            this.btnNegative.Size = new System.Drawing.Size(91, 33);
            this.btnNegative.TabIndex = 24;
            this.btnNegative.Text = "Negative";
            this.btnNegative.UseVisualStyleBackColor = true;
            this.btnNegative.Click += new System.EventHandler(this.btnNegative_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.numericUpDown1.Location = new System.Drawing.Point(110, 75);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(149, 33);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            // 
            // labelThreshold
            // 
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Font = new System.Drawing.Font("PMingLiU", 18F);
            this.labelThreshold.Location = new System.Drawing.Point(3, 80);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(101, 24);
            this.labelThreshold.TabIndex = 21;
            this.labelThreshold.Text = "Threshold";
            // 
            // FormAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 849);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BlueVerticalProjection);
            this.Controls.Add(this.BlueHorizontalProjection);
            this.Controls.Add(this.GreenVerticalProjection);
            this.Controls.Add(this.GreenHorizontalProjection);
            this.Controls.Add(this.RedVerticalProjection);
            this.Controls.Add(this.RedHorizontalProjection);
            this.Controls.Add(this.GrayVerticalProjection);
            this.Controls.Add(this.GrayHorizontalProjection);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormAnalysis";
            this.Text = "Analysis";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel GrayHorizontalProjection;
        private System.Windows.Forms.Panel GrayVerticalProjection;
        private System.Windows.Forms.Panel RedHorizontalProjection;
        private System.Windows.Forms.Panel RedVerticalProjection;
        private System.Windows.Forms.Panel GreenHorizontalProjection;
        private System.Windows.Forms.Panel GreenVerticalProjection;
        private System.Windows.Forms.Panel BlueHorizontalProjection;
        private System.Windows.Forms.Panel BlueVerticalProjection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnNegative;
        private System.Windows.Forms.Button button1;
    }
}
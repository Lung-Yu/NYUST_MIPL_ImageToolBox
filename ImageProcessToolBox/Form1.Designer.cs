namespace ImageProcessToolBox
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label labelMean;
            System.Windows.Forms.Label labelStandardDeviation;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnColorFunction = new System.Windows.Forms.Button();
            this.btnExp = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnColouring = new System.Windows.Forms.Button();
            this.btnBinarization = new System.Windows.Forms.Button();
            this.btnPowerLaw = new System.Windows.Forms.Button();
            this.btnHistogramEqualization = new System.Windows.Forms.Button();
            this.txtBinarization = new System.Windows.Forms.TextBox();
            this.labelOtus = new System.Windows.Forms.Label();
            this.btnOtus = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btn8BitPlaneSlicing = new System.Windows.Forms.Button();
            this.numeric8BitPlaneSlicing = new System.Windows.Forms.NumericUpDown();
            this.btnLog = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNegative = new System.Windows.Forms.Button();
            this.btnGrayscale = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnK_Means = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIterationLevel = new System.Windows.Forms.TextBox();
            this.txtKValue = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.btnIDFT = new System.Windows.Forms.Button();
            this.btnDFT = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnDWT = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnSwell = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnErosion = new System.Windows.Forms.Button();
            this.btnOpening = new System.Windows.Forms.Button();
            this.btnClosing = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnPrewittFilter = new System.Windows.Forms.Button();
            this.btnVertical = new System.Windows.Forms.Button();
            this.btnN45Degree = new System.Windows.Forms.Button();
            this.btn45Degree = new System.Windows.Forms.Button();
            this.btnHorizontal = new System.Windows.Forms.Button();
            this.btnMaxFilter = new System.Windows.Forms.Button();
            this.btnMinFilter = new System.Windows.Forms.Button();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMeanFilter = new System.Windows.Forms.Button();
            this.btnAvgFiliter = new System.Windows.Forms.Button();
            this.btnHighPassFilters = new System.Windows.Forms.Button();
            this.btnLowPassFilters = new System.Windows.Forms.Button();
            this.btnSobel = new System.Windows.Forms.Button();
            this.btnGaussianFilter = new System.Windows.Forms.Button();
            this.btnLaplacian = new System.Windows.Forms.Button();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRelief = new System.Windows.Forms.Button();
            this.btnMosaic = new System.Windows.Forms.Button();
            this.btnOldStyle = new System.Windows.Forms.Button();
            this.btnRippleEffect = new System.Windows.Forms.Button();
            this.btnSwirl = new System.Windows.Forms.Button();
            this.btnSpherize = new System.Windows.Forms.Button();
            this.btnSketch = new System.Windows.Forms.Button();
            this.btnInstagram1977 = new System.Windows.Forms.Button();
            this.btnLomo = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel26 = new System.Windows.Forms.Panel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnGaussianNoise = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.btnLBP = new System.Windows.Forms.Button();
            this.btnTry = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelconsle = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelImageSize = new System.Windows.Forms.Label();
            this.resultImageSize = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.ResultGraphicsB = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.ResultGraphicsG = new System.Windows.Forms.Panel();
            this.ResultGraphicsR = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.sourceGraphicsB = new System.Windows.Forms.Panel();
            this.sourceGraphicsG = new System.Windows.Forms.Panel();
            this.sourceGraphicsR = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnDrawSource = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnRestore = new System.Windows.Forms.Button();
            labelMean = new System.Windows.Forms.Label();
            labelStandardDeviation = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric8BitPlaneSlicing)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            this.panel21.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel24.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMean
            // 
            labelMean.AutoSize = true;
            labelMean.Font = new System.Drawing.Font("PMingLiU", 12F);
            labelMean.Location = new System.Drawing.Point(13, 46);
            labelMean.Name = "labelMean";
            labelMean.Size = new System.Drawing.Size(43, 16);
            labelMean.TabIndex = 52;
            labelMean.Text = "Mean";
            // 
            // labelStandardDeviation
            // 
            labelStandardDeviation.AutoSize = true;
            labelStandardDeviation.Font = new System.Drawing.Font("PMingLiU", 12F);
            labelStandardDeviation.Location = new System.Drawing.Point(110, 48);
            labelStandardDeviation.Name = "labelStandardDeviation";
            labelStandardDeviation.Size = new System.Drawing.Size(18, 16);
            labelStandardDeviation.TabIndex = 54;
            labelStandardDeviation.Text = "st";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.btnColorFunction);
            this.panel3.Controls.Add(this.btnExp);
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.btnColouring);
            this.panel3.Controls.Add(this.btnBinarization);
            this.panel3.Controls.Add(this.btnPowerLaw);
            this.panel3.Controls.Add(this.btnHistogramEqualization);
            this.panel3.Controls.Add(this.txtBinarization);
            this.panel3.Controls.Add(this.labelOtus);
            this.panel3.Controls.Add(this.btnOtus);
            this.panel3.Controls.Add(this.txtLog);
            this.panel3.Controls.Add(this.btn8BitPlaneSlicing);
            this.panel3.Controls.Add(this.numeric8BitPlaneSlicing);
            this.panel3.Controls.Add(this.btnLog);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.btnNegative);
            this.panel3.Controls.Add(this.btnGrayscale);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 277);
            this.panel3.TabIndex = 2;
            // 
            // btnColorFunction
            // 
            this.btnColorFunction.Location = new System.Drawing.Point(12, 244);
            this.btnColorFunction.Name = "btnColorFunction";
            this.btnColorFunction.Size = new System.Drawing.Size(94, 23);
            this.btnColorFunction.TabIndex = 4;
            this.btnColorFunction.Text = "Color Function";
            this.btnColorFunction.UseVisualStyleBackColor = true;
            this.btnColorFunction.Click += new System.EventHandler(this.btnColorFunction_Click);
            // 
            // btnExp
            // 
            this.btnExp.Location = new System.Drawing.Point(118, 161);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(51, 23);
            this.btnExp.TabIndex = 15;
            this.btnExp.Text = "EXP Transform";
            this.btnExp.UseVisualStyleBackColor = true;
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(116, 215);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(139, 23);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // btnColouring
            // 
            this.btnColouring.Location = new System.Drawing.Point(13, 215);
            this.btnColouring.Name = "btnColouring";
            this.btnColouring.Size = new System.Drawing.Size(94, 23);
            this.btnColouring.TabIndex = 3;
            this.btnColouring.Text = "炫彩";
            this.btnColouring.UseVisualStyleBackColor = true;
            this.btnColouring.Click += new System.EventHandler(this.btnColouring_Click);
            // 
            // btnBinarization
            // 
            this.btnBinarization.Location = new System.Drawing.Point(61, 130);
            this.btnBinarization.Name = "btnBinarization";
            this.btnBinarization.Size = new System.Drawing.Size(136, 23);
            this.btnBinarization.TabIndex = 14;
            this.btnBinarization.Text = "Binary Thresholding";
            this.btnBinarization.UseVisualStyleBackColor = true;
            this.btnBinarization.Click += new System.EventHandler(this.btnBinarization_Click);
            // 
            // btnPowerLaw
            // 
            this.btnPowerLaw.Location = new System.Drawing.Point(180, 161);
            this.btnPowerLaw.Name = "btnPowerLaw";
            this.btnPowerLaw.Size = new System.Drawing.Size(75, 23);
            this.btnPowerLaw.TabIndex = 17;
            this.btnPowerLaw.Text = "Power Law";
            this.btnPowerLaw.UseVisualStyleBackColor = true;
            this.btnPowerLaw.Click += new System.EventHandler(this.btnPowerLaw_Click);
            // 
            // btnHistogramEqualization
            // 
            this.btnHistogramEqualization.Location = new System.Drawing.Point(13, 190);
            this.btnHistogramEqualization.Name = "btnHistogramEqualization";
            this.btnHistogramEqualization.Size = new System.Drawing.Size(242, 23);
            this.btnHistogramEqualization.TabIndex = 11;
            this.btnHistogramEqualization.Text = "Histogram Equalization";
            this.btnHistogramEqualization.UseVisualStyleBackColor = true;
            this.btnHistogramEqualization.Click += new System.EventHandler(this.btnHistogramEqualization_Click);
            // 
            // txtBinarization
            // 
            this.txtBinarization.Location = new System.Drawing.Point(12, 132);
            this.txtBinarization.Name = "txtBinarization";
            this.txtBinarization.Size = new System.Drawing.Size(43, 22);
            this.txtBinarization.TabIndex = 13;
            this.txtBinarization.Text = "127";
            // 
            // labelOtus
            // 
            this.labelOtus.AutoSize = true;
            this.labelOtus.Location = new System.Drawing.Point(178, 77);
            this.labelOtus.Name = "labelOtus";
            this.labelOtus.Size = new System.Drawing.Size(52, 12);
            this.labelOtus.TabIndex = 12;
            this.labelOtus.Text = "__none__";
            // 
            // btnOtus
            // 
            this.btnOtus.Location = new System.Drawing.Point(12, 72);
            this.btnOtus.Name = "btnOtus";
            this.btnOtus.Size = new System.Drawing.Size(157, 23);
            this.btnOtus.TabIndex = 11;
            this.btnOtus.Text = "Thresholding Iterative";
            this.btnOtus.UseVisualStyleBackColor = true;
            this.btnOtus.Click += new System.EventHandler(this.btnOtus_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 161);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(43, 22);
            this.txtLog.TabIndex = 10;
            this.txtLog.Text = "100";
            // 
            // btn8BitPlaneSlicing
            // 
            this.btn8BitPlaneSlicing.Location = new System.Drawing.Point(61, 101);
            this.btn8BitPlaneSlicing.Name = "btn8BitPlaneSlicing";
            this.btn8BitPlaneSlicing.Size = new System.Drawing.Size(136, 23);
            this.btn8BitPlaneSlicing.TabIndex = 19;
            this.btn8BitPlaneSlicing.Text = "8 Bit-Plane Slicing";
            this.btn8BitPlaneSlicing.UseVisualStyleBackColor = true;
            this.btn8BitPlaneSlicing.Click += new System.EventHandler(this.btn8BitPlaneSlicing_Click);
            // 
            // numeric8BitPlaneSlicing
            // 
            this.numeric8BitPlaneSlicing.Location = new System.Drawing.Point(12, 101);
            this.numeric8BitPlaneSlicing.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numeric8BitPlaneSlicing.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric8BitPlaneSlicing.Name = "numeric8BitPlaneSlicing";
            this.numeric8BitPlaneSlicing.Size = new System.Drawing.Size(42, 22);
            this.numeric8BitPlaneSlicing.TabIndex = 18;
            this.numeric8BitPlaneSlicing.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(61, 161);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(51, 23);
            this.btnLog.TabIndex = 9;
            this.btnLog.Text = "Log Transform";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Point Processing";
            // 
            // btnNegative
            // 
            this.btnNegative.Location = new System.Drawing.Point(100, 41);
            this.btnNegative.Name = "btnNegative";
            this.btnNegative.Size = new System.Drawing.Size(75, 23);
            this.btnNegative.TabIndex = 3;
            this.btnNegative.Text = "Negative";
            this.btnNegative.UseVisualStyleBackColor = true;
            this.btnNegative.Click += new System.EventHandler(this.btnNegative_Click);
            // 
            // btnGrayscale
            // 
            this.btnGrayscale.Location = new System.Drawing.Point(13, 41);
            this.btnGrayscale.Name = "btnGrayscale";
            this.btnGrayscale.Size = new System.Drawing.Size(75, 23);
            this.btnGrayscale.TabIndex = 2;
            this.btnGrayscale.Text = "Grayscale";
            this.btnGrayscale.UseVisualStyleBackColor = true;
            this.btnGrayscale.Click += new System.EventHandler(this.btnGrayscale_Click);
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(592, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(161, 47);
            this.panel4.TabIndex = 1;
            // 
            // btnK_Means
            // 
            this.btnK_Means.Location = new System.Drawing.Point(159, 6);
            this.btnK_Means.Name = "btnK_Means";
            this.btnK_Means.Size = new System.Drawing.Size(83, 50);
            this.btnK_Means.TabIndex = 4;
            this.btnK_Means.Text = "K Means";
            this.btnK_Means.UseVisualStyleBackColor = true;
            this.btnK_Means.Click += new System.EventHandler(this.btnK_Means_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.txtIterationLevel);
            this.panel7.Controls.Add(this.txtKValue);
            this.panel7.Controls.Add(this.btnK_Means);
            this.panel7.Location = new System.Drawing.Point(15, 34);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(249, 63);
            this.panel7.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Iteration Level";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "K";
            // 
            // txtIterationLevel
            // 
            this.txtIterationLevel.Location = new System.Drawing.Point(80, 34);
            this.txtIterationLevel.Name = "txtIterationLevel";
            this.txtIterationLevel.Size = new System.Drawing.Size(67, 22);
            this.txtIterationLevel.TabIndex = 7;
            this.txtIterationLevel.Text = "10";
            // 
            // txtKValue
            // 
            this.txtKValue.Location = new System.Drawing.Point(80, 6);
            this.txtKValue.Name = "txtKValue";
            this.txtKValue.Size = new System.Drawing.Size(67, 22);
            this.txtKValue.TabIndex = 6;
            this.txtKValue.Text = "3";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel8.Controls.Add(this.panel25);
            this.panel8.Controls.Add(this.panel12);
            this.panel8.Controls.Add(this.panel13);
            this.panel8.Controls.Add(this.panel21);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(283, 5);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(274, 748);
            this.panel8.TabIndex = 9;
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.btnIDFT);
            this.panel25.Controls.Add(this.btnDFT);
            this.panel25.Controls.Add(this.label14);
            this.panel25.Controls.Add(this.btnDWT);
            this.panel25.Location = new System.Drawing.Point(6, 464);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(264, 171);
            this.panel25.TabIndex = 17;
            // 
            // btnIDFT
            // 
            this.btnIDFT.Location = new System.Drawing.Point(92, 73);
            this.btnIDFT.Name = "btnIDFT";
            this.btnIDFT.Size = new System.Drawing.Size(75, 23);
            this.btnIDFT.TabIndex = 51;
            this.btnIDFT.Text = "IDFT";
            this.btnIDFT.UseVisualStyleBackColor = true;
            this.btnIDFT.Click += new System.EventHandler(this.btnIDFT_Click);
            // 
            // btnDFT
            // 
            this.btnDFT.Location = new System.Drawing.Point(12, 73);
            this.btnDFT.Name = "btnDFT";
            this.btnDFT.Size = new System.Drawing.Size(75, 23);
            this.btnDFT.TabIndex = 41;
            this.btnDFT.Text = "DFT";
            this.btnDFT.UseVisualStyleBackColor = true;
            this.btnDFT.Click += new System.EventHandler(this.btnDFT_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label14.Location = new System.Drawing.Point(8, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(167, 22);
            this.label14.TabIndex = 50;
            this.label14.Text = "Frequency Domain";
            // 
            // btnDWT
            // 
            this.btnDWT.Location = new System.Drawing.Point(12, 44);
            this.btnDWT.Name = "btnDWT";
            this.btnDWT.Size = new System.Drawing.Size(241, 23);
            this.btnDWT.TabIndex = 0;
            this.btnDWT.Text = "Discrete Wavelet Transformation";
            this.btnDWT.UseVisualStyleBackColor = true;
            this.btnDWT.Click += new System.EventHandler(this.btnDWT_Click);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.btnSwell);
            this.panel12.Controls.Add(this.label8);
            this.panel12.Controls.Add(this.btnErosion);
            this.panel12.Controls.Add(this.btnOpening);
            this.panel12.Controls.Add(this.btnClosing);
            this.panel12.Location = new System.Drawing.Point(6, 641);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(264, 92);
            this.panel12.TabIndex = 13;
            // 
            // btnSwell
            // 
            this.btnSwell.Location = new System.Drawing.Point(92, 34);
            this.btnSwell.Name = "btnSwell";
            this.btnSwell.Size = new System.Drawing.Size(75, 23);
            this.btnSwell.TabIndex = 13;
            this.btnSwell.Text = "Dilation";
            this.btnSwell.UseVisualStyleBackColor = true;
            this.btnSwell.Click += new System.EventHandler(this.btnSwell_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label8.Location = new System.Drawing.Point(4, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 22);
            this.label8.TabIndex = 11;
            this.label8.Text = "Morphology";
            // 
            // btnErosion
            // 
            this.btnErosion.Location = new System.Drawing.Point(11, 35);
            this.btnErosion.Name = "btnErosion";
            this.btnErosion.Size = new System.Drawing.Size(75, 23);
            this.btnErosion.TabIndex = 12;
            this.btnErosion.Text = "Erosion";
            this.btnErosion.UseVisualStyleBackColor = true;
            this.btnErosion.Click += new System.EventHandler(this.btnErosion_Click);
            // 
            // btnOpening
            // 
            this.btnOpening.Location = new System.Drawing.Point(11, 63);
            this.btnOpening.Name = "btnOpening";
            this.btnOpening.Size = new System.Drawing.Size(75, 23);
            this.btnOpening.TabIndex = 14;
            this.btnOpening.Text = "Opening";
            this.btnOpening.UseVisualStyleBackColor = true;
            this.btnOpening.Click += new System.EventHandler(this.btnOpening_Click);
            // 
            // btnClosing
            // 
            this.btnClosing.Location = new System.Drawing.Point(92, 63);
            this.btnClosing.Name = "btnClosing";
            this.btnClosing.Size = new System.Drawing.Size(75, 23);
            this.btnClosing.TabIndex = 15;
            this.btnClosing.Text = "Closing";
            this.btnClosing.UseVisualStyleBackColor = true;
            this.btnClosing.Click += new System.EventHandler(this.btnClosing_Click);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.btnPrewittFilter);
            this.panel13.Controls.Add(this.btnVertical);
            this.panel13.Controls.Add(this.btnN45Degree);
            this.panel13.Controls.Add(this.btn45Degree);
            this.panel13.Controls.Add(this.btnHorizontal);
            this.panel13.Controls.Add(this.btnMaxFilter);
            this.panel13.Controls.Add(this.btnMinFilter);
            this.panel13.Controls.Add(this.numericUpDownY);
            this.panel13.Controls.Add(this.numericUpDownX);
            this.panel13.Controls.Add(this.label12);
            this.panel13.Controls.Add(this.label11);
            this.panel13.Controls.Add(this.label6);
            this.panel13.Controls.Add(this.btnMeanFilter);
            this.panel13.Controls.Add(this.btnAvgFiliter);
            this.panel13.Controls.Add(this.btnHighPassFilters);
            this.panel13.Controls.Add(this.btnLowPassFilters);
            this.panel13.Controls.Add(this.btnSobel);
            this.panel13.Controls.Add(this.btnGaussianFilter);
            this.panel13.Controls.Add(this.btnLaplacian);
            this.panel13.Location = new System.Drawing.Point(6, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(264, 261);
            this.panel13.TabIndex = 15;
            // 
            // btnPrewittFilter
            // 
            this.btnPrewittFilter.Location = new System.Drawing.Point(97, 170);
            this.btnPrewittFilter.Name = "btnPrewittFilter";
            this.btnPrewittFilter.Size = new System.Drawing.Size(75, 23);
            this.btnPrewittFilter.TabIndex = 40;
            this.btnPrewittFilter.Text = "Prewitt";
            this.btnPrewittFilter.UseVisualStyleBackColor = true;
            this.btnPrewittFilter.Click += new System.EventHandler(this.btnPrewittFilter_Click);
            // 
            // btnVertical
            // 
            this.btnVertical.Location = new System.Drawing.Point(97, 113);
            this.btnVertical.Name = "btnVertical";
            this.btnVertical.Size = new System.Drawing.Size(75, 23);
            this.btnVertical.TabIndex = 39;
            this.btnVertical.Text = "Vertical";
            this.btnVertical.UseVisualStyleBackColor = true;
            this.btnVertical.Click += new System.EventHandler(this.btnVertical_Click);
            // 
            // btnN45Degree
            // 
            this.btnN45Degree.Location = new System.Drawing.Point(97, 142);
            this.btnN45Degree.Name = "btnN45Degree";
            this.btnN45Degree.Size = new System.Drawing.Size(75, 23);
            this.btnN45Degree.TabIndex = 38;
            this.btnN45Degree.Text = "-45 Degree";
            this.btnN45Degree.UseVisualStyleBackColor = true;
            this.btnN45Degree.Click += new System.EventHandler(this.btnN45Degree_Click);
            // 
            // btn45Degree
            // 
            this.btn45Degree.Location = new System.Drawing.Point(12, 141);
            this.btn45Degree.Name = "btn45Degree";
            this.btn45Degree.Size = new System.Drawing.Size(75, 23);
            this.btn45Degree.TabIndex = 37;
            this.btn45Degree.Text = "45 Degree";
            this.btn45Degree.UseVisualStyleBackColor = true;
            this.btn45Degree.Click += new System.EventHandler(this.btn45Degree_Click);
            // 
            // btnHorizontal
            // 
            this.btnHorizontal.Location = new System.Drawing.Point(12, 113);
            this.btnHorizontal.Name = "btnHorizontal";
            this.btnHorizontal.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontal.TabIndex = 36;
            this.btnHorizontal.Text = "Horizontal";
            this.btnHorizontal.UseVisualStyleBackColor = true;
            this.btnHorizontal.Click += new System.EventHandler(this.btnHorizontal_Click);
            // 
            // btnMaxFilter
            // 
            this.btnMaxFilter.Location = new System.Drawing.Point(178, 53);
            this.btnMaxFilter.Name = "btnMaxFilter";
            this.btnMaxFilter.Size = new System.Drawing.Size(75, 23);
            this.btnMaxFilter.TabIndex = 35;
            this.btnMaxFilter.Text = "Max Filter";
            this.btnMaxFilter.UseVisualStyleBackColor = true;
            this.btnMaxFilter.Click += new System.EventHandler(this.btnMaxFilter_Click);
            // 
            // btnMinFilter
            // 
            this.btnMinFilter.Location = new System.Drawing.Point(12, 53);
            this.btnMinFilter.Name = "btnMinFilter";
            this.btnMinFilter.Size = new System.Drawing.Size(75, 23);
            this.btnMinFilter.TabIndex = 34;
            this.btnMinFilter.Text = "Min Filter";
            this.btnMinFilter.UseVisualStyleBackColor = true;
            this.btnMinFilter.Click += new System.EventHandler(this.btnMinFilter_Click);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownY.Location = new System.Drawing.Point(219, 12);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(34, 22);
            this.numericUpDownY.TabIndex = 33;
            this.numericUpDownY.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownX.Location = new System.Drawing.Point(159, 12);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(34, 22);
            this.numericUpDownX.TabIndex = 32;
            this.numericUpDownX.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label12.Location = new System.Drawing.Point(199, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 14);
            this.label12.TabIndex = 31;
            this.label12.Text = "y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.label11.Location = new System.Drawing.Point(139, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 14);
            this.label11.TabIndex = 30;
            this.label11.Text = "x";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label6.Location = new System.Drawing.Point(3, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 22);
            this.label6.TabIndex = 8;
            this.label6.Text = "Space filter";
            // 
            // btnMeanFilter
            // 
            this.btnMeanFilter.Location = new System.Drawing.Point(92, 53);
            this.btnMeanFilter.Name = "btnMeanFilter";
            this.btnMeanFilter.Size = new System.Drawing.Size(75, 23);
            this.btnMeanFilter.TabIndex = 9;
            this.btnMeanFilter.Text = "Median Filter";
            this.btnMeanFilter.UseVisualStyleBackColor = true;
            this.btnMeanFilter.Click += new System.EventHandler(this.btnMeanFilter_Click);
            // 
            // btnAvgFiliter
            // 
            this.btnAvgFiliter.Location = new System.Drawing.Point(12, 83);
            this.btnAvgFiliter.Name = "btnAvgFiliter";
            this.btnAvgFiliter.Size = new System.Drawing.Size(75, 23);
            this.btnAvgFiliter.TabIndex = 10;
            this.btnAvgFiliter.Text = "Mean Filter";
            this.btnAvgFiliter.UseVisualStyleBackColor = true;
            this.btnAvgFiliter.Click += new System.EventHandler(this.btnAvgFiliter_Click);
            // 
            // btnHighPassFilters
            // 
            this.btnHighPassFilters.Location = new System.Drawing.Point(178, 83);
            this.btnHighPassFilters.Name = "btnHighPassFilters";
            this.btnHighPassFilters.Size = new System.Drawing.Size(75, 23);
            this.btnHighPassFilters.TabIndex = 24;
            this.btnHighPassFilters.Text = "High Pass Filters";
            this.btnHighPassFilters.UseVisualStyleBackColor = true;
            this.btnHighPassFilters.Click += new System.EventHandler(this.btnHighPassFilters_Click);
            // 
            // btnLowPassFilters
            // 
            this.btnLowPassFilters.Location = new System.Drawing.Point(95, 83);
            this.btnLowPassFilters.Name = "btnLowPassFilters";
            this.btnLowPassFilters.Size = new System.Drawing.Size(75, 23);
            this.btnLowPassFilters.TabIndex = 23;
            this.btnLowPassFilters.Text = "Low Pass Filters";
            this.btnLowPassFilters.UseVisualStyleBackColor = true;
            this.btnLowPassFilters.Click += new System.EventHandler(this.btnLowPassFilters_Click);
            // 
            // btnSobel
            // 
            this.btnSobel.Location = new System.Drawing.Point(178, 113);
            this.btnSobel.Name = "btnSobel";
            this.btnSobel.Size = new System.Drawing.Size(75, 23);
            this.btnSobel.TabIndex = 16;
            this.btnSobel.Text = "Sobel filter";
            this.btnSobel.UseVisualStyleBackColor = true;
            this.btnSobel.Click += new System.EventHandler(this.btnSobel_Click);
            // 
            // btnGaussianFilter
            // 
            this.btnGaussianFilter.Location = new System.Drawing.Point(11, 199);
            this.btnGaussianFilter.Name = "btnGaussianFilter";
            this.btnGaussianFilter.Size = new System.Drawing.Size(75, 23);
            this.btnGaussianFilter.TabIndex = 21;
            this.btnGaussianFilter.Text = "Gaussian Filter";
            this.btnGaussianFilter.UseVisualStyleBackColor = true;
            this.btnGaussianFilter.Click += new System.EventHandler(this.btnGaussianFilter_Click);
            // 
            // btnLaplacian
            // 
            this.btnLaplacian.Location = new System.Drawing.Point(12, 170);
            this.btnLaplacian.Name = "btnLaplacian";
            this.btnLaplacian.Size = new System.Drawing.Size(75, 23);
            this.btnLaplacian.TabIndex = 20;
            this.btnLaplacian.Text = "Laplacian";
            this.btnLaplacian.UseVisualStyleBackColor = true;
            this.btnLaplacian.Click += new System.EventHandler(this.btnLaplacian_Click);
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel21.Controls.Add(this.label10);
            this.panel21.Controls.Add(this.btnRelief);
            this.panel21.Controls.Add(this.btnMosaic);
            this.panel21.Controls.Add(this.btnOldStyle);
            this.panel21.Controls.Add(this.btnRippleEffect);
            this.panel21.Controls.Add(this.btnSwirl);
            this.panel21.Controls.Add(this.btnSpherize);
            this.panel21.Controls.Add(this.btnSketch);
            this.panel21.Controls.Add(this.btnInstagram1977);
            this.panel21.Controls.Add(this.btnLomo);
            this.panel21.Location = new System.Drawing.Point(6, 267);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(265, 189);
            this.panel21.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label10.Location = new System.Drawing.Point(7, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(153, 22);
            this.label10.TabIndex = 34;
            this.label10.Text = "Style Space filter";
            // 
            // btnRelief
            // 
            this.btnRelief.Location = new System.Drawing.Point(182, 113);
            this.btnRelief.Name = "btnRelief";
            this.btnRelief.Size = new System.Drawing.Size(75, 23);
            this.btnRelief.TabIndex = 49;
            this.btnRelief.Text = "Relief";
            this.btnRelief.UseVisualStyleBackColor = true;
            this.btnRelief.Click += new System.EventHandler(this.btnRelief_Click);
            // 
            // btnMosaic
            // 
            this.btnMosaic.Location = new System.Drawing.Point(11, 54);
            this.btnMosaic.Name = "btnMosaic";
            this.btnMosaic.Size = new System.Drawing.Size(75, 23);
            this.btnMosaic.TabIndex = 41;
            this.btnMosaic.Text = "Mosaic ";
            this.btnMosaic.UseVisualStyleBackColor = true;
            this.btnMosaic.Click += new System.EventHandler(this.btnMosaic_Click);
            // 
            // btnOldStyle
            // 
            this.btnOldStyle.Location = new System.Drawing.Point(95, 113);
            this.btnOldStyle.Name = "btnOldStyle";
            this.btnOldStyle.Size = new System.Drawing.Size(75, 23);
            this.btnOldStyle.TabIndex = 48;
            this.btnOldStyle.Text = "old Style";
            this.btnOldStyle.UseVisualStyleBackColor = true;
            this.btnOldStyle.Click += new System.EventHandler(this.btnOldStyle_Click);
            // 
            // btnRippleEffect
            // 
            this.btnRippleEffect.Location = new System.Drawing.Point(95, 54);
            this.btnRippleEffect.Name = "btnRippleEffect";
            this.btnRippleEffect.Size = new System.Drawing.Size(75, 23);
            this.btnRippleEffect.TabIndex = 42;
            this.btnRippleEffect.Text = "Ripple Effect";
            this.btnRippleEffect.UseVisualStyleBackColor = true;
            this.btnRippleEffect.Click += new System.EventHandler(this.btnRippleEffect_Click);
            // 
            // btnSwirl
            // 
            this.btnSwirl.Location = new System.Drawing.Point(11, 113);
            this.btnSwirl.Name = "btnSwirl";
            this.btnSwirl.Size = new System.Drawing.Size(75, 23);
            this.btnSwirl.TabIndex = 47;
            this.btnSwirl.Text = "Swirl";
            this.btnSwirl.UseVisualStyleBackColor = true;
            this.btnSwirl.Click += new System.EventHandler(this.btnSwirl_Click);
            // 
            // btnSpherize
            // 
            this.btnSpherize.Location = new System.Drawing.Point(182, 54);
            this.btnSpherize.Name = "btnSpherize";
            this.btnSpherize.Size = new System.Drawing.Size(75, 23);
            this.btnSpherize.TabIndex = 43;
            this.btnSpherize.Text = "Spherize";
            this.btnSpherize.UseVisualStyleBackColor = true;
            this.btnSpherize.Click += new System.EventHandler(this.btnSpherize_Click);
            // 
            // btnSketch
            // 
            this.btnSketch.Location = new System.Drawing.Point(182, 84);
            this.btnSketch.Name = "btnSketch";
            this.btnSketch.Size = new System.Drawing.Size(75, 23);
            this.btnSketch.TabIndex = 46;
            this.btnSketch.Text = "Sketch";
            this.btnSketch.UseVisualStyleBackColor = true;
            this.btnSketch.Click += new System.EventHandler(this.btnSketch_Click);
            // 
            // btnInstagram1977
            // 
            this.btnInstagram1977.Location = new System.Drawing.Point(11, 84);
            this.btnInstagram1977.Name = "btnInstagram1977";
            this.btnInstagram1977.Size = new System.Drawing.Size(75, 23);
            this.btnInstagram1977.TabIndex = 44;
            this.btnInstagram1977.Text = "Instagram1977";
            this.btnInstagram1977.UseVisualStyleBackColor = true;
            this.btnInstagram1977.Click += new System.EventHandler(this.btnInstagram1977_Click);
            // 
            // btnLomo
            // 
            this.btnLomo.Location = new System.Drawing.Point(95, 84);
            this.btnLomo.Name = "btnLomo";
            this.btnLomo.Size = new System.Drawing.Size(75, 23);
            this.btnLomo.TabIndex = 45;
            this.btnLomo.Text = "Lomo";
            this.btnLomo.UseVisualStyleBackColor = true;
            this.btnLomo.Click += new System.EventHandler(this.btnLomo_Click);
            // 
            // panel9
            // 
            this.panel9.Location = new System.Drawing.Point(592, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(161, 47);
            this.panel9.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel26);
            this.panel2.Controls.Add(this.panel24);
            this.panel2.Controls.Add(this.panel14);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Location = new System.Drawing.Point(860, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(562, 766);
            this.panel2.TabIndex = 13;
            // 
            // panel26
            // 
            this.panel26.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel26.Controls.Add(this.numericUpDown2);
            this.panel26.Controls.Add(labelStandardDeviation);
            this.panel26.Controls.Add(this.numericUpDown1);
            this.panel26.Controls.Add(labelMean);
            this.panel26.Controls.Add(this.btnGaussianNoise);
            this.panel26.Controls.Add(this.label15);
            this.panel26.Location = new System.Drawing.Point(4, 402);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(273, 212);
            this.panel26.TabIndex = 53;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(132, 46);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(43, 22);
            this.numericUpDown2.TabIndex = 55;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(61, 46);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(43, 22);
            this.numericUpDown1.TabIndex = 53;
            this.numericUpDown1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // btnGaussianNoise
            // 
            this.btnGaussianNoise.Location = new System.Drawing.Point(189, 46);
            this.btnGaussianNoise.Name = "btnGaussianNoise";
            this.btnGaussianNoise.Size = new System.Drawing.Size(75, 23);
            this.btnGaussianNoise.TabIndex = 51;
            this.btnGaussianNoise.Text = "Gaussian Noise";
            this.btnGaussianNoise.UseVisualStyleBackColor = true;
            this.btnGaussianNoise.Click += new System.EventHandler(this.btnGaussianNoise_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label15.Location = new System.Drawing.Point(9, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 22);
            this.label15.TabIndex = 50;
            this.label15.Text = "Noise";
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel24.Controls.Add(this.btnLBP);
            this.panel24.Controls.Add(this.btnTry);
            this.panel24.Controls.Add(this.label13);
            this.panel24.Location = new System.Drawing.Point(4, 620);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(273, 133);
            this.panel24.TabIndex = 16;
            // 
            // btnLBP
            // 
            this.btnLBP.Location = new System.Drawing.Point(10, 50);
            this.btnLBP.Name = "btnLBP";
            this.btnLBP.Size = new System.Drawing.Size(75, 23);
            this.btnLBP.TabIndex = 52;
            this.btnLBP.Text = "LBP";
            this.btnLBP.UseVisualStyleBackColor = true;
            this.btnLBP.Click += new System.EventHandler(this.btnLBP_Click);
            // 
            // btnTry
            // 
            this.btnTry.Location = new System.Drawing.Point(11, 79);
            this.btnTry.Name = "btnTry";
            this.btnTry.Size = new System.Drawing.Size(75, 23);
            this.btnTry.TabIndex = 51;
            this.btnTry.Text = "try";
            this.btnTry.UseVisualStyleBackColor = true;
            this.btnTry.Click += new System.EventHandler(this.btnTry_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label13.Location = new System.Drawing.Point(9, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(163, 22);
            this.label13.TabIndex = 50;
            this.label13.Text = "Feature Extraction";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel14.Controls.Add(this.label9);
            this.panel14.Controls.Add(this.panel7);
            this.panel14.Location = new System.Drawing.Point(4, 288);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(273, 108);
            this.panel14.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("PMingLiU", 16F);
            this.label9.Location = new System.Drawing.Point(7, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 22);
            this.label9.TabIndex = 16;
            this.label9.Text = "Segmention";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel11.Controls.Add(this.panel5);
            this.panel11.Controls.Add(this.label7);
            this.panel11.Location = new System.Drawing.Point(3, 612);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(836, 155);
            this.panel11.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label7.Location = new System.Drawing.Point(6, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 19);
            this.label7.TabIndex = 10;
            this.label7.Text = "Log console";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel5.Controls.Add(this.labelconsle);
            this.panel5.Font = new System.Drawing.Font("DFKai-SB", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.panel5.Location = new System.Drawing.Point(6, 39);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(824, 107);
            this.panel5.TabIndex = 2;
            // 
            // labelconsle
            // 
            this.labelconsle.AutoSize = true;
            this.labelconsle.Font = new System.Drawing.Font("DFKai-SB", 10F, System.Drawing.FontStyle.Italic);
            this.labelconsle.Location = new System.Drawing.Point(3, 2);
            this.labelconsle.Name = "labelconsle";
            this.labelconsle.Size = new System.Drawing.Size(0, 14);
            this.labelconsle.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel10.Controls.Add(this.panel23);
            this.panel10.Controls.Add(this.panel22);
            this.panel10.Controls.Add(this.resultImageSize);
            this.panel10.Controls.Add(this.labelImageSize);
            this.panel10.Controls.Add(this.pictureBox1);
            this.panel10.Controls.Add(this.pictureBox2);
            this.panel10.Controls.Add(this.label3);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Location = new System.Drawing.Point(3, 46);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(836, 560);
            this.panel10.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label4.Location = new System.Drawing.Point(426, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Result Image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Source Image";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox2.Location = new System.Drawing.Point(430, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 400);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.Location = new System.Drawing.Point(4, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // labelImageSize
            // 
            this.labelImageSize.AutoSize = true;
            this.labelImageSize.Location = new System.Drawing.Point(344, 12);
            this.labelImageSize.Name = "labelImageSize";
            this.labelImageSize.Size = new System.Drawing.Size(53, 12);
            this.labelImageSize.TabIndex = 19;
            this.labelImageSize.Text = "ImageSize";
            // 
            // resultImageSize
            // 
            this.resultImageSize.AutoSize = true;
            this.resultImageSize.Location = new System.Drawing.Point(760, 12);
            this.resultImageSize.Name = "resultImageSize";
            this.resultImageSize.Size = new System.Drawing.Size(53, 12);
            this.resultImageSize.TabIndex = 20;
            this.resultImageSize.Text = "ImageSize";
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.ResultGraphicsR);
            this.panel22.Controls.Add(this.ResultGraphicsG);
            this.panel22.Controls.Add(this.panel18);
            this.panel22.Controls.Add(this.ResultGraphicsB);
            this.panel22.Controls.Add(this.panel20);
            this.panel22.Controls.Add(this.panel19);
            this.panel22.Location = new System.Drawing.Point(430, 428);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(399, 116);
            this.panel22.TabIndex = 15;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Lime;
            this.panel19.Location = new System.Drawing.Point(137, 106);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(128, 10);
            this.panel19.TabIndex = 17;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Red;
            this.panel20.Location = new System.Drawing.Point(3, 106);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(128, 10);
            this.panel20.TabIndex = 16;
            // 
            // ResultGraphicsB
            // 
            this.ResultGraphicsB.BackColor = System.Drawing.SystemColors.Control;
            this.ResultGraphicsB.Location = new System.Drawing.Point(271, 3);
            this.ResultGraphicsB.Name = "ResultGraphicsB";
            this.ResultGraphicsB.Size = new System.Drawing.Size(128, 100);
            this.ResultGraphicsB.TabIndex = 12;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Blue;
            this.panel18.Location = new System.Drawing.Point(270, 106);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(128, 10);
            this.panel18.TabIndex = 18;
            // 
            // ResultGraphicsG
            // 
            this.ResultGraphicsG.BackColor = System.Drawing.SystemColors.Control;
            this.ResultGraphicsG.Location = new System.Drawing.Point(137, 3);
            this.ResultGraphicsG.Name = "ResultGraphicsG";
            this.ResultGraphicsG.Size = new System.Drawing.Size(128, 100);
            this.ResultGraphicsG.TabIndex = 11;
            // 
            // ResultGraphicsR
            // 
            this.ResultGraphicsR.BackColor = System.Drawing.SystemColors.Control;
            this.ResultGraphicsR.Location = new System.Drawing.Point(3, 3);
            this.ResultGraphicsR.Name = "ResultGraphicsR";
            this.ResultGraphicsR.Size = new System.Drawing.Size(128, 100);
            this.ResultGraphicsR.TabIndex = 10;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.sourceGraphicsR);
            this.panel23.Controls.Add(this.sourceGraphicsG);
            this.panel23.Controls.Add(this.sourceGraphicsB);
            this.panel23.Controls.Add(this.panel17);
            this.panel23.Controls.Add(this.panel15);
            this.panel23.Controls.Add(this.panel16);
            this.panel23.Location = new System.Drawing.Point(4, 428);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(400, 110);
            this.panel23.TabIndex = 15;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Lime;
            this.panel16.Location = new System.Drawing.Point(135, 106);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(126, 10);
            this.panel16.TabIndex = 14;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Red;
            this.panel15.Location = new System.Drawing.Point(2, 106);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(127, 10);
            this.panel15.TabIndex = 13;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Blue;
            this.panel17.Location = new System.Drawing.Point(270, 106);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(129, 10);
            this.panel17.TabIndex = 15;
            // 
            // sourceGraphicsB
            // 
            this.sourceGraphicsB.BackColor = System.Drawing.SystemColors.Control;
            this.sourceGraphicsB.Location = new System.Drawing.Point(270, 3);
            this.sourceGraphicsB.Name = "sourceGraphicsB";
            this.sourceGraphicsB.Size = new System.Drawing.Size(128, 100);
            this.sourceGraphicsB.TabIndex = 9;
            // 
            // sourceGraphicsG
            // 
            this.sourceGraphicsG.BackColor = System.Drawing.SystemColors.Control;
            this.sourceGraphicsG.Location = new System.Drawing.Point(134, 3);
            this.sourceGraphicsG.Name = "sourceGraphicsG";
            this.sourceGraphicsG.Size = new System.Drawing.Size(128, 100);
            this.sourceGraphicsG.TabIndex = 9;
            // 
            // sourceGraphicsR
            // 
            this.sourceGraphicsR.BackColor = System.Drawing.SystemColors.Control;
            this.sourceGraphicsR.Location = new System.Drawing.Point(3, 3);
            this.sourceGraphicsR.Name = "sourceGraphicsR";
            this.sourceGraphicsR.Size = new System.Drawing.Size(128, 100);
            this.sourceGraphicsR.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.btnDrawSource);
            this.panel1.Controls.Add(this.btnReplace);
            this.panel1.Controls.Add(this.btnSaveFile);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Location = new System.Drawing.Point(3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 31);
            this.panel1.TabIndex = 0;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(3, 5);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(84, 5);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 3;
            this.btnSaveFile.Text = "Save File";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(246, 5);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 6;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnDrawSource
            // 
            this.btnDrawSource.Location = new System.Drawing.Point(723, 5);
            this.btnDrawSource.Name = "btnDrawSource";
            this.btnDrawSource.Size = new System.Drawing.Size(106, 23);
            this.btnDrawSource.TabIndex = 8;
            this.btnDrawSource.Text = "draw graphics";
            this.btnDrawSource.UseVisualStyleBackColor = true;
            this.btnDrawSource.Click += new System.EventHandler(this.btnDrawSource_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Controls.Add(this.panel11);
            this.panel6.Location = new System.Drawing.Point(12, 12);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(842, 766);
            this.panel6.TabIndex = 14;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(165, 5);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 9;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1426, 795);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Image Process Tool Box";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric8BitPlaneSlicing)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel22.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnGrayscale;
        private System.Windows.Forms.Button btnNegative;
        private System.Windows.Forms.Button btnK_Means;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtKValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIterationLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBinarization;
        private System.Windows.Forms.TextBox txtBinarization;
        private System.Windows.Forms.Label labelOtus;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnSwell;
        private System.Windows.Forms.Button btnErosion;
        private System.Windows.Forms.Button btnHistogramEqualization;
        private System.Windows.Forms.Button btnAvgFiliter;
        private System.Windows.Forms.Button btnMeanFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnClosing;
        private System.Windows.Forms.Button btnOpening;
        private System.Windows.Forms.Button btnOtus;
        private System.Windows.Forms.Button btnExp;
        private System.Windows.Forms.Button btnSobel;
        private System.Windows.Forms.Button btnPowerLaw;
        private System.Windows.Forms.Button btn8BitPlaneSlicing;
        private System.Windows.Forms.NumericUpDown numeric8BitPlaneSlicing;
        private System.Windows.Forms.Button btnLaplacian;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnHighPassFilters;
        private System.Windows.Forms.Button btnLowPassFilters;
        private System.Windows.Forms.Button btnGaussianFilter;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnColouring;
        private System.Windows.Forms.Button btnColorFunction;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRelief;
        private System.Windows.Forms.Button btnMosaic;
        private System.Windows.Forms.Button btnOldStyle;
        private System.Windows.Forms.Button btnRippleEffect;
        private System.Windows.Forms.Button btnSwirl;
        private System.Windows.Forms.Button btnSpherize;
        private System.Windows.Forms.Button btnSketch;
        private System.Windows.Forms.Button btnInstagram1977;
        private System.Windows.Forms.Button btnLomo;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Button btnTry;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnMaxFilter;
        private System.Windows.Forms.Button btnMinFilter;
        private System.Windows.Forms.Button btnVertical;
        private System.Windows.Forms.Button btnN45Degree;
        private System.Windows.Forms.Button btn45Degree;
        private System.Windows.Forms.Button btnHorizontal;
        private System.Windows.Forms.Button btnPrewittFilter;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Button btnDWT;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnLBP;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Button btnGaussianNoise;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button btnIDFT;
        private System.Windows.Forms.Button btnDFT;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelconsle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Panel sourceGraphicsR;
        private System.Windows.Forms.Panel sourceGraphicsG;
        private System.Windows.Forms.Panel sourceGraphicsB;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Panel ResultGraphicsR;
        private System.Windows.Forms.Panel ResultGraphicsG;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel ResultGraphicsB;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label resultImageSize;
        private System.Windows.Forms.Label labelImageSize;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDrawSource;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnRestore;
    }
}


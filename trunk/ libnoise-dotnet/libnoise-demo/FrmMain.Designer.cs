namespace Graphics.Tools.Noise {
	partial class FrmMain {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this._lblOctave = new System.Windows.Forms.Label();
			this._lblFrequency = new System.Windows.Forms.Label();
			this._lblLacunarity = new System.Windows.Forms.Label();
			this._nstpOctave = new System.Windows.Forms.NumericUpDown();
			this._btnStart = new System.Windows.Forms.Button();
			this._tbxFrequency = new System.Windows.Forms.TextBox();
			this._tbxLacunarity = new System.Windows.Forms.TextBox();
			this._gpbxFilter = new System.Windows.Forms.GroupBox();
			this._tbxExponent = new System.Windows.Forms.TextBox();
			this._lblExponent = new System.Windows.Forms.Label();
			this._tbxGain = new System.Windows.Forms.TextBox();
			this._lblGain = new System.Windows.Forms.Label();
			this._lblModule = new System.Windows.Forms.Label();
			this._cbxFilter = new System.Windows.Forms.ComboBox();
			this._tbxOffset = new System.Windows.Forms.TextBox();
			this._lblOffset = new System.Windows.Forms.Label();
			this._cbxQuality = new System.Windows.Forms.ComboBox();
			this._lblQuality = new System.Windows.Forms.Label();
			this._gpbxRender = new System.Windows.Forms.GroupBox();
			this._chkbx = new System.Windows.Forms.CheckBox();
			this._cbxProjection = new System.Windows.Forms.ComboBox();
			this._lblProjection = new System.Windows.Forms.Label();
			this._cbxGradient = new System.Windows.Forms.ComboBox();
			this._lblGradient = new System.Windows.Forms.Label();
			this._lblImageSize = new System.Windows.Forms.Label();
			this._cbxSize = new System.Windows.Forms.ComboBox();
			this._panImageViewport = new System.Windows.Forms.Panel();
			this._imageRendered = new System.Windows.Forms.PictureBox();
			this._prbarRenderProgression = new System.Windows.Forms.ProgressBar();
			this._lblLog = new System.Windows.Forms.Label();
			this._lblProgressPercent = new System.Windows.Forms.Label();
			this._gpbxPrimitive = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this._cbxPrimitive = new System.Windows.Forms.ComboBox();
			this._tbxSeed = new System.Windows.Forms.TextBox();
			this._lblSeed = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this._nstpOctave)).BeginInit();
			this._gpbxFilter.SuspendLayout();
			this._gpbxRender.SuspendLayout();
			this._panImageViewport.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._imageRendered)).BeginInit();
			this._gpbxPrimitive.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblOctave
			// 
			this._lblOctave.AutoSize = true;
			this._lblOctave.Location = new System.Drawing.Point(5, 40);
			this._lblOctave.Name = "_lblOctave";
			this._lblOctave.Size = new System.Drawing.Size(53, 13);
			this._lblOctave.TabIndex = 1;
			this._lblOctave.Text = "Octaves :";
			// 
			// _lblFrequency
			// 
			this._lblFrequency.AutoSize = true;
			this._lblFrequency.Location = new System.Drawing.Point(4, 64);
			this._lblFrequency.Name = "_lblFrequency";
			this._lblFrequency.Size = new System.Drawing.Size(63, 13);
			this._lblFrequency.TabIndex = 2;
			this._lblFrequency.Text = "Frequency :";
			// 
			// _lblLacunarity
			// 
			this._lblLacunarity.AutoSize = true;
			this._lblLacunarity.Location = new System.Drawing.Point(4, 87);
			this._lblLacunarity.Name = "_lblLacunarity";
			this._lblLacunarity.Size = new System.Drawing.Size(62, 13);
			this._lblLacunarity.TabIndex = 3;
			this._lblLacunarity.Text = "Lacunarity :";
			// 
			// _nstpOctave
			// 
			this._nstpOctave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._nstpOctave.Location = new System.Drawing.Point(78, 38);
			this._nstpOctave.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this._nstpOctave.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._nstpOctave.Name = "_nstpOctave";
			this._nstpOctave.Size = new System.Drawing.Size(46, 20);
			this._nstpOctave.TabIndex = 6;
			this._nstpOctave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._nstpOctave.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// _btnStart
			// 
			this._btnStart.Location = new System.Drawing.Point(2, 524);
			this._btnStart.Name = "_btnStart";
			this._btnStart.Size = new System.Drawing.Size(196, 23);
			this._btnStart.TabIndex = 7;
			this._btnStart.Text = "Generate";
			this._btnStart.UseVisualStyleBackColor = true;
			this._btnStart.Click += new System.EventHandler(this._btnStart_Click);
			// 
			// _tbxFrequency
			// 
			this._tbxFrequency.Location = new System.Drawing.Point(78, 61);
			this._tbxFrequency.MaxLength = 10;
			this._tbxFrequency.Name = "_tbxFrequency";
			this._tbxFrequency.Size = new System.Drawing.Size(62, 20);
			this._tbxFrequency.TabIndex = 9;
			this._tbxFrequency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._tbxFrequency_KeyPress);
			// 
			// _tbxLacunarity
			// 
			this._tbxLacunarity.Location = new System.Drawing.Point(78, 84);
			this._tbxLacunarity.MaxLength = 10;
			this._tbxLacunarity.Name = "_tbxLacunarity";
			this._tbxLacunarity.Size = new System.Drawing.Size(62, 20);
			this._tbxLacunarity.TabIndex = 10;
			this._tbxLacunarity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._tbxLacunarity_KeyPress);
			// 
			// _gpbxFilter
			// 
			this._gpbxFilter.Controls.Add(this._tbxExponent);
			this._gpbxFilter.Controls.Add(this._lblExponent);
			this._gpbxFilter.Controls.Add(this._tbxGain);
			this._gpbxFilter.Controls.Add(this._lblGain);
			this._gpbxFilter.Controls.Add(this._lblModule);
			this._gpbxFilter.Controls.Add(this._cbxFilter);
			this._gpbxFilter.Controls.Add(this._tbxOffset);
			this._gpbxFilter.Controls.Add(this._lblOctave);
			this._gpbxFilter.Controls.Add(this._tbxLacunarity);
			this._gpbxFilter.Controls.Add(this._lblFrequency);
			this._gpbxFilter.Controls.Add(this._tbxFrequency);
			this._gpbxFilter.Controls.Add(this._lblLacunarity);
			this._gpbxFilter.Controls.Add(this._lblOffset);
			this._gpbxFilter.Controls.Add(this._nstpOctave);
			this._gpbxFilter.Location = new System.Drawing.Point(2, 90);
			this._gpbxFilter.Name = "_gpbxFilter";
			this._gpbxFilter.Size = new System.Drawing.Size(196, 181);
			this._gpbxFilter.TabIndex = 6;
			this._gpbxFilter.TabStop = false;
			this._gpbxFilter.Text = "Filter";
			// 
			// _tbxExponent
			// 
			this._tbxExponent.Location = new System.Drawing.Point(78, 153);
			this._tbxExponent.MaxLength = 10;
			this._tbxExponent.Name = "_tbxExponent";
			this._tbxExponent.Size = new System.Drawing.Size(62, 20);
			this._tbxExponent.TabIndex = 19;
			this._tbxExponent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._tbxOffset_KeyPress);
			// 
			// _lblExponent
			// 
			this._lblExponent.AutoSize = true;
			this._lblExponent.Location = new System.Drawing.Point(4, 156);
			this._lblExponent.Name = "_lblExponent";
			this._lblExponent.Size = new System.Drawing.Size(61, 13);
			this._lblExponent.TabIndex = 18;
			this._lblExponent.Text = "Exponent  :";
			// 
			// _tbxGain
			// 
			this._tbxGain.Location = new System.Drawing.Point(78, 130);
			this._tbxGain.MaxLength = 10;
			this._tbxGain.Name = "_tbxGain";
			this._tbxGain.Size = new System.Drawing.Size(62, 20);
			this._tbxGain.TabIndex = 17;
			this._tbxGain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._tbxGain_KeyPress);
			// 
			// _lblGain
			// 
			this._lblGain.AutoSize = true;
			this._lblGain.Location = new System.Drawing.Point(5, 133);
			this._lblGain.Name = "_lblGain";
			this._lblGain.Size = new System.Drawing.Size(38, 13);
			this._lblGain.TabIndex = 16;
			this._lblGain.Text = "Gain  :";
			// 
			// _lblModule
			// 
			this._lblModule.AutoSize = true;
			this._lblModule.Location = new System.Drawing.Point(4, 17);
			this._lblModule.Name = "_lblModule";
			this._lblModule.Size = new System.Drawing.Size(48, 13);
			this._lblModule.TabIndex = 14;
			this._lblModule.Text = "Module :";
			// 
			// _cbxFilter
			// 
			this._cbxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxFilter.FormattingEnabled = true;
			this._cbxFilter.Location = new System.Drawing.Point(78, 14);
			this._cbxFilter.Name = "_cbxFilter";
			this._cbxFilter.Size = new System.Drawing.Size(112, 21);
			this._cbxFilter.TabIndex = 15;
			// 
			// _tbxOffset
			// 
			this._tbxOffset.Location = new System.Drawing.Point(78, 107);
			this._tbxOffset.MaxLength = 10;
			this._tbxOffset.Name = "_tbxOffset";
			this._tbxOffset.Size = new System.Drawing.Size(62, 20);
			this._tbxOffset.TabIndex = 11;
			this._tbxOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._tbxOffset_KeyPress);
			// 
			// _lblOffset
			// 
			this._lblOffset.AutoSize = true;
			this._lblOffset.Location = new System.Drawing.Point(5, 110);
			this._lblOffset.Name = "_lblOffset";
			this._lblOffset.Size = new System.Drawing.Size(44, 13);
			this._lblOffset.TabIndex = 4;
			this._lblOffset.Text = "Offset  :";
			// 
			// _cbxQuality
			// 
			this._cbxQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxQuality.FormattingEnabled = true;
			this._cbxQuality.Location = new System.Drawing.Point(78, 39);
			this._cbxQuality.Name = "_cbxQuality";
			this._cbxQuality.Size = new System.Drawing.Size(112, 21);
			this._cbxQuality.TabIndex = 19;
			// 
			// _lblQuality
			// 
			this._lblQuality.AutoSize = true;
			this._lblQuality.Location = new System.Drawing.Point(5, 44);
			this._lblQuality.Name = "_lblQuality";
			this._lblQuality.Size = new System.Drawing.Size(45, 13);
			this._lblQuality.TabIndex = 18;
			this._lblQuality.Text = "Quality :";
			// 
			// _gpbxRender
			// 
			this._gpbxRender.Controls.Add(this._chkbx);
			this._gpbxRender.Controls.Add(this._cbxProjection);
			this._gpbxRender.Controls.Add(this._lblProjection);
			this._gpbxRender.Controls.Add(this._cbxGradient);
			this._gpbxRender.Controls.Add(this._lblGradient);
			this._gpbxRender.Controls.Add(this._lblImageSize);
			this._gpbxRender.Controls.Add(this._cbxSize);
			this._gpbxRender.Location = new System.Drawing.Point(2, 272);
			this._gpbxRender.Name = "_gpbxRender";
			this._gpbxRender.Size = new System.Drawing.Size(196, 128);
			this._gpbxRender.TabIndex = 8;
			this._gpbxRender.TabStop = false;
			this._gpbxRender.Text = "Render";
			// 
			// _chkbx
			// 
			this._chkbx.AutoSize = true;
			this._chkbx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._chkbx.Location = new System.Drawing.Point(5, 96);
			this._chkbx.Name = "_chkbx";
			this._chkbx.Size = new System.Drawing.Size(80, 17);
			this._chkbx.TabIndex = 19;
			this._chkbx.Text = "Seamless : ";
			this._chkbx.UseVisualStyleBackColor = true;
			// 
			// _cbxProjection
			// 
			this._cbxProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxProjection.FormattingEnabled = true;
			this._cbxProjection.Items.AddRange(new object[] {
            "Planar",
            "Spherical",
            "Cylindrical"});
			this._cbxProjection.Location = new System.Drawing.Point(78, 71);
			this._cbxProjection.Name = "_cbxProjection";
			this._cbxProjection.Size = new System.Drawing.Size(112, 21);
			this._cbxProjection.TabIndex = 17;
			// 
			// _lblProjection
			// 
			this._lblProjection.AutoSize = true;
			this._lblProjection.Location = new System.Drawing.Point(6, 74);
			this._lblProjection.Name = "_lblProjection";
			this._lblProjection.Size = new System.Drawing.Size(60, 13);
			this._lblProjection.TabIndex = 16;
			this._lblProjection.Text = "Projection :";
			// 
			// _cbxGradient
			// 
			this._cbxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxGradient.FormattingEnabled = true;
			this._cbxGradient.Items.AddRange(new object[] {
            "Grayscale",
            "Terrain"});
			this._cbxGradient.Location = new System.Drawing.Point(78, 44);
			this._cbxGradient.Name = "_cbxGradient";
			this._cbxGradient.Size = new System.Drawing.Size(112, 21);
			this._cbxGradient.TabIndex = 15;
			// 
			// _lblGradient
			// 
			this._lblGradient.AutoSize = true;
			this._lblGradient.Location = new System.Drawing.Point(6, 47);
			this._lblGradient.Name = "_lblGradient";
			this._lblGradient.Size = new System.Drawing.Size(53, 13);
			this._lblGradient.TabIndex = 14;
			this._lblGradient.Text = "Gradient :";
			// 
			// _lblImageSize
			// 
			this._lblImageSize.AutoSize = true;
			this._lblImageSize.Location = new System.Drawing.Point(6, 21);
			this._lblImageSize.Name = "_lblImageSize";
			this._lblImageSize.Size = new System.Drawing.Size(33, 13);
			this._lblImageSize.TabIndex = 12;
			this._lblImageSize.Text = "Size :";
			// 
			// _cbxSize
			// 
			this._cbxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxSize.FormattingEnabled = true;
			this._cbxSize.Items.AddRange(new object[] {
            "128 x 128",
            "256 x 256",
            "512 x 512",
            "1024 x 1024",
            "256 x 128",
            "512 x 256",
            "1024 x 512",
            "2048 x 1024"});
			this._cbxSize.Location = new System.Drawing.Point(78, 18);
			this._cbxSize.Name = "_cbxSize";
			this._cbxSize.Size = new System.Drawing.Size(112, 21);
			this._cbxSize.TabIndex = 13;
			// 
			// _panImageViewport
			// 
			this._panImageViewport.AutoScroll = true;
			this._panImageViewport.BackColor = System.Drawing.Color.Black;
			this._panImageViewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._panImageViewport.Controls.Add(this._imageRendered);
			this._panImageViewport.Location = new System.Drawing.Point(208, 9);
			this._panImageViewport.Name = "_panImageViewport";
			this._panImageViewport.Size = new System.Drawing.Size(512, 512);
			this._panImageViewport.TabIndex = 14;
			// 
			// _imageRendered
			// 
			this._imageRendered.Anchor = System.Windows.Forms.AnchorStyles.None;
			this._imageRendered.Location = new System.Drawing.Point(128, 121);
			this._imageRendered.MaximumSize = new System.Drawing.Size(1024, 1024);
			this._imageRendered.MinimumSize = new System.Drawing.Size(128, 128);
			this._imageRendered.Name = "_imageRendered";
			this._imageRendered.Size = new System.Drawing.Size(256, 256);
			this._imageRendered.TabIndex = 0;
			this._imageRendered.TabStop = false;
			// 
			// _prbarRenderProgression
			// 
			this._prbarRenderProgression.Location = new System.Drawing.Point(419, 524);
			this._prbarRenderProgression.Name = "_prbarRenderProgression";
			this._prbarRenderProgression.Size = new System.Drawing.Size(199, 23);
			this._prbarRenderProgression.TabIndex = 15;
			this._prbarRenderProgression.Value = 50;
			// 
			// _lblLog
			// 
			this._lblLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._lblLog.Location = new System.Drawing.Point(2, 403);
			this._lblLog.Name = "_lblLog";
			this._lblLog.Size = new System.Drawing.Size(196, 118);
			this._lblLog.TabIndex = 16;
			// 
			// _lblProgressPercent
			// 
			this._lblProgressPercent.AutoSize = true;
			this._lblProgressPercent.BackColor = System.Drawing.SystemColors.Control;
			this._lblProgressPercent.Location = new System.Drawing.Point(624, 529);
			this._lblProgressPercent.Name = "_lblProgressPercent";
			this._lblProgressPercent.Size = new System.Drawing.Size(93, 13);
			this._lblProgressPercent.TabIndex = 17;
			this._lblProgressPercent.Text = "50 % - nnnn line(s)";
			this._lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _gpbxPrimitive
			// 
			this._gpbxPrimitive.Controls.Add(this._cbxQuality);
			this._gpbxPrimitive.Controls.Add(this._lblQuality);
			this._gpbxPrimitive.Controls.Add(this.label2);
			this._gpbxPrimitive.Controls.Add(this._cbxPrimitive);
			this._gpbxPrimitive.Controls.Add(this._tbxSeed);
			this._gpbxPrimitive.Controls.Add(this._lblSeed);
			this._gpbxPrimitive.Location = new System.Drawing.Point(2, 2);
			this._gpbxPrimitive.Name = "_gpbxPrimitive";
			this._gpbxPrimitive.Size = new System.Drawing.Size(196, 91);
			this._gpbxPrimitive.TabIndex = 20;
			this._gpbxPrimitive.TabStop = false;
			this._gpbxPrimitive.Text = "Primitive";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Module :";
			// 
			// _cbxPrimitive
			// 
			this._cbxPrimitive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxPrimitive.FormattingEnabled = true;
			this._cbxPrimitive.Location = new System.Drawing.Point(78, 14);
			this._cbxPrimitive.Name = "_cbxPrimitive";
			this._cbxPrimitive.Size = new System.Drawing.Size(112, 21);
			this._cbxPrimitive.TabIndex = 15;
			// 
			// _tbxSeed
			// 
			this._tbxSeed.Location = new System.Drawing.Point(78, 64);
			this._tbxSeed.MaxLength = 10;
			this._tbxSeed.Name = "_tbxSeed";
			this._tbxSeed.Size = new System.Drawing.Size(62, 20);
			this._tbxSeed.TabIndex = 8;
			this._tbxSeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._tbxSeed_KeyPress);
			// 
			// _lblSeed
			// 
			this._lblSeed.AutoSize = true;
			this._lblSeed.Location = new System.Drawing.Point(5, 68);
			this._lblSeed.Name = "_lblSeed";
			this._lblSeed.Size = new System.Drawing.Size(38, 13);
			this._lblSeed.TabIndex = 0;
			this._lblSeed.Text = "Seed :";
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(729, 551);
			this.Controls.Add(this._gpbxPrimitive);
			this.Controls.Add(this._lblProgressPercent);
			this.Controls.Add(this._lblLog);
			this.Controls.Add(this._prbarRenderProgression);
			this.Controls.Add(this._panImageViewport);
			this.Controls.Add(this._gpbxRender);
			this.Controls.Add(this._gpbxFilter);
			this.Controls.Add(this._btnStart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "libnoise-dotnet 1.0 - demo";
			((System.ComponentModel.ISupportInitialize)(this._nstpOctave)).EndInit();
			this._gpbxFilter.ResumeLayout(false);
			this._gpbxFilter.PerformLayout();
			this._gpbxRender.ResumeLayout(false);
			this._gpbxRender.PerformLayout();
			this._panImageViewport.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._imageRendered)).EndInit();
			this._gpbxPrimitive.ResumeLayout(false);
			this._gpbxPrimitive.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _lblOctave;
		private System.Windows.Forms.Label _lblFrequency;
		private System.Windows.Forms.Label _lblLacunarity;
		private System.Windows.Forms.NumericUpDown _nstpOctave;
		private System.Windows.Forms.Button _btnStart;
		private System.Windows.Forms.TextBox _tbxFrequency;
		private System.Windows.Forms.TextBox _tbxLacunarity;
		private System.Windows.Forms.GroupBox _gpbxFilter;
		private System.Windows.Forms.GroupBox _gpbxRender;
		private System.Windows.Forms.Label _lblImageSize;
		private System.Windows.Forms.ComboBox _cbxSize;
		private System.Windows.Forms.Panel _panImageViewport;
		private System.Windows.Forms.PictureBox _imageRendered;
		private System.Windows.Forms.ProgressBar _prbarRenderProgression;
		private System.Windows.Forms.Label _lblModule;
		private System.Windows.Forms.ComboBox _cbxFilter;
		private System.Windows.Forms.Label _lblGradient;
		private System.Windows.Forms.ComboBox _cbxGradient;
		private System.Windows.Forms.ComboBox _cbxQuality;
		private System.Windows.Forms.Label _lblQuality;
		private System.Windows.Forms.ComboBox _cbxProjection;
		private System.Windows.Forms.Label _lblProjection;
		private System.Windows.Forms.Label _lblLog;
		private System.Windows.Forms.Label _lblProgressPercent;
		private System.Windows.Forms.GroupBox _gpbxPrimitive;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox _cbxPrimitive;
		private System.Windows.Forms.TextBox _tbxOffset;
		private System.Windows.Forms.Label _lblOffset;
		private System.Windows.Forms.TextBox _tbxSeed;
		private System.Windows.Forms.Label _lblSeed;
		private System.Windows.Forms.TextBox _tbxExponent;
		private System.Windows.Forms.Label _lblExponent;
		private System.Windows.Forms.TextBox _tbxGain;
		private System.Windows.Forms.Label _lblGain;
		private System.Windows.Forms.CheckBox _chkbx;
	}
}


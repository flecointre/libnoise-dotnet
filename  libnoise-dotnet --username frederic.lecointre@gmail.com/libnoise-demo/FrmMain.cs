// This file is part of Libnoise c#.
//
// Libnoise c# is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Libnoise c# is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with Libnoise c#.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Graphics.Tools.Noise.Primitive;
using Graphics.Tools.Noise.Filter;
using Graphics.Tools.Noise.Builder;
using Graphics.Tools.Noise.Renderer;
using Graphics.Tools.Noise.Modifier;

namespace Graphics.Tools.Noise {

	public partial class FrmMain :Form {

		protected Bitmap _bitmap;

		public FrmMain(){

			InitializeComponent();
			
#if DEBUG
			this.Text = String.Format("libnoise-dotnet {0} - demo (DEBUG)", Libnoise.VERSION);
#else
			this.Text = String.Format("libnoise-dotnet {0} - demo", Libnoise.VERSION);
#endif


			// Primitive
			_tbxSeed.Text = PrimitiveModule.DEFAULT_SEED.ToString();
			_cbxPrimitive.Items.AddRange( Enum.GetNames(typeof(NoisePrimitive)));
			_cbxPrimitive.SelectedItem = Enum.GetName(typeof(NoisePrimitive), NoisePrimitive.ImprovedPerlin);

			_cbxQuality.Items.AddRange(Enum.GetNames(typeof(NoiseQuality)));
			_cbxQuality.SelectedItem = Enum.GetName(typeof(NoiseQuality), NoiseQuality.Standard);

			// Filter
			_cbxFilter.Items.AddRange(Enum.GetNames(typeof(NoiseFilter)));
			_cbxFilter.SelectedItem = Enum.GetName(typeof(NoiseFilter), NoiseFilter.SumFractal);

			_tbxFrequency.Text = FilterModule.DEFAULT_FREQUENCY.ToString();
			_tbxLacunarity.Text = FilterModule.DEFAULT_LACUNARITY.ToString();
			_tbxOffset.Text = FilterModule.DEFAULT_OFFSET.ToString();
			_tbxExponent.Text = FilterModule.DEFAULT_SPECTRAL_EXPONENT.ToString();
			_tbxGain.Text = FilterModule.DEFAULT_GAIN.ToString();
			_nstpOctave.Value = (decimal)FilterModule.DEFAULT_OCTAVE_COUNT;

			// Render
			_cbxGradient.SelectedItem = "Grayscale";
			_cbxProjection.SelectedItem = "Planar";
			_cbxSize.SelectedItem = "256 x 256";
			_chkbx.Checked = true;

			// Progress
			_prbarRenderProgression.Value = 0;
			_lblProgressPercent.Text = "";
			_prbarRenderProgression.Visible = false;
			_lblProgressPercent.Visible = false;

		}//end FrmMain

		/// <summary>
		/// 
		/// </summary>
		protected void GenerateNoise(){

			EnabledInterface(false);

			// Parse input ------------------------------------------------------------------------------------
			int seed = ParseInt(_tbxSeed.Text, PrimitiveModule.DEFAULT_SEED);
			float frequency = ParseFloat(_tbxFrequency.Text, (float)FilterModule.DEFAULT_FREQUENCY);
			float lacunarity = ParseFloat(_tbxLacunarity.Text, (float)FilterModule.DEFAULT_LACUNARITY);
			float gain = ParseFloat(_tbxGain.Text, (float)FilterModule.DEFAULT_GAIN);
			float offset = ParseFloat(_tbxOffset.Text, (float)FilterModule.DEFAULT_OFFSET);
			float exponent = ParseFloat(_tbxExponent.Text, (float)FilterModule.DEFAULT_SPECTRAL_EXPONENT);
			int octaveCount = (int)_nstpOctave.Value;
			bool seamless = _chkbx.Checked;

			GradientColor gradient = GradientColor.GRAYSCALE;
			NoiseQuality quality = PrimitiveModule.DEFAULT_QUALITY;
			NoisePrimitive primitive = NoisePrimitive.ImprovedPerlin;
			NoiseFilter filter = NoiseFilter.SumFractal;

			try {
				quality = (NoiseQuality)Enum.Parse(typeof(NoiseQuality), _cbxQuality.Text);
			}
			catch {

				MessageBox.Show(
					String.Format("Unknown quality '{0}'", _cbxQuality.Text),
					"Libnoise Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				EnabledInterface(true);

				return;
			}

			try {
				primitive = (NoisePrimitive)Enum.Parse(typeof(NoisePrimitive), _cbxPrimitive.Text);
			}
			catch {

				MessageBox.Show(
					String.Format("Unknown primitive '{0}'", _cbxPrimitive.Text),
					"Libnoise Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				EnabledInterface(true);

				return;
			}

			try {
				filter = (NoiseFilter)Enum.Parse(typeof(NoiseFilter), _cbxFilter.Text);
			}
			catch {
				MessageBox.Show(
					String.Format("Unknown filter '{0}'", _cbxFilter.Text),
					"Libnoise Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				EnabledInterface(true);

				return;
			}

			switch(_cbxGradient.Text) {

				case "Grayscale":
					gradient = GradientColor.GRAYSCALE;
					break;

				case "Terrain":
					gradient = GradientColor.TERRAIN;
					break;

			}//end switch

			// Create module tree ------------------------------------------------------------------------------------

			PrimitiveModule pModule = null;

			switch(primitive) {

				case NoisePrimitive.Constant:
					pModule = new Constant(offset);
					break;

				case NoisePrimitive.Cylinders:
					pModule = new Cylinders((double)offset);
					seamless = false;
					break;

				case NoisePrimitive.Spheres:
					pModule = new Spheres((double)offset);
					seamless = false;
					break;

				case NoisePrimitive.BevinsGradient:
					pModule = new BevinsGradient();
					break;

				case NoisePrimitive.BevinsValue:
					pModule = new BevinsValue();
					break;

				case NoisePrimitive.ImprovedPerlin:
					pModule = new ImprovedPerlin();
					break;

				case NoisePrimitive.SimplexPerlin:
					pModule = new SimplexPerlin();
					break;

			}//end switch

			pModule.Quality = quality;
			pModule.Seed = seed;

			FilterModule fModule = null;
			ScaleBias scale = null;

			switch(filter) {
				case NoiseFilter.Pipe:
					fModule = new Pipe();
					break;

				case NoiseFilter.SumFractal:
					fModule = new SumFractal();
					break;

				case NoiseFilter.SinFractal:
					fModule = new SinFractal();
					break;

				case NoiseFilter.MultiFractal:
					fModule = new MultiFractal();
					// Used to show the difference with our gradient color (-1 + 1)
					scale = new ScaleBias(fModule, 1, -0.8);
					break;

				case NoiseFilter.Billow:
					fModule = new Billow();
					((Billow)fModule).Bias = -0.2;
					((Billow)fModule).Scale = 2;
					break;

				case NoiseFilter.HeterogeneousMultiFractal:
					fModule = new HeterogeneousMultiFractal();
					// Used to show the difference with our gradient color (-1 + 1)
					scale = new ScaleBias(fModule, -1, 2);
					break;

				case NoiseFilter.HybridMultiFractal:
					fModule = new HybridMultiFractal();
					// Used to show the difference with our gradient color (-1 + 1)
					scale = new ScaleBias(fModule, 0.7, -2);
					break;

				case NoiseFilter.RidgedMultiFractal:
					fModule = new RidgedMultiFractal();
					// Used to show the difference with our gradient color (-1 + 1)
					scale = new ScaleBias(fModule, 0.9, -1.25);
					break;

				case NoiseFilter.Voronoi:
					fModule = new Voronoi();
					break;
					
			}//end switch

			fModule.Frequency = frequency;
			fModule.Lacunarity = lacunarity;
			fModule.OctaveCount = octaveCount;
			fModule.Offset = offset;
			fModule.Offset = offset;
			fModule.Gain = gain;
			fModule.Primitive3D = (IModule3D)pModule;

			IModule3D finalModule;

			if(scale == null) {
				finalModule = (IModule3D) fModule;
			}//end if
			else {
				finalModule = scale;
			}//end else

			NoiseMapBuilder projection;

			switch(_cbxProjection.Text) {

				case "Spherical":
					projection = new NoiseMapBuilderSphere();
					((NoiseMapBuilderSphere)projection).SetBounds(-90.0, 90.0, -180.0, 180.0); // degrees
					break;

				case "Cylindrical":
					projection = new NoiseMapBuilderCylinder();
					((NoiseMapBuilderCylinder)projection).SetBounds(-180.0, 180.0, -10.0, 10.0); 
					break;

				case "Planar":
				default:
					double bound = 2;
					projection = new NoiseMapBuilderPlane(bound, bound*2, bound, bound*2, seamless);
					//projection = new NoiseMapBuilderPlane(-bound, bound, -bound, bound, seamless);
					//projection = new NoiseMapBuilderPlane(0, bound, 0, bound, seamless);
					break;

			}//end switch

			int width = 0;
			int height = 0;

			switch(_cbxSize.Text) {

				case "256 x 256":
					width = 256;
					height = 256;
					break;

				case "512 x 512":
					width = 512;
					height = 512;
					break;

				case "1024 x 1024":
					width = 1024;
					height = 1024;
					break;

				case "256 x 128":
					width = 256;
					height = 128;
					break;

				case "512 x 256":
					width = 512;
					height = 256;
					break;

				case "1024 x 512":
					width = 1024;
					height = 512;
					break;

				case "2048 x 1024":
					width = 2048;
					height = 1024;
					break;
				default:

				case "128 x 128":
					width = 128;
					height = 128;
					break;

			}//end switch

			// ------------------------------------------------------------------------------------------------
			// 0 - Initializing
			_prbarRenderProgression.Visible = true;
			_lblProgressPercent.Visible = true;
			_prbarRenderProgression.Value = 0;;
			_lblProgressPercent.Text = "";

			_lblLog.Text = String.Format("Create a {0} image with a {1} projection\n", _cbxSize.Text, _cbxProjection.Text);
			
			System.Diagnostics.Stopwatch watchDog = new System.Diagnostics.Stopwatch();
			TimeSpan ts;
			double elaspedTime = 0;

			// ------------------------------------------------------------------------------------------------
			// 1 - Build the noise map
			watchDog.Reset();

			_prbarRenderProgression.Value = 0;
			_lblLog.Text += "Building noise map ... ";

			NoiseMap noiseMap = new NoiseMap();

			projection.SetSize(width, height);
			projection.SourceModule  = finalModule;
			projection.NoiseMap = noiseMap;
			projection.CallBack = delegate(int line) {

				line++;

				watchDog.Stop();

				//Process message
				Application.DoEvents();

				_prbarRenderProgression.Value = (int)(line*100/height);
				_lblProgressPercent.Text = String.Format("{0} % - {1} line(s)", _prbarRenderProgression.Value, line);
				
				watchDog.Start();
			};

			watchDog.Start();
			projection.Build();
			watchDog.Stop();

			ts = watchDog.Elapsed;
			elaspedTime += ts.TotalMilliseconds;

			_lblLog.Text += String.Format("{0:00}:{1:00} {2:00},{3:0000}\n",
				ts.Hours, ts.Minutes,
				ts.Seconds, ts.Milliseconds *10
			);

			// ------------------------------------------------------------------------------------------------
			// 2 - Render image
			// Create a renderer, BitmapAdaptater create a System.Drawing.Bitmap on the fly
			watchDog.Reset();
			_prbarRenderProgression.Value = 0;
			_lblLog.Text += "Rendering image ... ";

			ImageRenderer renderer =  new ImageRenderer();
			renderer.NoiseMap = noiseMap;
			renderer.Gradient = gradient;
			renderer.LightBrightness = 2;
			renderer.LightContrast = 8;
			//renderer.LightEnabled = true;
			
			//Graphics.Tools.Noise.Renderer.Image image = new Graphics.Tools.Noise.Renderer.Image();
			//renderer.Image = image;

			BitmapAdaptater bitmapAdaptater = new BitmapAdaptater(width, height);
			renderer.Image = bitmapAdaptater;

			renderer.CallBack = delegate(int line) {

				line++;

				watchDog.Stop();

				//Process message
				Application.DoEvents();

				_prbarRenderProgression.Value = (int)(line*100/height);
				_lblProgressPercent.Text = String.Format("{0} % - {1} line(s)", _prbarRenderProgression.Value, line);

				watchDog.Start();
			};

			// Render the texture.
			watchDog.Start();
			renderer.Render();
			watchDog.Stop();

			ts = watchDog.Elapsed;
			elaspedTime += ts.TotalMilliseconds;

			_lblLog.Text += String.Format("{0:00}:{1:00} {2:00},{3:0000}\n",
				ts.Hours, ts.Minutes,
				ts.Seconds, ts.Milliseconds *10
			);

			// ------------------------------------------------------------------------------------------------
			// 3 - Painting

			_imageRendered.Width = width;
			_imageRendered.Height = height;

			//_imageRendered.Image = _bitmap;
			_imageRendered.Image = bitmapAdaptater.Bitmap;
			bitmapAdaptater.Bitmap.Save("rendered.bmp");

			if(_imageRendered.Width > _panImageViewport.Width) {
				_imageRendered.Left = 0;
				
			}//end if
			else {
				_imageRendered.Left = (_panImageViewport.Width - _imageRendered.Width) /2;
			}//end else

			if(_imageRendered.Height > _panImageViewport.Height) {
				_imageRendered.Top = 0;
			}//end if
			else {
				_imageRendered.Top = (_panImageViewport.Height - _imageRendered.Height) /2;
			}//end else

			if(_imageRendered.Width > _panImageViewport.Width || _imageRendered.Height > _panImageViewport.Height) {
				_imageRendered.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
				_panImageViewport.AutoScroll = true;
			}//end if
			else {
				_panImageViewport.AutoScroll = false;
			}//end else

			// ----------------------------------------------------------------

			ts = TimeSpan.FromMilliseconds(elaspedTime);

			// Format and display the TimeSpan value.
			_lblLog.Text += String.Format("Total duration : {0:00}:{1:00} {2:00},{3:0000}\n",
				ts.Hours, ts.Minutes,
				ts.Seconds, ts.Milliseconds *10
			);

			EnabledInterface(true);
			
			_prbarRenderProgression.Value = 0;
			_lblProgressPercent.Text = "";
			_prbarRenderProgression.Visible = false;
			_lblProgressPercent.Visible = false;
			
		}//end GenerateNoise

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enabled"></param>
		protected void EnabledInterface(bool enabled) {

			_cbxGradient.Enabled = enabled;
			_cbxPrimitive.Enabled = enabled;
			_cbxFilter.Enabled = enabled;
			_cbxProjection.Enabled = enabled;
			_cbxQuality.Enabled = enabled;
			_cbxSize.Enabled = enabled;
			_tbxFrequency.Enabled = enabled;
			_tbxLacunarity.Enabled = enabled;
			_tbxOffset.Enabled = enabled;
			_tbxSeed.Enabled = enabled;
			_tbxGain.Enabled = enabled;
			_tbxOffset.Enabled = enabled;
			_tbxExponent.Enabled = enabled;
			_nstpOctave.Enabled = enabled;
			_btnStart.Enabled = enabled;

		}//end EnabledInterface

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		protected int ParseInt(string value, int defaultValue) {

			try {
				return Int32.Parse(
					value,
					NumberStyles.Number,
					new CultureInfo("en-US", false).NumberFormat
				);
			}//end try
			catch {
				return defaultValue;
			}//end catch

		}//end ParseInt

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		protected float ParseFloat(string value, float defaultValue) {

			value = value.Replace(',', '.');

			try {
				return Single.Parse(
					value,
					NumberStyles.Number,
					new CultureInfo("en-US", false).NumberFormat
				);
			}//end try
			catch {
				return defaultValue;
			}//end catch

		}//end ParseInt

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <param name="minRange"></param>
		/// <param name="maxRange"></param>
		protected void TextFilterNumeric_keyPress(TextBox sender, KeyPressEventArgs e, int minRange, int maxRange) {
			String allowed = "0123456789";
			int value;

			try {

				if(allowed.IndexOf(e.KeyChar) >= 0) {

					int selStart = sender.SelectionStart;
					string before = sender.Text.Substring(0, selStart);
					string after = sender.Text.Substring(before.Length);

					value = Convert.ToInt32(string.Concat(before, e.KeyChar, after));

					if(value >= minRange && value <= maxRange) {
						sender.Text = value.ToString();
						sender.SelectionStart = before.Length + 1;
					}//end if

					e.Handled = true;

				}//end if
				else {
					// Lets the default back space
					if(e.KeyChar != (char)Keys.Back) {
						e.Handled = true;
					}//end if
				}//end else

			}//end try
			catch {
				e.Handled = true;
			}//end catch

		}//end TextFilterNumeric_keyPress

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <param name="minRange"></param>
		/// <param name="maxRange"></param>
		protected void TextFilterNumeric_keyPress(TextBox sender, KeyPressEventArgs e, float minRange, float maxRange) {
			String allowed = "0123456789.,";
			float value;

			try {

				if(allowed.IndexOf(e.KeyChar) >= 0) {

					if(e.KeyChar == ',') {
						e.KeyChar = '.';
					}//end if

					int selStart = sender.SelectionStart;
					string before = sender.Text.Substring(0, selStart);
					string after = sender.Text.Substring(before.Length);
					string newText = string.Concat(before, e.KeyChar, after);

					value = Single.Parse(
						newText, 
						NumberStyles.Number, 
						new CultureInfo( "en-US", false ).NumberFormat
					);

					if(value >= minRange && value <= maxRange) {
						sender.Text = newText;
						sender.SelectionStart = before.Length + 1;
					}//end if
					
					e.Handled = true;

				}//end if
				else {

					// Lets the default back space
					if(e.KeyChar != (char)Keys.Back) {
						e.Handled = true;
					}//end if
				}//end else

			}//end try
			catch {
				e.Handled = true;
			}//end catch

		}//end TextFilterInt32_keyPress

		private void _tbxSeed_KeyPress(object sender, KeyPressEventArgs e) {
			TextFilterNumeric_keyPress((TextBox)sender, e, (int)0, int.MaxValue);
		}

		private void _tbxFrequency_KeyPress(object sender, KeyPressEventArgs e) {
			TextFilterNumeric_keyPress((TextBox)sender, e, 0f, 10.0f);
		}

		private void _tbxLacunarity_KeyPress(object sender, KeyPressEventArgs e) {
			TextFilterNumeric_keyPress((TextBox)sender, e, 0f, 10.0f);
		}

		private void _tbxGain_KeyPress(object sender, KeyPressEventArgs e) {
			TextFilterNumeric_keyPress((TextBox)sender, e, 0f, 10.0f);
		}

		private void _tbxOffset_KeyPress(object sender, KeyPressEventArgs e) {
			TextFilterNumeric_keyPress((TextBox)sender, e, 0f, 10.0f);
		}

		private void _tbxExponent_KeyPress(object sender, KeyPressEventArgs e) {
			TextFilterNumeric_keyPress((TextBox)sender, e, 0f, 10.0f);
		}

		private void _btnStart_Click(object sender, EventArgs e) {
			GenerateNoise();
		}

	}//end class
}//end namespace

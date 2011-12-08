// This file is part of libnoise-dotnet.
//
// libnoise-dotnet is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// libnoise-dotnet is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with libnoise-dotnet.  If not, see <http://www.gnu.org/licenses/>.

using System.Drawing;
using NoiseNS = Graphics.Tools.Noise.Renderer;
using System.Drawing.Imaging;
using System;

namespace Graphics.Tools.Noise.Ext.Dotnet {

	/// <summary>
	/// Implements an image, a 2-dimensional array of color values.
	///
	/// An image can be used to store a color texture.
	///
	/// These color values are of type IColor.
	/// 
	/// TODO Implement unimplemented method
	/// TODO Create a dotnet projet for this extension
	/// Utiliser lockbits
	/// http://msdn.microsoft.com/fr-fr/library/5ey6h79d.aspx
	/// </summary>
	public class BitmapAdaptater :IMap2D<NoiseNS::IColor> {

		#region Fields
		/// <summary>
		/// The value used for all positions outside of the map.
		/// </summary>
		protected NoiseNS::IColor _borderValue;

		/// <summary>
		/// The bitmap
		/// </summary>
		protected Bitmap _adaptatee;

		/// <summary>
		/// The width of the map, internal use
		/// </summary>
		protected int _width = 0;

		/// <summary>
		/// The height of the map, internal use
		/// </summary>
		protected int _height = 0;

		/// <summary>
		/// Internal data buffer for performances purpose
		/// </summary>
		protected byte[] _data;

		/// <summary>
		/// Flags that indicates if some bitmap changes need to be applied.
		/// As is an expansive operation, BitmapAdaptater.apply is only called
		/// in the Bitmap accessor if changes have been previously done
		/// </summary>
		protected bool _bitsLocked = false;

		/// <summary>
		/// Bitmap information
		/// </summary>
		protected BitmapData _bmData;

		/// <summary>
		/// Size in byte of one pixel data
		/// </summary>
		protected byte _structSize = 1;

		#endregion

		#region Accessors

		/// <summary>
		/// Gets the width of the map
		/// </summary>
		public int Width {
			get { return _width; }
		}//end Width

		/// <summary>
		/// Gets the height of the map
		/// </summary>
		public int Height {
			get { return _height; }
		}//end Height

		/// <summary>
		/// Gets the border value of the map
		/// </summary>
		public NoiseNS::IColor BorderValue {
			get { return _borderValue; }
			set { _borderValue = value; }
		}// end BorderValue

		/// <summary>
		/// Gets the adaptated System.Drawing.Bitmap
		/// </summary>
		public Bitmap Bitmap {
			get {

				if(_bitsLocked) {
					Apply();
				}//end if

				return _adaptatee; 
			}
			/*
			set { 
				_adaptatee = value; 
				_width = _adaptatee.Width;
				_height = _adaptatee.Height;
			}
			*/
		}// end BorderValue

		#endregion

		#region Ctor/Dtor

		/// <summary>
		/// Create a new Bitmap with the given values
		/// </summary>
		/// <param name="bitmap">The bitmap to adapt.</param>
		public BitmapAdaptater(Bitmap bitmap) {

			switch(bitmap.PixelFormat) {

				case PixelFormat.Canonical: //RGBA
				case PixelFormat.Format8bppIndexed: //R
				case PixelFormat.Format24bppRgb: // RGB
				case PixelFormat.Format32bppRgb: // RGB_
				case PixelFormat.Format32bppArgb: //RGBA
					//ok

					break;
				default:
					throw new ArgumentException("Unsupported image format : " + bitmap.PixelFormat.ToString());
			}//end switch

			_adaptatee = bitmap;
			_borderValue = NoiseNS::Color.WHITE;

			_width = _adaptatee.Width;
			_height = _adaptatee.Height;

			AllocateBuffer();

		}//End BitmapAdaptater

		/// <summary>
		/// Create a new Bitmap with the given values
		/// </summary>
		/// <param name="width">The width of the new bitmap.</param>
		/// <param name="height">The height of the new bitmap</param>
		public BitmapAdaptater(int width, int height) {

			_adaptatee = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			_borderValue = NoiseNS::Color.WHITE;

			_width = _adaptatee.Width;
			_height = _adaptatee.Height;

			AllocateBuffer();

		}//End BitmapAdaptater

		#endregion

		#region IMap2D<IColor> Members

		/// <summary>
		/// Gets a value at a specified position in the map.
		///
		/// This method does nothing if the map object is empty or the
		/// position is outside the bounds of the noise map.
		/// </summary>
		/// <param name="x">The x coordinate of the position</param>
		/// <param name="y">The y coordinate of the position</param>
		public NoiseNS::IColor GetValue(int x, int y) {

			if(_adaptatee != null 
				&& (x >= 0 && x < _width) 
				&& (y >= 0 && y < _height)
			) {

				if(_bitsLocked) {

					// Noise.Image start to bottom left
					// Drawing.Bitmap start to top left
					int indexBase = _bmData.Stride * (_height -1 -y) + x *_structSize;

					switch(_bmData.PixelFormat) {

						case PixelFormat.Format8bppIndexed: //R
							return new NoiseNS::Color(_data[indexBase], _data[indexBase], _data[indexBase], 255);

						case PixelFormat.Format24bppRgb: // RGB
						case PixelFormat.Format32bppRgb: // RGB_
							return new NoiseNS::Color(_data[indexBase +2], _data[indexBase +1], _data[indexBase], 255);

						case PixelFormat.Canonical: //RGBA
						case PixelFormat.Format32bppArgb: //RGBA
							return new NoiseNS::Color(_data[indexBase +2], _data[indexBase +1], _data[indexBase], _data[indexBase +3]);

					}//end switch

				}//end if
				else {
					// Noise.Image start to bottom left
					// Drawing.Bitmap start to top left
					System.Drawing.Color sysColor = _adaptatee.GetPixel(x, _height -1 -y);
					return new NoiseNS::Color(sysColor.R, sysColor.G, sysColor.B, sysColor.A);
				}//end else

			}//end if

			return _borderValue;

		}//end GetValue

		/// <summary>
		/// Sets a value at a specified position in the map.
		///
		/// This method does nothing if the map object is empty or the
		/// position is outside the bounds of the noise map.
		/// </summary>
		/// <param name="x">The x coordinate of the position</param>
		/// <param name="y">The y coordinate of the position</param>
		/// <param name="value">The value to set at the given position</param>
		public void SetValue(int x, int y, NoiseNS::IColor value) {

			if(_adaptatee != null 
				&& (x >= 0 && x < _width) 
				&& (y >= 0 && y < _height)
			) {

				if(_bitsLocked) {

					// Noise.Image start to bottom left
					// Drawing.Bitmap start to top left
					int indexBase = _bmData.Stride * (_height -1 -y) + x *_structSize;

					switch(_bmData.PixelFormat) {

						
						case PixelFormat.Format8bppIndexed: //R
							_data[indexBase] = value.Red;

							break;

						case PixelFormat.Format24bppRgb: // RGB
							_data[indexBase] = value.Blue;
							_data[indexBase +1] = value.Green;
							_data[indexBase +2] = value.Red;

							break;

						case PixelFormat.Format32bppRgb: // RGB_
							_data[indexBase] = value.Blue;
							_data[indexBase +1] = value.Green;
							_data[indexBase +2] = value.Red;
							_data[indexBase +3] = 255;

							break;

						case PixelFormat.Canonical: //RGBA
						case PixelFormat.Format32bppArgb: //RGBA
							_data[indexBase] = value.Blue;
							_data[indexBase +1] = value.Green;
							_data[indexBase +2] = value.Red;
							_data[indexBase +3] = value.Alpha;

							break;

					}//end switch

				}//end if
				else {
					// Noise.Image start to bottom left
					// Drawing.Bitmap start to top left
					_adaptatee.SetPixel(
						x,
						_adaptatee.Height -1 -y,
						System.Drawing.Color.FromArgb(value.Alpha, value.Red, value.Green, value.Blue)
					);
				}//end else

			}//end if

		}//end SetValue

		/// <summary>
		/// Sets the new size for the map.
		/// 
		/// </summary>
		/// <param name="width">width The new width for the bitmap</param>
		/// <param name="height">height The new height for the bitmap</param>
		public void SetSize(int width, int height) {
			if(_adaptatee.Width != width || _adaptatee.Height != height) {
				throw new System.NotImplementedException("System.Drawing.Bitmap does not support resize");
			}//end if
		}//end SetSize

		/// <summary>
		/// Resets the bitmap
		/// </summary>
		public void Reset() {
			throw new System.NotImplementedException();
		}//end Reset

		/// <summary>
		/// Clears the bitmap to a specified value.
		/// This method is a O(n) operation, where n is equal to width * height.
		/// </summary>
		/// <param name="color">The color that all positions within the bitmap are cleared to.</param>
		public void Clear(NoiseNS::IColor color) {
			throw new System.NotImplementedException();
		}//end Clear

		/// <summary>
		/// Clears the bitmap to a Color.WHITE value
		/// </summary>
		public void Clear() {
			throw new System.NotImplementedException();
		}//end Clear

		/// <summary>
		/// Find the lowest and highest value in the map
		/// </summary>
		/// <param name="min">the lowest value</param>
		/// <param name="max">the highest value</param>
		public void MinMax(out NoiseNS::IColor min, out NoiseNS::IColor max) {
			throw new System.NotImplementedException();
		}//end Clear

		#endregion

		#region Internal

		/// <summary>
		/// Allocate a buffer
		/// </summary>
		protected void AllocateBuffer() {

			if(_bitsLocked) {
				throw new Exception("Buffer already allocated");
			}//end if

			try {

				switch(_adaptatee.PixelFormat) {

					case PixelFormat.Canonical: //RGBA
					case PixelFormat.Format32bppRgb: // RGB_
					case PixelFormat.Format32bppArgb: //RGBA
						_structSize = 4;
						break;

					case PixelFormat.Format8bppIndexed: //R
						_structSize = 1;
						break;

					case PixelFormat.Format24bppRgb: // RGB
						_structSize = 3;
						break;

					default:
						throw new ArgumentException("Unsupported image format : " + _adaptatee.PixelFormat.ToString());
				}//end switch

				// Lock memory region
				Rectangle region = new Rectangle(0, 0, _width, _height);
				_bmData =  _adaptatee.LockBits(region, ImageLockMode.ReadWrite, _adaptatee.PixelFormat);

				// BitmapData.Stride could be a negative number
				int size = Math.Abs(_bmData.Stride) * _height;

				// Create buffer
				if(_data == null) {
					_data = new byte[size];
				}//end if
				else {
					Array.Resize<byte>(ref _data, size);
				}//end else

				// Memcopy
				System.Runtime.InteropServices.Marshal.Copy(_bmData.Scan0, _data, 0, size);

				_bitsLocked = true;



			}//end try
			catch(Exception e) {
				throw new Exception("Unable to lock bitmap memory", e);
			}//end catch

		}//end AllocateBuffer

		/// <summary>
		/// 
		/// </summary>
		protected void Apply(){

			if(!_bitsLocked) {
				throw new Exception("Buffer is empty");
			}//end if

			try {

				// Memcopy
				System.Runtime.InteropServices.Marshal.Copy(_data, 0, _bmData.Scan0, _data.Length);

				// Unlock region
				_adaptatee.UnlockBits(_bmData);

				_bitsLocked = true;
				_bmData = null;
				_data = null;

			}//end try
			catch(Exception e) {
				throw new Exception("Unable to unlock bitmap memory", e);
			}//end catch

		}//end Apply

		#endregion

	}//end class

}//end namespace

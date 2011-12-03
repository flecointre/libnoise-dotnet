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

		#endregion

		#region Accessors

		/// <summary>
		/// Gets the width of the map
		/// </summary>
		public int Width {
			get { return _adaptatee.Width; }
		}//end Width

		/// <summary>
		/// Gets the height of the map
		/// </summary>
		public int Height {
			get { return _adaptatee.Height; }
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
		/// TODO implement a setter
		/// </summary>
		public Bitmap Bitmap {
			get { return _adaptatee; }
			//set { _adaptatee = value; }
		}// end BorderValue

		#endregion

		#region Ctor/Dtor

		/// <summary>
		/// Create a new Bitmap with the given values
		/// </summary>
		/// <param name="bitmap">The bitmap to adapt.</param>
		public BitmapAdaptater(Bitmap bitmap) {
			_adaptatee = bitmap;
			_borderValue = NoiseNS::Color.WHITE;
		}//End BitmapAdaptater

		/// <summary>
		/// Create a new Bitmap with the given values
		/// </summary>
		/// <param name="width">The width of the new bitmap.</param>
		/// <param name="height">The height of the new bitmap</param>
		public BitmapAdaptater(int width, int height) {
			_adaptatee = new Bitmap(width, height);
			_borderValue = NoiseNS::Color.WHITE;
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
				&& (x >= 0 && x < _adaptatee.Width) 
				&& (y >= 0 && y < _adaptatee.Height)
			) {
				System.Drawing.Color sysColor = _adaptatee.GetPixel(x, y);
				return new NoiseNS::Color(sysColor.R, sysColor.G, sysColor.B, sysColor.A);
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
				&& (x >= 0 && x < _adaptatee.Width) 
				&& (y >= 0 && y < _adaptatee.Height)
			) {
				// Noise.Image start to bottom left
				// Drawing.Bitmap start to top left
				_adaptatee.SetPixel(
					x, 
					_adaptatee.Height -1 -y, 
					System.Drawing.Color.FromArgb(value.Alpha, value.Red, value.Green, value.Blue)
				);

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

		#endregion
	}//end class

}//end namespace

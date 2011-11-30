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
// 
// From the original Jason Bevins's Libnoise (http://libnoise.sourceforge.net)

using Graphics.Tools.Noise.Utils;

namespace Graphics.Tools.Noise.Renderer {

	/// <summary>
	/// Implements an image, a 2-dimensional array of color values.
	///
	/// An image can be used to store a color texture.
	///
	/// These color values are of type Color.
	/// </summary>
	public class Image :DataMap<Color>, IMap2D<Color> {

		#region Fields

		#endregion

		#region Accessors

		#endregion

		#region Ctor/Dtor
		/// <summary>
		/// Create an empty Image
		/// </summary>
		public Image() {
			_borderValue = Color.TRANSPARENT;
			AllocateBuffer();
		}//End NoiseMap

		/// <summary>
		/// Create a new Image with the given values
		///
		/// The width and height values are positive.
		/// The width and height values do not exceed the maximum
		/// possible width and height for the image.
		///
		/// @throw System.ArgumentException See the preconditions.
		/// @throw noise::ExceptionOutOfMemory Out of memory.
		///
		/// Creates a image with uninitialized values.
		///
		/// It is considered an error if the specified dimensions are not
		/// positive.
		/// </summary>
		/// <param name="width">The width of the new noise map.</param>
		/// <param name="height">The height of the new noise map</param>
		public Image(int width, int height){
			_borderValue = Color.WHITE;
			AllocateBuffer(width, height);
			
		}//End NoiseMap

		/// <summary>
		/// Copy constructor
		/// @throw noise::ExceptionOutOfMemory Out of memory.
		/// </summary>
		/// <param name="copy">The image to copy</param>
		public Image(Image copy) {
			_borderValue = Color.WHITE;
			CopyFrom(copy);
		}//End NoiseMap

		#endregion

		#region Interaction

		#endregion

		#region Internal

		/// <summary>
		/// Return the memory size of a Color
		/// 
		/// </summary>
		/// <returns>The memory size of a Color</returns>
		protected override int SizeofT() {
			return 64; // 4* byte(8) + 1 int(32)
		}//end protected

		#endregion

	}//end class

}//end namespace

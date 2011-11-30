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
// c# port by Frédéric Lecointre (frederic.lecointre@burnweb.net)

using System;
using System.IO;
using Graphics.Tools.Noise.Renderer;

namespace Graphics.Tools.Noise.Utils {

	/// <summary>
	/// Windows bitmap image writer class.
	///
	/// This class creates a file in Windows bitmap (*.bmp) format given the
	/// contents of an image object.
	///
	/// <b>Writing the image</b>
	///
	/// To write the image to a file, perform the following steps:
	/// - Pass the filename to the Filename property.
	/// - Pass an Image object to the Image property.
	/// - Call the WriteFile().
	/// </summary>
	public class BMPWriter {

		#region constants
		
		/// <summary>
		/// Bitmap header size.
		/// </summary>
		public const int BMP_HEADER_SIZE = 54;

		#endregion

		#region Fields

		/// <summary>
		/// The destination image
		/// </summary>
		protected Image _image;

		/// <summary>
		/// the name of the file to write.
		/// </summary>
		protected string _filename;

		

		#endregion

		#region Accessors
			
		/// <summary>
		/// Gets or sets the name of the file to write.
		/// </summary>
		public string Filename {
			get { return _filename; }
			set { _filename = value; }
		}

		/// <summary>
		/// Gets or sets the destination image
		/// </summary>
		public Image Image {
			get { return _image; }
			set { _image = value; }
		}

		#endregion

		#region Ctor/Dtor

		#endregion

		#region Interaction
		
		/// <summary>
		/// Writes the contents of the image object to the file.
		///
		/// @pre Filename has been previously defined.
		/// @pre Image has been previously defined.
		///
		/// @throw ArgumentException See the preconditions.
		/// @throw IOException An I/O exception occurred.
		/// Possibly the file could not be written.
		/// 
		/// </summary>
		public void WriteFile(){ 

			if(_image == null) {
				throw new ArgumentException("An image map must be provided");
			}//end id

			int width  = _image.Width;
			int height = _image.Height;

			// The width of one line in the file must be aligned on a 4-byte boundary.
			int bufferSize = CalcWidthByteCount(width);
			int destSize   = bufferSize * height;

			// This buffer holds one horizontal line in the destination file.
			// Allocate a buffer to hold one horizontal line in the bitmap.
			byte[] pLineBuffer = new byte[bufferSize];

			if(File.Exists(_filename)){
				File.Delete(_filename);
			}//end if

			FileStream stream = new FileStream(_filename, FileMode.Create);
			BinaryWriter writer = new BinaryWriter(stream);

			// Build and write the header.
			// A 32 bit buffer
			byte[] b4 = new byte[4];

			// A 16 bit buffer
			byte[] b2 = new byte[2];

			b2[0] = 0x42; //B
			b2[1] = 0x4D; //M

			writer.Write(b2); //BM Magic number 424D 
			writer.Write(Libnoise.UnpackLittleUint32(destSize + BMP_HEADER_SIZE, ref b4));

			writer.Write(Libnoise.UnpackLittleUint32(0, ref b4));

			writer.Write(Libnoise.UnpackLittleUint32(BMP_HEADER_SIZE, ref b4));
			writer.Write(Libnoise.UnpackLittleUint32(40, ref b4)); // Palette offset
			writer.Write(Libnoise.UnpackLittleUint32(width, ref b4)); // width
			writer.Write(Libnoise.UnpackLittleUint32(height, ref b4)); // height
			writer.Write(Libnoise.UnpackLittleUint16((short)1, ref b2)); // Planes per pixel
			writer.Write(Libnoise.UnpackLittleUint16((short)24, ref b2)); // Bits per plane

			writer.Write(Libnoise.UnpackLittleUint32(0, ref b4)); // Compression (0 = none)

			writer.Write(Libnoise.UnpackLittleUint32(destSize, ref b4));
			writer.Write(Libnoise.UnpackLittleUint32(2834, ref b4)); // X pixels per meter
			writer.Write(Libnoise.UnpackLittleUint32(2834, ref b4)); // Y pixels per meter

			writer.Write(Libnoise.UnpackLittleUint32(0, ref b4));
			writer.Write(b4);

			// Build and write each horizontal line to the file.
			for (int y = 0; y < height; y++){

				int i = 0;

				// Each line is aligned to a 32-bit boundary (\0 padding)
				Array.Clear(pLineBuffer, 0, pLineBuffer.Length);

				Color pSource;

				for (int x = 0; x < width; x++) {

					pSource = _image.GetValue(x, y);

					// Little endian order : B G R
					pLineBuffer[i++] = pSource.Blue;
					pLineBuffer[i++] = pSource.Green;
					pLineBuffer[i++] = pSource.Red;

				}//end for

				writer.Write(pLineBuffer);

			}//end for

			writer.Close();
			stream.Close();

		}//end WriteFile

		#endregion

		#region Internal

		/// <summary>
		/// Calculates the width of one horizontal line in the file, in bytes.
		///
		/// Windows bitmap files require that the width of one horizontal line
		/// must be aligned to a 32-bit boundary.
		/// </summary>
		/// <param name="width">The width of the image, in points</param>
		/// <returns>The width of one horizontal line in the file</returns>
		protected int CalcWidthByteCount(int width) {
			return ((width * 3) + 3) & ~0x03;
		}//end CalcWidthByteCount

		#endregion

	}//end class

}//end namespace

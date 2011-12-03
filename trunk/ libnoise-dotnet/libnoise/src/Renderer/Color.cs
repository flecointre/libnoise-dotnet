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
// 
// From the original Jason Bevins's Libnoise (http://libnoise.sourceforge.net)


using System;

namespace Graphics.Tools.Noise.Renderer {

	/// <summary>
	/// Defines a color.
	///
	/// A color object contains four 8-bit channels: red, green, blue, and an
	/// alpha (transparency) channel.  Channel values range from 0 to 255.
	///
	/// The alpha channel defines the transparency of the color.  If the alpha
	/// channel has a value of 0, the color is completely transparent.  If the
	/// alpha channel has a value of 255, the color is completely opaque.
	/// </summary>
	public class Color :IEquatable<Color>, IColor {

		#region fields

		/// <summary>
		/// Value of the red channel
		/// </summary>
		protected byte _red;

		/// <summary>
		/// Value of the green channel
		/// </summary>
		protected byte _green;

		/// <summary>
		/// Value of the blue channel
		/// </summary>
		protected byte _blue;

		/// <summary>
		/// Value of the alpha (transparency) channel.
		/// </summary>
		protected byte _alpha;

		/// <summary>
		/// Internal hashcode
		/// </summary>
		protected int _hashcode;

		/// <summary>
		/// Static Random generator
		/// </summary>
		private static Random _rnd = new Random(666);

		#endregion

		#region Properties
		/// <summary>
		/// The red channel
		/// </summary>
		public byte Red {
			get { return _red; }
			set { _red = value; }
		}

		/// <summary>
		/// The green channel
		/// </summary>
		public byte Green {
			get { return _green; }
			set { _green = value; }
		}

		/// <summary>
		/// The bllue channel
		/// </summary>
		public byte Blue {
			get { return _blue; }
			set { _blue = value; }
		}

		/// <summary>
		/// The alpha channel
		/// </summary>
		public byte Alpha {
			get { return _alpha; }
			set { _alpha = value; }
		}

		/// <summary>
		/// Create a black color
		/// </summary>
		public static Color BLACK {
			get { return new Color(0, 0, 0, 255); }
		}

		/// <summary>
		/// Create a white color
		/// </summary>
		public static Color WHITE {
			get { return new Color(255, 255, 255, 255); }
		}

		/// <summary>
		/// Create a red color
		/// </summary>
		public static Color RED {
			get { return new Color(255, 0, 0, 255); }
		}

		/// <summary>
		/// Create a green color
		/// </summary>
		public static Color GREEN {
			get { return new Color(0, 255, 0, 255); }
		}

		/// <summary>
		/// Create a blue color
		/// </summary>
		public static Color BLUE {
			get { return new Color(0, 0, 255, 255); }
		}

		/// <summary>
		/// Create a transparent color
		/// </summary>
		public static Color TRANSPARENT {
			get { return new Color(0, 0, 0, 0); }
		}

		#endregion

		#region Ctor/Dtor

		/// <summary>
		/// Create a new Color
		/// </summary>
		/// <param name="r">Value of the red channel</param>
		/// <param name="g">Value of the green channel</param>
		/// <param name="b">Value of the blue channel</param>
		/// <param name="a">Value of the alpha channel</param>
		public Color(byte r, byte g, byte b, byte a) {
			_red = r;
			_green = g;
			_blue = b;
			_alpha = a;
			_hashcode = (_red +_green +_blue) ^_rnd.Next();
		}//end Color

		#endregion

		#region Interface implementation

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Color other) {
			return (
				_red == other.Red 
				&& _green == other.Green 
				&& _blue == other.Blue
				&& _alpha == other.Alpha  
			);
		}//end Equals

		#endregion

		#region Interaction
		/// <summary>
		/// Performs linear interpolation between two 8-bit channel values.
		/// </summary>
		/// <param name="channel0">The first channel</param>
		/// <param name="channel1">The second channel</param>
		/// <param name="alpha">The alpha value</param>
		/// <returns></returns>
		public static Byte BlendChannel(byte channel0, byte channel1, float alpha){
			float c0 = (float)channel0 / 255.0f;
			float c1 = (float)channel1 / 255.0f;
			return (byte)(((c1 * alpha) + (c0 * (1.0f - alpha))) * 255.0f);
		}//end BlendChannel

		/// <summary>
		/// Performs linear interpolation between two colors
		/// </summary>
		/// <param name="color0"></param>
		/// <param name="color1"></param>
		/// <param name="alpha"></param>
		/// <returns></returns>
		public static IColor Lerp(IColor color0, IColor color1, float alpha) {

			return new Color(
				BlendChannel(color0.Red  , color1.Red  , alpha),
				BlendChannel(color0.Green  , color1.Green  , alpha),
				BlendChannel(color0.Blue  , color1.Blue  , alpha),
				BlendChannel(color0.Alpha  , color1.Alpha  , alpha)
			);

		}//end Lerp

		#endregion

		#region Overloading

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return String.Format("Color({0},{1},{2},{3})", Red, Green, Blue, Alpha);
		}//end ToString

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(Object other) {
			if(other is IColor) {
				return Equals((IColor)other);
			}//end if
			else {
				return false;
			}//end else
		}//end Equals

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() {
			return _hashcode;
		}//end Equals

		/// <summary>
		/// Overloading '==' operator:
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator==(Color a, IColor b) {
			return a.Equals(b);
		}//end ==
		
		/// <summary>
		/// Overloading '!=' operator:
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator!=(Color a, IColor b) {
			return !a.Equals(b);
		}//end !=

		#endregion

	}//end Struct

}//end namespace

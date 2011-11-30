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

namespace Graphics.Tools.Noise.Modifier {

	/// <summary>
	/// Noise module that applies a scaling factor and a bias to the output
	/// value from a source module.
	///
	/// The GetValue() method retrieves the output value from the source
	/// module, multiplies it with a scaling factor, adds a bias to it, then
	/// outputs the value.
	///
	/// </summary>
	public class ScaleBias :ModifierModule, IModule3D {

		#region Constants
		/// <summary>
		/// Default scale
		/// noise module.
		/// </summary>
		public const double DEFAULT_SCALE = 1.0;

		/// <summary>
		/// Default bias
		/// noise module.
		/// </summary>
		public const double DEFAULT_BIAS = 0.0;

		#endregion

		#region Fields
		/// <summary>
		/// the scaling factor to apply to the output value from the source module.
		/// </summary>
		protected double _scale = DEFAULT_SCALE;

		/// <summary>
		/// the bias to apply to the scaled output value from the source module.
		/// </summary>
		protected double _bias = DEFAULT_BIAS;

		#endregion

		#region Accessors
		/// <summary>
		/// gets or sets the scale value
		/// </summary>
		public double Scale {
			get { return _scale; }
			set { _scale = value; }
		}

		/// <summary>
		/// gets or sets the bias value
		/// </summary>
		public double Bias {
			get { return _bias; }
			set { _bias = value; }
		}

		#endregion

		#region Ctor/Dtor
		public ScaleBias()
			: base() {
		}//end Exponent

		public ScaleBias(IModule source)
			: base(source) {
		}//end Exponent

		public ScaleBias(IModule source, double scale, double bias)
			: base(source) {
			_scale = scale;
			_bias = bias;
		}//end Exponent

		#endregion

		#region IModule3D Members

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The input coordinate on the x-axis.</param>
		/// <param name="y">The input coordinate on the y-axis.</param>
		/// <param name="z">The input coordinate on the z-axis.</param>
		/// <returns>The resulting output value.</returns>
		public double GetValue(double x, double y, double z) {

			return ((IModule3D)_sourceModule).GetValue(x, y, z) * _scale + _bias;

		}//end GetValue

		#endregion

	}//end class

}//end namespace

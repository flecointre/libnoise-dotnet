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
	/// Noise module that maps the output value from a source module onto an
	/// exponential curve.
	///
	/// Because most noise modules will output values that range from -1.0 to
	/// +1.0, this noise module first normalizes this output value (the range
	/// becomes 0.0 to 1.0), maps that value onto an exponential curve, then
	/// rescales that value back to the original range.
	///
	/// </summary>
	public class Exponent :ModifierModule, IModule3D {

		#region Connstant
		/// <summary>
		/// Default exponent
		/// noise module.
		/// </summary>
		public const double DEFAULT_EXPONENT = 1.0;

		#endregion

		#region Fields
		/// <summary>
		/// Exponent to apply to the output value from the source module.
		/// </summary>
		protected double _exponent = DEFAULT_EXPONENT;

		#endregion

		#region Accessors
		/// <summary>
		/// gets or sets the exponent
		/// </summary>
		public double ExponentValue {
			get { return _exponent; }
			set { _exponent = value; }
		}

		#endregion

		#region Ctor/Dtor
		public Exponent()
			: base() {
		}//end Exponent

		public Exponent(IModule source)
			: base(source) {
		}//end Exponent

		public Exponent(IModule source, double exponent)
			: base(source) {
			_exponent = exponent;
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
			double value = ((IModule3D)_sourceModule).GetValue(x, y, z);
			return (System.Math.Pow(System.Math.Abs((value + 1.0) / 2.0), _exponent) * 2.0 - 1.0);

		}//end GetValue

		#endregion

	}//end class

}//end namespace

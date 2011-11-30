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

namespace Graphics.Tools.Noise.Tranformer {

	/// <summary>
    /// Noise module that rotates the input value around the origin before
    /// returning the output value from a source module.
    ///
    /// The GetValue() method rotates the coordinates of the input value
    /// around the origin before returning the output value from the source
    /// module.  To set the rotation angles, call the SetAngles() method.  To
    /// set the rotation angle around the individual x, y, or z axes,
    /// set the XAngle, YAngle or ZAngle properties,
    /// respectively.
    ///
    /// The coordinate system of the input value is assumed to be
    /// "left-handed" (x increases to the right, y increases upward,
    /// and z increases inward.)
	/// </summary>
	public class RotatePoint :TransformerModule, IModule3D {

		#region Connstant
		/// <summary>
		/// Default x rotation angle for the RotatePoint noise module.
		/// </summary>
		public const double DEFAULT_ROTATE_X = 0.0;

		/// <summary>
		/// Default y rotation angle for the RotatePoint noise module.
		/// </summary>
		public const double DEFAULT_ROTATE_Y = 0.0;

		/// <summary>
		/// Default z rotation angle for the RotatePoint noise module.
		/// </summary>
		public const double DEFAULT_ROTATE_Z = 0.0;

		#endregion

		#region Fields
		/// <summary>
		/// The source input module
		/// </summary>
		protected IModule _sourceModule;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected double _x1Matrix;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected double _x2Matrix;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected double _x3Matrix;

		/// <summary>
		/// x rotation angle applied to the input value, in degrees.
		/// </summary>
		protected  double _xAngle;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected  double _y1Matrix;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected double _y2Matrix;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected double _y3Matrix;

		/// <summary>
		/// y rotation angle applied to the input value, in degrees.
		/// </summary>
		protected  double _yAngle;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected  double _z1Matrix;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected  double _z2Matrix;

		/// <summary>
		/// An entry within the 3x3 rotation matrix used for rotating the
		/// input value.
		/// </summary>
		protected  double _z3Matrix;

		/// <summary>
		/// z rotation angle applied to the input value, in degrees.
		/// </summary>
		protected double _zAngle;

		#endregion

		#region Accessors
		/// <summary>
		/// Gets or sets the source module
		/// </summary>
		public IModule SourceModule {
			get { return _sourceModule; }
			set { _sourceModule = value; }
		}

		/// <summary>
		/// Gets or sets the x rotation angle applied to the input value, in degrees.
		/// </summary>
		public double XAngle {
			get { return _xAngle; }
			set { SetAngles(value, _yAngle, _zAngle); }
		}

		/// <summary>
		/// Gets or sets the y rotation angle applied to the input value, in degrees.
		/// </summary>
		public double YAngle {
			get { return _yAngle; }
			set { SetAngles(_xAngle, value, _zAngle); }
		}

		/// <summary>
		/// Gets or sets the z rotation angle applied to the input value, in degrees.
		/// </summary>
		public double ZAngle {
			get { return _zAngle; }
			set { SetAngles(_xAngle, _yAngle, value); }
		}

		#endregion

		#region Ctor/Dtor
		/// <summary>
		/// Create a new noise module with default values
		/// </summary>
		public RotatePoint() {

		}//end RotatePoint

		/// <summary>
		/// Create a new noise module with given values
		/// </summary>
		/// <param name="source">the source module</param>
		public RotatePoint(IModule source) {
			_sourceModule = source;
		}//end TranslatePoint

		/// <summary>
		/// Create a new noise module with the given values
		/// </summary>
		/// <param name="source">The input source module</param>
		/// <param name="xAngle">the x rotation angle applied to the input value, in degrees.</param>
		/// <param name="yAngle">the y rotation angle applied to the input value, in degrees.</param>
		/// <param name="zAngle">the z rotation angle applied to the input value, in degrees.</param>
		public RotatePoint(IModule source, double xAngle, double yAngle, double zAngle) {
			_sourceModule = source;
			SetAngles(xAngle, yAngle, zAngle);
		}//end RotatePoint

		#endregion

		#region Interaction

		/// <summary>
		/// Sets the rotation angles around all three axes to apply to the
		/// input value.
		/// </summary>
		/// <param name="xAngle">the x rotation angle applied to the input value, in degrees.</param>
		/// <param name="yAngle">the y rotation angle applied to the input value, in degrees.</param>
		/// <param name="zAngle">the z rotation angle applied to the input value, in degrees.</param>
		public void SetAngles(double xAngle, double yAngle, double zAngle) {

			double xCos, yCos, zCos, xSin, ySin, zSin;

			xCos = System.Math.Cos(xAngle * Libnoise.DEG2RAD);
			yCos = System.Math.Cos(yAngle * Libnoise.DEG2RAD);
			zCos = System.Math.Cos(zAngle * Libnoise.DEG2RAD);
			xSin = System.Math.Sin(xAngle * Libnoise.DEG2RAD);
			ySin = System.Math.Sin(yAngle * Libnoise.DEG2RAD);
			zSin = System.Math.Sin(zAngle * Libnoise.DEG2RAD);

			_x1Matrix = ySin * xSin * zSin + yCos * zCos;
			_y1Matrix = xCos * zSin;
			_z1Matrix = ySin * zCos - yCos * xSin * zSin;
			
			_x2Matrix = ySin * xSin * zCos - yCos * zSin;
			_y2Matrix = xCos * zCos;
			_z2Matrix = -yCos * xSin * zCos - ySin * zSin;
			
			_x3Matrix = -ySin * xCos;
			_y3Matrix = xSin;
			_z3Matrix = yCos * xCos;

			_xAngle = xAngle;
			_yAngle = yAngle;
			_zAngle = zAngle;

		}//end public

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

			double nx = (_x1Matrix * x) + (_y1Matrix * y) + (_z1Matrix * z);
			double ny = (_x2Matrix * x) + (_y2Matrix * y) + (_z2Matrix * z);
			double nz = (_x3Matrix * x) + (_y3Matrix * y) + (_z3Matrix * z);

			return ((IModule3D)_sourceModule).GetValue(nx, ny, nz);

		}//end GetValue

		#endregion

	}//end class

}//end namespace

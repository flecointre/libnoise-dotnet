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
using Graphics.Tools.Noise.Model;

namespace Graphics.Tools.Noise.Builder {

	/// <summary>
	/// Builds a planar noise map.
	///
	/// This class builds a noise map by filling it with coherent-noise values
	/// generated from the surface of a plane.
	///
	/// This class describes these input values using (x, z) coordinates.
	/// Their y coordinates are always 0.0.
	///
	/// The application must provide the lower and upper x coordinate bounds
	/// of the noise map, in units, and the lower and upper z coordinate
	/// bounds of the noise map, in units.
	///
	/// To make a tileable noise map with no seams at the edges, use the 
	/// Seamless property.
	/// </summary>
	public class NoiseMapBuilderPlane : NoiseMapBuilder{

		#region Fields

		/// <summary>
		/// A flag specifying whether seamless tiling is enabled.
		/// </summary>
		bool _seamless = false;

		/// <summary>
		/// Lower x boundary of the planar noise map, in units.
		/// </summary>
		double _lowerXBound;

		/// <summary>
		/// Lower z boundary of the planar noise map, in units.
		/// </summary>
		double _lowerZBound;

		/// <summary>
		/// Upper x boundary of the planar noise map, in units.
		/// </summary>
		double _upperXBound;

		/// <summary>
		/// Upper z boundary of the planar noise map, in units.
		/// </summary>
		double _upperZBound;

		#endregion

		#region Accessors

		/// <summary>
		/// Gets or sets a flag specifying whether seamless tiling is enabled.
		/// </summary>
		public bool Seamless {
			get { return _seamless; }
			set { _seamless = value; }
		}

		/// <summary>
		/// Gets the lower x boundary of the planar noise map, in units.
		/// </summary>
		public double LowerXBound {
			get { return _lowerXBound; }
		}

		/// <summary>
		/// Gets the lower z boundary of the planar noise map, in units.
		/// </summary>
		public double LowerZBound {
			get { return _lowerZBound; }
		}

		/// <summary>
		/// Gets the upper x boundary of the planar noise map, in units.
		/// </summary>
		public double UpperXBound {
			get { return _upperXBound; }
		}

		/// <summary>
		/// Gets the upper z boundary of the planar noise map, in units.
		/// </summary>
		public double UpperZBound {
			get { return _upperZBound; }
		}

		#endregion

		#region Ctor/Dtor

		/// <summary>
		/// Default constructor
		/// </summary>
		public NoiseMapBuilderPlane() {
			_seamless = false;
			_lowerXBound = _lowerZBound = _upperXBound = _upperZBound = 0.0;
		}//end NoiseMapBuilder

		/// <summary>
		/// Create a new plane with given value.
		///
		/// @pre The lower x boundary is less than the upper x boundary.
		/// @pre The lower z boundary is less than the upper z boundary.
		///
		/// @throw ArgumentException See the preconditions.
		/// </summary>
		/// <param name="lowerXBound">The lower x boundary of the noise map, in units.</param>
		/// <param name="upperXBound">The upper x boundary of the noise map, in units.</param>
		/// <param name="lowerZBound">The lower z boundary of the noise map, in units.</param>
		/// <param name="upperZBound">The upper z boundary of the noise map, in units.</param>
		/// <param name="seamless">a flag specifying whether seamless tiling is enabled.</param>
		public NoiseMapBuilderPlane(double lowerXBound, double upperXBound, double lowerZBound, double upperZBound, bool seamless) {
			_seamless = seamless;
			SetBounds(lowerXBound, upperXBound, lowerZBound, upperZBound);
		}//end NoiseMapBuilder

		#endregion

		#region Interaction

		/// <summary>
		/// Sets the boundaries of the planar noise map.
		///
		/// @pre The lower x boundary is less than the upper x boundary.
		/// @pre The lower z boundary is less than the upper z boundary.
		///
		/// @throw ArgumentException See the preconditions.
		/// </summary>
		/// <param name="lowerXBound">The lower x boundary of the noise map, in units.</param>
		/// <param name="upperXBound">The upper x boundary of the noise map, in units.</param>
		/// <param name="lowerZBound">The lower z boundary of the noise map, in units.</param>
		/// <param name="upperZBound">The upper z boundary of the noise map, in units.</param>
		public void SetBounds(double lowerXBound, double upperXBound, double lowerZBound, double upperZBound){
			
			if (lowerXBound >= upperXBound || lowerZBound >= upperZBound) {
				throw new ArgumentException("Incoherent bounds : lowerXBound >= upperXBound or lowerZBound >= upperZBound");
			}//end if

			_lowerXBound = lowerXBound;
			_upperXBound = upperXBound;
			_lowerZBound = lowerZBound;
			_upperZBound = upperZBound;

		}//end SetBounds

		/// <summary>
		/// Builds the noise map.
		///
		/// @pre SetBounds() was previously called.
		/// @pre NoiseMap was previously defined.
		/// @pre a SourceModule was previously defined.
		/// @pre The width and height values specified by SetSize() are
		/// positive.
		/// @pre The width and height values specified by SetSize() do not
		/// exceed the maximum possible width and height for the noise map.
		///
		/// @post The original contents of the destination noise map is
		/// destroyed.
		///
		/// @throw noise::ArgumentException See the preconditions.
		///
		/// If this method is successful, the destination noise map contains
		/// the coherent-noise values from the noise module specified by
		/// the SourceModule.
		/// </summary>
		public override void Build() {

			if (_lowerXBound >= _upperXBound || _lowerZBound >= _upperZBound) {
				throw new ArgumentException("Incoherent bounds : lowerXBound >= upperXBound or lowerZBound >= upperZBound");
			}//end if

			if(_width < 0 || _height < 0) {
				throw new ArgumentException("Dimension must be greater or equal 0");
			}//end if

			if(_sourceModule == null){
				throw new ArgumentException("A source module must be provided");
			}//end if

			if(_noiseMap == null){
				throw new ArgumentException("A noise map must be provided");
			}//end if

			// Resize the destination noise map so that it can store the new output
			// values from the source model.
			_noiseMap.SetSize(_width, _height);

			// Create the plane model.
			Plane model = new Plane(_sourceModule);

			double xExtent = _upperXBound - _lowerXBound;
			double zExtent = _upperZBound - _lowerZBound;
			double xDelta  = xExtent / (double)_width ;
			double zDelta  = zExtent / (double)_height;
			double xCur    = _lowerXBound;
			double zCur    = _lowerZBound;

			// Fill every point in the noise map with the output values from the model.
			for (int z = 0; z < _height; z++) {

				xCur = _lowerXBound;

				for (int x = 0; x < _width; x++) {

					float finalValue;

					if (_seamless) {

						double swValue, seValue, nwValue, neValue;
						
						swValue = model.GetValue(xCur, zCur);
						seValue = model.GetValue(xCur + xExtent, zCur);
						nwValue = model.GetValue(xCur, zCur + zExtent);
						neValue = model.GetValue(xCur + xExtent, zCur + zExtent);

						double xBlend = 1.0 - ((xCur - _lowerXBound) / xExtent);
						double zBlend = 1.0 - ((zCur - _lowerZBound) / zExtent);

						double z0 = Libnoise.Lerp(swValue, seValue, xBlend);
						double z1 = Libnoise.Lerp(nwValue, neValue, xBlend);

						finalValue = (float)Libnoise.Lerp(z0, z1, zBlend);
					}//end if
					else {
						finalValue = (float)model.GetValue(xCur, zCur);
					}//end else

					_noiseMap.SetValue(x, z, finalValue);

					xCur += xDelta;

				}//end for

				zCur += zDelta;

				if (_callBack != null) {
					_callBack(z);
				}//end if

			}//end for

		}//end Build

		#endregion

		#region Internal

		#endregion

	}//end class

}//end namespace

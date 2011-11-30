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
//
// The following code is based on Ken Musgrave's explanations and sample
// source code in the book "Texturing and Modelling: A procedural approach"

namespace Graphics.Tools.Noise.Filter {

	/// <summary>
	/// Noise module that outputs 3-dimensional MultiFractal noise.
	///
	/// The multifractal algorithm differs from the Fractal brownian motion in that perturbations are combined 
	/// multiplicatively and introduces an offset parameter. The perturbation at each frequency is computed as 
	/// in the fBM algorithm, but offset is finally added to the value. 
	/// The role of offset is to emphasize the final perturbation value. 
	/// Multiplicative combination of perturbation, in turn, emphasizes the "mountain-like-aspect" of the landscape, 
	/// so that between mountains a sort of slopes are generated
	/// (From http://meshlab.sourceforge.net/wiki/index.php/Fractal_Creation )
	/// </summary>
	public class MultiFractal :FilterModule, IModule3D, IModule2D {

		#region Ctor/Dtor

		/// <summary>
		/// 0-args constructor
		/// </summary>
		public MultiFractal():base(){

		}//end MultiFractal

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

			double signal;
			double value;
			int curOctave;

			x *= _frequency;
			y *= _frequency;
			z *= _frequency;

			// Initialize value
			value = 1.0; // StrandardMultiFractal starts with 1

			// inner loop of spectral construction, where the fractal is built
			for(curOctave = 0; curOctave < _octaveCount; curOctave++) {

				// Get the coherent-noise value.
				signal = _offset + (_source3D.GetValue(x, y, z) * _spectralWeights[curOctave]);

				// Add the signal to the output value.
				value *= signal;

				// Go to the next octave.
				x *= _lacunarity;
				y *= _lacunarity;
				z *= _lacunarity;

			}//end for

			//take care of remainder in _octaveCount
			double remainder = _octaveCount - (int)_octaveCount;

			if(remainder > 0) {
				value += remainder * _source2D.GetValue(x, y) * _spectralWeights[curOctave];
			}//end if

			return value;

		}//end GetValue

		#endregion

		#region IModule2D Members

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The input coordinate on the x-axis.</param>
		/// <param name="y">The input coordinate on the y-axis.</param>
		/// <returns>The resulting output value.</returns>
		public double GetValue(double x, double y) {

			double signal;
			double value;
			int curOctave;

			x *= _frequency;
			y *= _frequency;

			// Initialize value
			value = 1.0; // StrandardMultiFractal starts with 1

			// inner loop of spectral construction, where the fractal is built
			for(curOctave = 0; curOctave < _octaveCount; curOctave++) {

				// Get the coherent-noise value.
				signal = _offset + (_source2D.GetValue(x, y) * _spectralWeights[curOctave]);

				// Add the signal to the output value.
				value *= signal;

				// Go to the next octave.
				x *= _lacunarity;
				y *= _lacunarity;

			}//end for

			//take care of remainder in _octaveCount
			double remainder = _octaveCount - (int)_octaveCount;

			if(remainder > 0) {
				value += remainder * _source2D.GetValue(x, y) * _spectralWeights[curOctave];
			}//end if

			return value;

		}//end GetValue

		#endregion

	}//end class

}//end namespace

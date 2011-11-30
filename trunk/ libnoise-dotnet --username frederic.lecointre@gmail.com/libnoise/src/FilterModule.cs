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

namespace Graphics.Tools.Noise {

	/// <summary>
	/// Base class for all filter module
	/// 
	/// Provides some commons or usefull properties and constants
	/// </summary>
	abstract public class FilterModule :IModule {

		#region Constants

		/// <summary>
		/// Default frequency for the noise module.
		/// </summary>
		public const double DEFAULT_FREQUENCY = 1.0;

		/// <summary>
		/// Default lacunarity for the noise module.
		/// </summary>
		public const double DEFAULT_LACUNARITY = 2.0;

		/// <summary>
		/// Default number of octaves for the noise module.
		/// </summary>
		public const double DEFAULT_OCTAVE_COUNT = 6;

		/// <summary>
		/// Maximum number of octaves for a noise module.
		/// </summary>
		public const int MAX_OCTAVE = 30;

		/// <summary>
		/// Default offset
		/// </summary>
		public const double DEFAULT_OFFSET = 1.0;

		/// <summary>
		/// Default gain
		/// </summary>
		public const double DEFAULT_GAIN = 2.0;

		/// <summary>
		/// Default spectral exponent
		/// </summary>
		public const double DEFAULT_SPECTRAL_EXPONENT = 0.9;

		#endregion

		#region Fields

		/// <summary>
		/// Frequency of the first octave
		/// </summary>
		protected double _frequency = DEFAULT_FREQUENCY;

		/// <summary>
		/// The lacunarity specifies the frequency multipler between successive
		/// octaves.
		///
		/// The effect of modifying the lacunarity is subtle; you may need to play
		/// with the lacunarity value to determine the effects.  For best results,
		/// set the lacunarity to a number between 1.5 and 3.5.
		/// </summary>
		protected double _lacunarity = DEFAULT_LACUNARITY;

		/// <summary>
		/// The number of octaves control the <i>amount of detail</i> of the
		/// noise.  Adding more octaves increases the detail of the 
		/// noise, but with the drawback of increasing the calculation time.
		///
		/// An octave is one of the coherent-noise functions in a series of
		/// coherent-noise functions that are added together to form noise.
		///
		/// </summary>
		protected double _octaveCount = DEFAULT_OCTAVE_COUNT;

		/// <summary>
		/// 
		/// </summary>
		protected double[] _spectralWeights = new double[MAX_OCTAVE];

		/// <summary>
		/// 
		/// </summary>
		protected double _offset = DEFAULT_OFFSET;

		/// <summary>
		/// 
		/// </summary>
		protected double _gain = DEFAULT_GAIN;

		/// <summary>
		/// 
		/// </summary>
		protected double _spectralExponent = DEFAULT_SPECTRAL_EXPONENT;

		/// <summary>
		/// 
		/// </summary>
		protected IModule4D _source4D;
		/// <summary>
		/// 
		/// </summary>
		protected IModule3D _source3D;
		/// <summary>
		/// 
		/// </summary>
		protected IModule2D _source2D;
		/// <summary>
		/// 
		/// </summary>
		protected IModule1D _source1D;

		#endregion

		#region Accessors

		/// <summary>
		/// Gets or sets the frequency
		/// </summary>
		public double Frequency {
			get { return _frequency; }
			set { _frequency = value; }
		}//end

		/// <summary>
		/// Gets or sets the lacunarity
		/// </summary>
		public double Lacunarity {
			get { return _lacunarity; }
			set { 
				_lacunarity = value; 
				ComputeSpectralWeights(); 
			}
		}//end

		/// <summary>
		/// Gets or sets the number of octaves 
		/// </summary>
		public double OctaveCount {
			get { return _octaveCount; }
			set { _octaveCount = (double)Libnoise.Clamp(value, 1, MAX_OCTAVE); }
		}//end

		/// <summary>
		/// Gets or sets the offset
		/// </summary>
		public double Offset {
			get { return _offset; }
			set { _offset = value; }
		}//end Offset

		/// <summary>
		/// Gets or sets the gain
		/// </summary>
		public double Gain {
			get { return _gain; }
			set { _gain = value; }
		}//end Gain

		/// <summary>
		/// Gets or sets the spectralExponent
		/// </summary>
		public double SpectralExponent {
			get { return _spectralExponent; }
			set {
				_spectralExponent = value;
				ComputeSpectralWeights();
			}
		}//end Seed

		/// <summary>
		/// Gets or sets the primitive 4D
		/// </summary>
		public IModule4D Primitive4D {
			get { return _source4D; }
			set { _source4D = value; }
		}//end Primitive4D

		/// <summary>
		/// Gets or sets the primitive 3D
		/// </summary>
		public IModule3D Primitive3D {
			get { return _source3D; }
			set { _source3D = value; }
		}//end Primitive3D

		/// <summary>
		/// Gets or sets the primitive 2D
		/// </summary>
		public IModule2D Primitive2D {
			get { return _source2D; }
			set { _source2D = value; }
		}//end Primitive2D

		/// <summary>
		/// Gets or sets the primitive 1D
		/// </summary>
		public IModule1D Primitive1D {
			get { return _source1D; }
			set { _source1D = value; }
		}//end Primitive1D

		#endregion


		#region Ctor/Dtor

		/// <summary>
		/// Template default constructor
		/// </summary>
		protected  FilterModule() 
			:this(DEFAULT_FREQUENCY, DEFAULT_LACUNARITY, DEFAULT_SPECTRAL_EXPONENT, DEFAULT_OCTAVE_COUNT){

		}//end FilterModule

		/// <summary>
		/// Template constructor
		/// </summary>
		/// <param name="frequency"></param>
		/// <param name="lacunarity"></param>
		/// <param name="exponent"></param>
		/// <param name="octaveCount"></param>
		protected FilterModule(double frequency, double lacunarity, double exponent, double octaveCount) {

			_frequency = frequency;
			_lacunarity = lacunarity;
			_spectralExponent = exponent;
			_octaveCount = octaveCount;

			ComputeSpectralWeights();

		}//end FilterModule

		#endregion

		#region Internal

		/// <summary>
		/// Calculates the spectral weights for each octave.
		/// </summary>
		protected void ComputeSpectralWeights() {

			// Compute weight for each frequency.
			for(int i = 0; i < MAX_OCTAVE; i++) {

				// determines how "heavy" is the i-th octave
				_spectralWeights[i] = System.Math.Pow(_lacunarity, -i * _spectralExponent);

			}//end for

			/*
			double frequency = 1.0;

			// Compute weight for each frequency.
			for(int i = 0; i < MAX_OCTAVE; i++) {

				// determines how "heavy" is the i-th octave
				_spectralWeights[i] = System.Math.Pow(frequency, -_spectralExponent);
				
				// calculates the next octave frequency
				frequency *= _lacunarity;

			}//end for
			*/
		}//end ComputeSpectralWeights

		#endregion

	}//end class

}//end namespace

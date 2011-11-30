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

using Graphics.Tools.Noise;

namespace Graphics.Tools.Noise.Renderer {

	/// <summary>
	/// Abstract base class for a renderer
	/// </summary>
	abstract public class AbstractRenderer {

		#region Fields
		/// <summary>
		/// The source noise map that contains the coherent-noise values.
		/// </summary>
		protected IMap2D<float> _noiseMap;

		/// <summary>
		/// The destination image
		/// </summary>
		protected IMap2D<Color> _image;

		#endregion

		#region Accessors

		/// <summary>
		/// Gets or sets the source noise map
		/// </summary>
		public IMap2D<float> NoiseMap {
			get { return _noiseMap; }
			set { _noiseMap = value; }
		}

		/// <summary>
		/// Gets or sets the destination image
		/// </summary>
		public IMap2D<Color> Image {
			get { return _image; }
			set { _image = value; }
		}

		#endregion


		#region Interaction

		/// <summary>
		/// Renders the destination image using the contents of the source
		/// noise map.
		///
		/// @pre NoiseMap has been defined.
		/// @pre Image has been defined.
		///
		/// @post The original contents of the destination image is destroyed.
		///
		/// @throw ArgumentException See the preconditions.
		/// </summary>
		abstract public void Render();

		#endregion

	}//end class

}//end namespace

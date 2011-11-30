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

namespace Graphics.Tools.Noise.Filter {

	/// <summary>
	/// Noise module that outputs Voronoi cells.
	///
	///
	/// In mathematics, a <i>Voronoi cell</i> is a region containing all the
	/// points that are closer to a specific <i>seed point</i> than to any
	/// other seed point.  These cells mesh with one another, producing
	/// polygon-like formations.
	///
	/// By default, this noise module randomly places a seed point within
	/// each unit cube.  By modifying the <i>frequency</i> of the seed points,
	/// an application can change the distance between seed points.  The
	/// higher the frequency, the closer together this noise module places
	/// the seed points, which reduces the size of the cells.
	///
	/// This noise module assigns each Voronoi cell with a random constant
	/// value from a coherent-noise function.  The <i>displacement value</i>
	/// controls the range of random values to assign to each cell.  The
	/// range of random values is +/- the displacement value.
	/// The frequency determines the size of the Voronoi cells and the
	/// distance between these cells.
	/// 
	/// To modify the random positions of the seed points, call the SetSeed()
	/// method.
	///
	/// This noise module can optionally add the distance from the nearest
	/// seed to the output value.  To enable this feature, call the
	/// EnableDistance() method.  This causes the points in the Voronoi cells
	/// to increase in value the further away that point is from the nearest
	/// seed point.
	///
	/// Voronoi cells are often used to generate cracked-mud terrain
	/// formations or crystal-like textures
	/// </summary>
	public class Voronoi :FilterModule, IModule3D {

		#region Constants

		/// <summary>
		/// Default persistence value for the Voronoi noise module.
		/// </summary>
		public const double DEFAULT_DISPLACEMENT = 1.0;

		#endregion

		#region Fields

		/// <summary>
		/// This noise module assigns each Voronoi cell with a random constant
		/// value from a coherent-noise function.  The <i>displacement
		/// value</i> controls the range of random values to assign to each
		/// cell.  The range of random values is +/- the displacement value.
		/// </summary>
		protected double _displacement = DEFAULT_DISPLACEMENT;

		/// <summary>
		/// Applying the distance from the nearest seed point to the output
		/// value causes the points in the Voronoi cells to increase in value
		/// the further away that point is from the nearest seed point.
		/// </summary>
		protected bool _distance = false;

		#endregion

		#region Accessors

		/// <summary>
		/// Gets or sets the displacement
		/// </summary>
		public double Displacement {
			get { return _displacement; }
			set { _displacement = value; }
		}//end

		/// <summary>
		/// 
		/// </summary>
		public bool Distance {
			get { return _distance; }
			set { _distance = value; }
		}//end Seed

		#endregion

		#region Ctor/Dtor

		/// <summary>
		/// Create new Voronoi generator with default values
		/// </summary>
		public Voronoi() {

		}//end Voronoi

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

			//TODO This method could be more efficient by caching the seed values.
			x *= _frequency;
			y *= _frequency;
			z *= _frequency;

			int xInt = (x > 0.0? (int)x: (int)x - 1);
			int yInt = (y > 0.0? (int)y: (int)y - 1);
			int zInt = (z > 0.0? (int)z: (int)z - 1);

			double minDist = 2147483647.0;
			double xCandidate = 0;
			double yCandidate = 0;
			double zCandidate = 0;

			// Inside each unit cube, there is a seed point at a random position.  Go
			// through each of the nearby cubes until we find a cube with a seed point
			// that is closest to the specified position.
			for(int zCur = zInt - 2; zCur <= zInt + 2; zCur++) {
				for(int yCur = yInt - 2; yCur <= yInt + 2; yCur++) {
					for(int xCur = xInt - 2; xCur <= xInt + 2; xCur++) {

						// Calculate the position and distance to the seed point inside of
						// this unit cube.
						double xPos = xCur + _source3D.GetValue(xCur, yCur, zCur);
						double yPos = yCur + _source3D.GetValue(xCur, yCur, zCur);
						double zPos = zCur + _source3D.GetValue(xCur, yCur, zCur);

						double xDist = xPos - x;
						double yDist = yPos - y;
						double zDist = zPos - z;
						double dist = xDist * xDist + yDist * yDist + zDist * zDist;

						if(dist < minDist) {
							// This seed point is closer to any others found so far, so record
							// this seed point.
							minDist = dist;
							xCandidate = xPos;
							yCandidate = yPos;
							zCandidate = zPos;
						}//end if
					}//end for
				}//end for
			}//end for

			double value;

			if(_distance) {
				// Determine the distance to the nearest seed point.
				double xDist = xCandidate - x;
				double yDist = yCandidate - y;
				double zDist = zCandidate - z;
				value = (System.Math.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist)
				  ) * Libnoise.SQRT_3 - 1.0;
			}//end if
			else {
				value = 0.0;
			}//end else

			// Return the calculated distance with the displacement value applied.
			return value + (_displacement * (double)_source3D.GetValue(
				(int)(System.Math.Floor(xCandidate)),
				(int)(System.Math.Floor(yCandidate)),
				(int)(System.Math.Floor(zCandidate)))
			);

		}//end GetValue

		#endregion

	}//end class

}//end namespace

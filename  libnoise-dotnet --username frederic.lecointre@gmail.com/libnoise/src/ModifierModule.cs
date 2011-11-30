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

namespace Graphics.Tools.Noise {

	/// <summary>
	/// Base class for source module modifiers
	/// </summary>
	abstract public class ModifierModule :IModule {

		#region Fields
		/// <summary>
		/// The source input module
		/// </summary>
		protected IModule _sourceModule;

		#endregion

		#region Accessors

		/// <summary>
		/// Gets or sets the source module
		/// </summary>
		public IModule SourceModule {
			get { return _sourceModule; }
			set { _sourceModule = value; }
		}

		#endregion

		#region Ctor/Dtor
		public ModifierModule() {
		}//end CombinerModule

		public ModifierModule(IModule source) {
			_sourceModule = source;
		}//end CombinerModule

		#endregion

	}//end class

}//end namespace

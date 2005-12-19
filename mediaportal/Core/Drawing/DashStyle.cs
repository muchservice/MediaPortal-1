#region Copyright (C) 2005 Team MediaPortal

/* 
 *	Copyright (C) 2005 Team MediaPortal
 *	http://www.team-mediaportal.com
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#endregion

using System;

namespace MediaPortal.Drawing
{
	public class DashStyle
	{
		#region Constructors

		public DashStyle()
		{
			_dashes = new double[] { 0 };
		}

		public DashStyle(double[] dashes, double offset)
		{
			_dashes = dashes;
			_offset = offset;
		}

		#endregion Constructors

		#region Properties

		public double[] Dashes
		{
			get { return _dashes; }
		}

		public double Offset
		{
			get { return _offset; }
		}

		#endregion Properties

		#region Fields

		double[]					_dashes;
		double						_offset;

		#endregion Fields
	}
}

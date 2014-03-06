using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QUT.Gppg;

namespace MiniJava.Frontend
{
	public struct Location : IMerge<Location>, IComparable<Location>
	{
		public int StartLine { get; private set; }
		public int StartColumn { get; private set; }
		public int EndLine { get; private set; }
		public int EndColumn { get; private set; }
		public int StartIndex { get; private set; }
		public int EndIndex { get; private set; }

		public Location(int startLine, int startCol, int endLine, int endCol,
			int startIndex, int endIndex) : this()
		{
			StartLine = startLine;
			StartColumn = startCol;
			EndLine = endLine;
			EndColumn = endCol;
			StartIndex = startIndex;
			EndIndex = endIndex;
		}
		public Location(int sl, int sc, int el, int ec) : this(sl, sc, el, ec, -1, -1) { }
		public override string ToString()
		{
			string lineFormat, colFormat;
			if (StartLine == EndLine)
				lineFormat = "{0}";
			else
				lineFormat = "{0}-{1}";

			if (EndColumn < 0)
				colFormat = "";
			else if (StartColumn == EndColumn)
				colFormat = ":{2}";
			else
				colFormat = ":{2}-{3}";
			return String.Format(lineFormat + colFormat, StartLine, EndLine, StartColumn, EndColumn);
		}
		public Location Merge(Location last)
		{
			return new Location(StartLine, StartColumn,
				last.EndLine, last.EndColumn,
				StartIndex, last.EndIndex);
				
		}
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Location))
				return false;
			Location rhs = (Location)obj;
			return StartColumn == rhs.StartColumn &&
				EndColumn == rhs.EndColumn &&
				StartLine == rhs.StartLine &&
				EndLine == rhs.EndLine;
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		public int CompareTo(Location other)
		{
			if (StartIndex != other.StartIndex)
				return StartIndex.CompareTo(other.StartIndex);
			return EndIndex.CompareTo(other.EndIndex);
		}
	}
}

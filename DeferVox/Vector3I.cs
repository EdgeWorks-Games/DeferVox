using OpenTK;

namespace DeferVox
{
	public struct Vector3I
	{
		private readonly int _x;
		private readonly int _y;
		private readonly int _z;

		public Vector3I(int xPos, int yPos, int zPos)
		{
			_x = xPos;
			_y = yPos;
			_z = zPos;
		}

		public int X
		{
			get { return _x; }
		}

		public int Y
		{
			get { return _y; }
		}

		public int Z
		{
			get { return _z; }
		}

		public static Vector3I operator *(Vector3I vector, int scale)
		{
			return Multiply(vector, scale);
		}

		public static Vector3I Multiply(Vector3I vector, int scale)
		{
			return new Vector3I(vector._x*scale, vector._y*scale, vector._z*scale);
		}

		public Vector3 ToVector3()
		{
			return new Vector3(_x, _y, _z);
		}

		#region Equality Functions and Operators

		public override int GetHashCode()
		{
			return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vector3I))
				return false;

			return Equals((Vector3I) obj);
		}

		private bool Equals(Vector3I other)
		{
			if (_x != other._x)
				return false;
			if (_y != other._y)
				return false;
			return _z == other._z;
		}

		public static bool operator ==(Vector3I left, Vector3I right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Vector3I left, Vector3I right)
		{
			return !left.Equals(right);
		}

		#endregion
	}
}
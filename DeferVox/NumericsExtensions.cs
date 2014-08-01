using System.Numerics;
using OpenTK;

namespace DeferVox
{
// ReSharper disable InconsistentNaming
	public static class NumericsExtensions
	{
		public static Vector3 ToVector3(this Vector3f vector)
		{
			return new Vector3(vector.X, vector.Y, vector.Z);
		}
	}

	internal struct Vector3i
	{
		public int X;
		public int Y;
		public int Z;

		public Vector3i(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Vector3i operator *(Vector3i vec, int scale)
		{
			vec.X *= scale;
			vec.Y *= scale;
			vec.Z *= scale;
			return vec;
		}

		public Vector3 ToVector3()
		{
			return new Vector3(X, Y, Z);
		}
	}

// ReSharper restore InconsistentNaming
}
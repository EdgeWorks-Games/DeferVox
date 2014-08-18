using System.Numerics;
using OpenTK;

namespace DeferVox
{
	public static class NumericsExtensions
	{
		public static Vector3 ToVector3(this Vector3f vector)
		{
			return new Vector3(vector.X, vector.Y, vector.Z);
		}
	}
}
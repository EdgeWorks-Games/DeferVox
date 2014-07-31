using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;

namespace DeferVox.Graphics
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PositionColorVertex
	{
		public readonly Vector3 Position;
		public readonly Vector3 Color;

		public PositionColorVertex(float x, float y, float z, Color color)
		{
			Position = new Vector3(x, y, z);
			Color = new Vector3(color.R/255f, color.G/255f, color.B/255f);
		}

		public static readonly int SizeInBytes = Marshal.SizeOf(new PositionColorVertex());
	}
}
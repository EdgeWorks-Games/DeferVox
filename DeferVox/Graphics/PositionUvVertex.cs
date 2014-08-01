using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Graphics
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PositionUvVertex
	{
		public readonly Vector3 Position;
		public readonly Vector2 UV;

		public PositionUvVertex(float x, float y, float z, float uvX, float uvY)
		{
			Position = new Vector3(x, y, z);
			UV = new Vector2(uvX, uvY);
		}

		public static readonly int SizeInBytes = Marshal.SizeOf(new PositionUvVertex());

		public static void SetVertexAttribPointers()
		{
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer( // Vertices
				0, // attribute layout #0
				Vector3.SizeInBytes, // size
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				SizeInBytes, // offset between values
				0); // start offset

			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer( // Colors
				1, // attribute layout #1
				Vector2.SizeInBytes, // size
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				SizeInBytes, // offset between values
				Vector3.SizeInBytes); // start offset
		}

		public static void ClearVertexAttribPointers()
		{
			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);
		}
	}
}
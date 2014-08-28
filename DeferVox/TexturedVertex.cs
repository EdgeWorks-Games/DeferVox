using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TexturedVertex
	{
		private readonly Vector3 _position;
		private readonly Vector2 _UV;

		public TexturedVertex(float x, float y, float z, float u, float v)
		{
			_position = new Vector3(x, y, z);
			_UV = new Vector2(u, v);
		}

		public static readonly int SizeInBytes = Marshal.SizeOf(new TexturedVertex());

		public static void SetVertexAttribPointers()
		{
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer( // Vertices
				0, // attribute layout #0
				3, // size in type
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				SizeInBytes, // offset between start of vertex values (0 = tightly packed)
				0); // start offset

			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer( // Colors
				1, // attribute layout #1
				2, // size in type
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				SizeInBytes, // offset between start of vertex values (0 = tightly packed)
				Vector3.SizeInBytes); // start offset
		}

		public static void ClearVertexAttribPointers()
		{
			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);
		}

		#region Equality Functions and Operators

		public override int GetHashCode()
		{
			return _position.GetHashCode() ^ _UV.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is TexturedVertex))
				return false;

			return Equals((TexturedVertex)obj);
		}

		private bool Equals(TexturedVertex other)
		{
			if (_position != other._position)
				return false;
			return _UV == other._UV;
		}

		public static bool operator ==(TexturedVertex left, TexturedVertex right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TexturedVertex left, TexturedVertex right)
		{
			return !left.Equals(right);
		}

		#endregion
	}
}
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PositionUvVertex
	{
		private readonly Vector3 _position;
		private readonly Vector2 _UV;

		public PositionUvVertex(float x, float y, float z, float u, float v)
		{
			_position = new Vector3(x, y, z);
			_UV = new Vector2(u, v);
		}

		public static readonly int SizeInBytes = Vector3.SizeInBytes + Vector2.SizeInBytes;

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

		#region Equality Functions and Operators

		public override int GetHashCode()
		{
			return _position.GetHashCode() ^ _UV.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PositionUvVertex))
				return false;

			return Equals((PositionUvVertex)obj);
		}

		private bool Equals(PositionUvVertex other)
		{
			if (_position != other._position)
				return false;
			return _UV == other._UV;
		}

		public static bool operator ==(PositionUvVertex left, PositionUvVertex right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(PositionUvVertex left, PositionUvVertex right)
		{
			return !left.Equals(right);
		}

		#endregion
	}
}
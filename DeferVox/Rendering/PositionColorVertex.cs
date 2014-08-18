using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PositionColorVertex
	{
		private readonly Vector3 _position;
		private readonly Vector3 _color;

		public PositionColorVertex(float x, float y, float z, Color color)
		{
			_position = new Vector3(x, y, z);
			_color = new Vector3(color.R/255f, color.G/255f, color.B/255f);
		}

		public static readonly int SizeInBytes = Marshal.SizeOf(new PositionColorVertex());

		public static void SetVertexAttribPointers()
		{
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer( // Vertices
				0, // attribute layout #0
				3, // size
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				SizeInBytes, // offset between values
				0); // start offset

			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer( // Colors
				1, // attribute layout #1
				3, // size
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
			return _position.GetHashCode() ^ _color.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PositionColorVertex))
				return false;

			return Equals((PositionColorVertex) obj);
		}

		private bool Equals(PositionColorVertex other)
		{
			if (_position != other._position)
				return false;
			return _color == other._color;
		}

		public static bool operator ==(PositionColorVertex left, PositionColorVertex right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(PositionColorVertex left, PositionColorVertex right)
		{
			return !left.Equals(right);
		}

		#endregion
	}
}
﻿using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

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
	}
}
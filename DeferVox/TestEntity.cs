using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct PositionColorVertex
	{
		public readonly Vector3 Position;
		public readonly Vector3 Color;

		public PositionColorVertex(float x, float y, float z, Color color)
		{
			Position = new Vector3(x, y, z);
			Color = new Vector3(color.R / 255f, color.G / 255f, color.B / 255f);
		}

		public static readonly int SizeInBytes = Marshal.SizeOf(new PositionColorVertex());
	}

	public sealed class TestEntity : IEntity
	{
		private static readonly PositionColorVertex[] TestBufferData =
		{
			// Front
			new PositionColorVertex(-1f, -1f, 0f, Color.Red), // Left Bottom
			new PositionColorVertex(1f, -1f, 0f, Color.FromArgb(0,255,0)), // Right Bottom
			new PositionColorVertex(-1f, 1f, 0f, Color.Blue), // Left Top

			new PositionColorVertex(1f, -1f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.Blue), // Right Top
			new PositionColorVertex(-1f, 1f, 0f, Color.Blue), // Left Top

			// Back
			new PositionColorVertex(1f, -1f, 0f, Color.Blue), // Left Bottom 
			new PositionColorVertex(-1f, -1f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.Blue), // Left Top

			new PositionColorVertex(-1f, -1f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(-1f, 1f, 0f, Color.Blue), // Right Top
			new PositionColorVertex(1f, 1f, 0f, Color.Blue) // Left Top
		};

		private readonly int _arrayBufferId;
		private readonly ShaderProgram _shaderProgram;
		private float _rotation;

		public TestEntity()
		{
			// Create a new shader to use
			_shaderProgram = new ShaderProgram(
				File.ReadAllText("Shaders/basic.vert.glsl"),
				File.ReadAllText("Shaders/test.frag.glsl"));

			// Set up the vertex buffer
			_arrayBufferId = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _arrayBufferId);
			GL.BufferData(
				BufferTarget.ArrayBuffer,
				new IntPtr(PositionColorVertex.SizeInBytes*TestBufferData.Length),
				TestBufferData,
				BufferUsageHint.StaticDraw);
		}

		public void Update(TimeSpan delta)
		{
			_rotation += 2f*(float) delta.TotalSeconds;
		}

		public void Render()
		{
			_shaderProgram.Use();

			var projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(100), 1280f/720f, 0.1f, 100f);
			var view = Matrix4.CreateTranslation(0, 0, -4);
			var model = Matrix4.Identity*Matrix4.CreateRotationY(_rotation);
			_shaderProgram.MvpMatrix = model*view*projection;

			// Set information about the data we're going to draw
			GL.BindBuffer(BufferTarget.ArrayBuffer, _arrayBufferId);

			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer( // Vertices
				0, // attribute layout #0
				3, // size
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				PositionColorVertex.SizeInBytes, // offset between values
				0); // start offset

			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer( // Colors
				1, // attribute layout #1
				3, // size
				VertexAttribPointerType.Float, // type
				false, // normalize this attribute?
				PositionColorVertex.SizeInBytes, // offset between values
				Vector3.SizeInBytes); // start offset

			// Actually draw the triangle
			GL.DrawArrays(PrimitiveType.Triangles, 0, 12);

			// Clean up
			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);
		}

		public void Dispose()
		{
			_shaderProgram.Dispose();
		}
	}
}
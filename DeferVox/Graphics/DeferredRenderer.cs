using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Graphics
{
	public sealed class DeferredRenderer : IRenderer
	{
		private readonly Matrix4 _projection;
		private readonly Size _resolution;
		private readonly ShaderProgram _shaderProgram;
		private Matrix4 _pvMatrix;

		public DeferredRenderer(Size resolution)
		{
			_resolution = resolution;

			// Create a new shader to use
			_shaderProgram = new ShaderProgram(
				File.ReadAllText("Shaders/test.vert.glsl"),
				File.ReadAllText("Shaders/test.frag.glsl"));

			_projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60), 1280f/720f, 0.1f, 100f);
		}

		public void Render(GameScene scene)
		{
			// Set up OpenGL settings
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);
			// For some reason, it's culling the exact opposite faces so I flipped it, the correct order IS counter-clockwise
			GL.FrontFace(FrontFaceDirection.Cw);
			GL.Viewport(0, 0, _resolution.Width, _resolution.Height);

			// Clear the default buffer
			GL.ClearColor(Color.CornflowerBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			// Multiply the projection and view in advance
			var view =
				Matrix4.CreateTranslation(5, -5, -5)*
				Matrix4.CreateRotationY(MathHelper.DegreesToRadians(40))*
				Matrix4.CreateRotationX(MathHelper.DegreesToRadians(40));
			_pvMatrix = view*_projection;

			// Render the currently active scene
			scene.Entities.ForEach(e => e.Render(this));
		}

		public void RenderStreamedMesh(Vector3 position, Vector3 rotation, PositionColorVertex[] meshData)
		{
			// Send the mesh data to the GPU in a vertex buffer
			var arrayBufferId = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, arrayBufferId);
			GL.BufferData(
				BufferTarget.ArrayBuffer,
				new IntPtr(PositionColorVertex.SizeInBytes*meshData.Length),
				meshData,
				BufferUsageHint.StreamDraw);

			// Set the shader settings
			_shaderProgram.Use();
			var model =
				Matrix4.CreateRotationX(rotation.X)*
				Matrix4.CreateRotationY(rotation.Y)*
				Matrix4.CreateRotationZ(rotation.Z)*
				Matrix4.CreateTranslation(position);
			_shaderProgram.MvpMatrix = model*_pvMatrix;

			// Set information about the data we're going to draw
			GL.BindBuffer(BufferTarget.ArrayBuffer, arrayBufferId);

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
			GL.DrawArrays(PrimitiveType.Triangles, 0, meshData.Length);

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
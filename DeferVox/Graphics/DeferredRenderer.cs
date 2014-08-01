using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Graphics
{
	public sealed class DeferredRenderer : IRenderer
	{
		private readonly ShaderProgram _colorShaderProgram;
		private readonly ShaderProgram _textureShaderProgram;
		private readonly Matrix4 _projection;
		private readonly Size _resolution;
		private Matrix4 _pvMatrix;
		private float _rot;

		public DeferredRenderer(Size resolution)
		{
			_resolution = resolution;

			// Create a new shader to use
			_colorShaderProgram = new ShaderProgram(
				File.ReadAllText("Shaders/color.vert.glsl"),
				File.ReadAllText("Shaders/color.frag.glsl"));
			_textureShaderProgram = new ShaderProgram(
				File.ReadAllText("Shaders/texture.vert.glsl"),
				File.ReadAllText("Shaders/texture.frag.glsl"));

			_projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70), 1280f/720f, 0.1f, 100f);
		}

		public void RenderScene(GameScene scene)
		{
			// Set up OpenGL settings
			GL.Disable(EnableCap.Blend);
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
			_rot += 0.01f;
			var view =
				Matrix4.CreateRotationY(_rot)*
				Matrix4.CreateTranslation(0, -5, -5)*
				Matrix4.CreateRotationX(MathHelper.DegreesToRadians(40));
			_pvMatrix = view*_projection;

			// Render the currently active scene
			scene.Entities.ForEach(e => e.Render(this));
		}

		public void RenderStreamedMesh(Vector3f position, Vector3f rotation, PositionColorVertex[] meshData)
		{
			InternalRenderStreamedMesh(
				position, rotation,
				meshData, PositionColorVertex.SizeInBytes * meshData.Length,
				PositionColorVertex.SetVertexAttribPointers, PositionColorVertex.ClearVertexAttribPointers);
		}

		public void RenderStreamedMesh(Vector3f position, Vector3f rotation, PositionUvVertex[] meshData)
		{
			InternalRenderStreamedMesh(
				position, rotation,
				meshData, PositionUvVertex.SizeInBytes * meshData.Length,
				PositionUvVertex.SetVertexAttribPointers, PositionUvVertex.ClearVertexAttribPointers);
		}

		private void InternalRenderStreamedMesh<T>(
			Vector3f position, Vector3f rotation,
			T[] meshData, int meshSizeInBytes,
			Action setPointers, Action clearPointers)
			where T : struct
		{
			// Set the shader settings
			_colorShaderProgram.Use();
			var model =
				Matrix4.CreateRotationX(rotation.X) *
				Matrix4.CreateRotationY(rotation.Y) *
				Matrix4.CreateRotationZ(rotation.Z) *
				Matrix4.CreateTranslation(position.ToVector3());
			_colorShaderProgram.MvpMatrix = model * _pvMatrix;

			// Set information about the data we're going to draw
			var arrayBufferId = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, arrayBufferId);
			GL.BufferData(
				BufferTarget.ArrayBuffer,
				new IntPtr(meshSizeInBytes),
				meshData,
				BufferUsageHint.StreamDraw);
			setPointers();

			// Actually draw the triangles
			GL.DrawArrays(PrimitiveType.Triangles, 0, meshData.Length);

			// Clean up
			clearPointers();
			GL.DeleteBuffer(arrayBufferId);
		}

		public void Dispose()
		{
			_colorShaderProgram.Dispose();
			_textureShaderProgram.Dispose();
		}
	}
}
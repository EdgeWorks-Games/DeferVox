using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering
{
	public sealed class DeferredRenderer : IDisposable
	{
		private readonly ShaderProgram _colorShaderProgram;
		private readonly ShaderProgram _textureShaderProgram;
		private readonly Texture2D _grassTexture;

		public DeferredRenderer()
		{
			_colorShaderProgram = new ShaderProgram(
				File.ReadAllText("Shaders/color.vert.glsl"),
				File.ReadAllText("Shaders/color.frag.glsl"));
			_textureShaderProgram = new ShaderProgram(
				File.ReadAllText("Shaders/texture.vert.glsl"),
				File.ReadAllText("Shaders/texture.frag.glsl"));

			_grassTexture = new Texture2D("Textures/grass1.png");
		}

		public Matrix4 PvMatrix { get; set; }

		public void Dispose()
		{
			_grassTexture.Dispose();
			_colorShaderProgram.Dispose();
			_textureShaderProgram.Dispose();
		}

		public void RenderMesh(Matrix4 model, RenderMesh mesh)
		{
			// Set the shader settings
			_textureShaderProgram.Use();
			_textureShaderProgram.SetModelViewProjection(model * PvMatrix);

			GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.BufferId);
			TexturedVertex.SetVertexAttribPointers();
			_grassTexture.Bind();

			GL.DrawArrays(PrimitiveType.Triangles, 0, mesh.VertexCount);

			Texture2D.ClearBind();
			TexturedVertex.ClearVertexAttribPointers();
		}
	}
}
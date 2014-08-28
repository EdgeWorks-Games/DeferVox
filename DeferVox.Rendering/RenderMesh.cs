using System;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering
{
	public sealed class RenderMesh : IDisposable
	{
		public RenderMesh(Mesh mesh)
		{
			if (mesh == null)
				throw new ArgumentNullException("mesh");

			var meshData = mesh.Vertices;

			BufferId = GL.GenBuffer();
			VertexCount = meshData.Length;

			GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
			GL.BufferData(
				BufferTarget.ArrayBuffer,
				new IntPtr(TexturedVertex.SizeInBytes*meshData.Length),
				meshData,
				BufferUsageHint.StaticDraw);
		}

		public int BufferId { get; private set; }
		public int VertexCount { get; private set; }

		public void Dispose()
		{
			GL.DeleteBuffer(BufferId);
			GC.SuppressFinalize(this);
		}

		~RenderMesh()
		{
			Trace.TraceWarning("[RESOURCE LEAK] RenderMesh finalizer invoked!");
			Dispose();
		}
	}
}
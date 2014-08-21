using System;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering
{
	public sealed class StaticMesh<TVertex> : IDisposable where TVertex : struct
	{
		public StaticMesh(TVertex[] mesh, int vertexSizeInBytes)
		{
			if (mesh == null)
				throw new ArgumentNullException("mesh");

			BufferId = GL.GenBuffer();
			VertexCount = mesh.Length;

			GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
			GL.BufferData(
				BufferTarget.ArrayBuffer,
				new IntPtr(vertexSizeInBytes*mesh.Length),
				mesh,
				BufferUsageHint.StaticDraw);
		}

		public int BufferId { get; private set; }
		public int VertexCount { get; private set; }

		public void Dispose()
		{
			GL.DeleteBuffer(BufferId);
		}
	}
}
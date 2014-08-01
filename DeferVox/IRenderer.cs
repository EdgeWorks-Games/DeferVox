using System;
using System.Numerics;
using DeferVox.Graphics;

namespace DeferVox
{
	public interface IRenderer : IDisposable
	{
		void RenderScene(GameScene scene);
		void RenderStreamedMesh(Vector3f position, Vector3f rotation, PositionColorVertex[] meshData);
		void RenderStreamedMesh(Vector3f position, Vector3f rotation, PositionUvVertex[] meshData);
	}
}
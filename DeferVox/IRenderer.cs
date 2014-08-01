using System;
using System.Numerics;
using DeferVox.Graphics;

namespace DeferVox
{
	public interface IRenderer : IDisposable
	{
		void RenderScene(GameScene scene);
		void RenderMesh(Vector3f position, Vector3f rotation, StaticMesh<PositionColorVertex> mesh);
	}
}
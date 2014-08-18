using System;

namespace DeferVox
{
	public interface ISceneRenderer : IDisposable
	{
		void RenderScene(GameScene scene);
	}
}
using System;

namespace DeferVox.Scenes
{
	public interface ISceneRenderer : IDisposable
	{
		void RenderScene(GameScene scene);
	}
}
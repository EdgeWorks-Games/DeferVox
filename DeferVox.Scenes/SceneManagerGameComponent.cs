using System;

namespace DeferVox.Scenes
{
	public sealed class SceneManagerGameComponent : IGameComponent, IDisposable
	{
		private readonly Action<GameScene> _defaultSceneInitializer;
		private readonly ISceneRenderer _sceneRenderer;
		private GameScene _currentScene;

		public SceneManagerGameComponent(
			Action<GameScene> defaultSceneInitializer,
			ISceneRenderer sceneRenderer)
		{
			_defaultSceneInitializer = defaultSceneInitializer;
			_sceneRenderer = sceneRenderer;
		}

		public void Dispose()
		{
			// If the scene is loaded, dispose it
			if (_currentScene != null)
				_currentScene.Dispose();

			_sceneRenderer.Dispose();
		}

		public void Load()
		{
			// Create the default scene
			_currentScene = new GameScene();
			_defaultSceneInitializer(_currentScene);
		}

		public void PreUpdate()
		{
		}

		public void Update(TimeSpan delta)
		{
			_currentScene.Update(delta);
		}

		public void Render()
		{
			_sceneRenderer.RenderScene(_currentScene);
		}
	}
}
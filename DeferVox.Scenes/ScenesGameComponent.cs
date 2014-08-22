using System;

namespace DeferVox.Scenes
{
	public sealed class ScenesGameComponent : IGameComponent, IDisposable
	{
		private readonly Action<GameScene> _defaultSceneInitializer;
		private readonly ISceneRenderer _sceneRenderer;
		private GameScene _currentScene;

		public ScenesGameComponent(
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
			if (_currentScene == null)
				throw new InvalidOperationException("Cannot Update before a scene has been loaded. Call Load to load the default scene.");

			_currentScene.Update(delta);
		}

		public void Render()
		{
			_sceneRenderer.RenderScene(_currentScene);
		}
	}
}
﻿using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class GameEngine : IDisposable
	{
		private readonly Action<GameScene> _defaultSceneInitializer;
		private readonly GameWindow _gameWindow;
		private readonly Func<Size, ISceneRenderer> _rendererFactory;
		private GameScene _currentScene;
		private ISceneRenderer _renderer;

		public GameEngine(
			string friendlyName,
			Action<GameScene> defaultSceneInitializer,
			Func<Size, ISceneRenderer> rendererFactory)
		{
			_defaultSceneInitializer = defaultSceneInitializer;
			_rendererFactory = rendererFactory;

			// Set up the game window
			_gameWindow = new GameWindow(
				1280, 720,
				new GraphicsMode(32, 16, 0, 0), // Deferred rendering so no samples
				friendlyName,
				GameWindowFlags.FixedWindow);

			_gameWindow.Load += _gameWindow_Load;
			_gameWindow.RenderFrame += _gameWindow_RenderFrame;
			_gameWindow.UpdateFrame += _gameWindow_UpdateFrame;
		}

		public void Dispose()
		{
			// If the scene is loaded, dispose it
			if (_currentScene != null)
				_currentScene.Dispose();

			_renderer.Dispose();
			_gameWindow.Dispose();
		}

		private void _gameWindow_Load(object sender, EventArgs e)
		{
			// Set up rendering
			_renderer = _rendererFactory(_gameWindow.ClientSize);

			// Create the default scene
			_currentScene = new GameScene();
			_defaultSceneInitializer(_currentScene);
		}

		private void _gameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			var delta = TimeSpan.FromSeconds(Math.Min(e.Time, 0.4));

			if (Keyboard.GetState().IsKeyDown(Key.Escape))
				_gameWindow.Exit();

			// Update the currently active scene
			_currentScene.Update(delta);
		}

		private void _gameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			_renderer.RenderScene(_currentScene);

			_gameWindow.SwapBuffers();
		}

		public void Run()
		{
			_gameWindow.Run();
			Cursor.Clip = new Rectangle();
		}
	}
}
using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class GameEngine : IDisposable
	{
		private readonly Func<GameScene> _defaultSceneFactory;
		private readonly GameWindow _gameWindow;
		private GameScene _currentScene;
		private bool _mouseLocked = true;

		public GameEngine(string friendlyName, Func<GameScene> defaultSceneFactory)
		{
			_defaultSceneFactory = defaultSceneFactory;

			// Set up the game window
			_gameWindow = new GameWindow(
				1280, 720,
				new GraphicsMode(32, 16, 0, 0), // Deferred rendering so no samples
				friendlyName,
				GameWindowFlags.FixedWindow);

			_gameWindow.Load += _gameWindow_Load;
			_gameWindow.RenderFrame += _gameWindow_RenderFrame;
			_gameWindow.UpdateFrame += _gameWindow_UpdateFrame;
			_gameWindow.MouseMove += _gameWindow_MouseMove;
			_gameWindow.FocusedChanged += _gameWindow_FocusedChanged;

			CenterCursor();
			ClipCursor();
		}

		public void Dispose()
		{
			_currentScene.Dispose();
			_gameWindow.Dispose();
		}

		private void _gameWindow_Load(object sender, EventArgs e)
		{
			// Create the default scene
			_currentScene = _defaultSceneFactory();
		}

		private void _gameWindow_MouseMove(object sender, MouseMoveEventArgs e)
		{
			if (_mouseLocked)
				CenterCursor();
		}

		private void ClipCursor()
		{
			/*var borderSize = (_gameWindow.Bounds.Width - _gameWindow.ClientSize.Width)/2;
			Cursor.Clip = new Rectangle(
				_gameWindow.Bounds.X + borderSize,
				(_gameWindow.Bounds.Y + _gameWindow.Bounds.Height) - (_gameWindow.ClientSize.Height + borderSize),
				_gameWindow.ClientSize.Width,
				_gameWindow.ClientSize.Height);*/
		}

		private void CenterCursor()
		{
			/*Cursor.Position = new Point(
				_gameWindow.Bounds.Left + (_gameWindow.Bounds.Width/2),
				_gameWindow.Bounds.Top + (_gameWindow.Bounds.Height/2));*/
		}

		private void _gameWindow_FocusedChanged(object sender, EventArgs e)
		{
			_mouseLocked = _gameWindow.Focused;
			CenterCursor();
			ClipCursor();
		}

		private void _gameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			var delta = TimeSpan.FromSeconds(e.Time);

			if (Keyboard.GetState().IsKeyDown(Key.Escape))
				_gameWindow.Exit();

			// Update the currently active scene
			_currentScene.Update(delta);
		}

		private void _gameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			// Set up OpenGL settings
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);

			// Clear the default buffer
			GL.ClearColor(Color.CornflowerBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			// Render the currently active scene
			_currentScene.Render();

			_gameWindow.SwapBuffers();
		}

		public void Run()
		{
			_gameWindow.Run();
			Cursor.Clip = new Rectangle();
		}
	}
}
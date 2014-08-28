using System;
using System.ComponentModel;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace DeferVox.Window
{
	public class WindowGameComponent : IGameComponent, IDisposable
	{
		private readonly GameEngine _engine;
		private readonly GameWindow _window;

		public WindowGameComponent(GameEngine engine)
		{
			_engine = engine;

			// Set up the game window
			_window = new GameWindow(
				1280, 720,
				new GraphicsMode(32, 16, 0, 0), // Deferred rendering so no samples
				engine.Name,
				GameWindowFlags.FixedWindow)
			{
				Visible = true
			};

			// We own the window so we're responsible for polling events
			_engine.PreUpdate += EngineOnPreUpdate;
			_window.Closing += WindowOnClosing;
		}

		public bool Visible
		{
			get { return _window.Visible; }
			set { _window.Visible = value; }
		}

		public bool CursorVisible
		{
			get { return _window.CursorVisible; }
			set { _window.CursorVisible = value; }
		}

		public Rectangle Bounds
		{
			get { return _window.Bounds; }
			set { _window.Bounds = value; }
		}

		public Size ClientSize
		{
			get { return _window.ClientSize; }
			set { _window.ClientSize = value; }
		}

		public void Dispose()
		{
		}

		private void EngineOnPreUpdate(object sender, UpdateEventArgs e)
		{
			_window.ProcessEvents();
		}

		private void WindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
		{
			_engine.KeepRunning = false;
		}

		public void MakeCurrent()
		{
			_window.MakeCurrent();
		}

		public void SwapBuffers()
		{
			_window.SwapBuffers();
		}
	}
}
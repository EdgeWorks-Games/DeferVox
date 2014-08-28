using System;
using System.Drawing;
using System.Windows.Forms;
using DeferVox.Window;

namespace DeferVox.Input
{
	public sealed class InputGameComponent : IGameComponent, IDisposable
	{
		private readonly WindowGameComponent _window;
		Point _currentPointer, _previousPointer;

		public InputGameComponent(GameEngine engine, WindowGameComponent window)
		{
			_window = window;
			_window.CursorVisible = false;

			engine.PreUpdate += EngineOnPreUpdate;

			ClipCursor();
		}

		public void Dispose()
		{
			Cursor.Clip = new Rectangle();
		}

		public event EventHandler<AimEventArgs> AimChange = (e, s) => { };

		private void ClipCursor()
		{
			var borderSize = (_window.Bounds.Width - _window.ClientSize.Width) / 2;
			Cursor.Clip = new Rectangle(
				_window.Bounds.X + borderSize,
				(_window.Bounds.Y + _window.Bounds.Height) - (_window.ClientSize.Height + borderSize),
				_window.ClientSize.Width,
				_window.ClientSize.Height);
		}

		private void EngineOnPreUpdate(object s, UpdateEventArgs e)
		{
			_currentPointer = Cursor.Position;

			var deltaPointer = new Point(
				_currentPointer.X - _previousPointer.X,
				_currentPointer.Y - _previousPointer.Y);

			Cursor.Position = new Point(
				_window.Bounds.Left + (_window.Bounds.Width / 2),
				_window.Bounds.Top + (_window.Bounds.Height / 2));

			_previousPointer = Cursor.Position;

			AimChange(this, new AimEventArgs(deltaPointer.X, deltaPointer.Y));
		}
	}
}
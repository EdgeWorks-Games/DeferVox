using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class AimEventArgs : EventArgs
	{
		public AimEventArgs(int xDelta, int yDelta)
		{
			XDelta = xDelta;
			YDelta = yDelta;
		}

		public int XDelta { get; private set; }
		public int YDelta { get; private set; }
	}

	public sealed class GameInput : IDisposable
	{
		private readonly GameWindow _gameWindow;
		Point _currentPointer, _previousPointer;

		public GameInput(GameWindow gameWindow)
		{
			_gameWindow = gameWindow;
			_gameWindow.CursorVisible = false;
			ClipCursor();
		}

		public void Dispose()
		{
			Cursor.Clip = new Rectangle();
		}

		public event EventHandler<AimEventArgs> AimChange = (e, s) => { };

		private void ClipCursor()
		{
			var borderSize = (_gameWindow.Bounds.Width - _gameWindow.ClientSize.Width)/2;
			Cursor.Clip = new Rectangle(
				_gameWindow.Bounds.X + borderSize,
				(_gameWindow.Bounds.Y + _gameWindow.Bounds.Height) - (_gameWindow.ClientSize.Height + borderSize),
				_gameWindow.ClientSize.Width,
				_gameWindow.ClientSize.Height);
		}


		public void UpdateMousePosition()
		{
			_currentPointer = Cursor.Position;

			var deltaPointer = new Point(
				_currentPointer.X - _previousPointer.X,
				_currentPointer.Y - _previousPointer.Y);

			Cursor.Position = new Point(
				_gameWindow.Bounds.Left + (_gameWindow.Bounds.Width / 2),
				_gameWindow.Bounds.Top + (_gameWindow.Bounds.Height / 2));

			_previousPointer = Cursor.Position;

			AimChange(this, new AimEventArgs(deltaPointer.X, deltaPointer.Y));
		}
	}
}
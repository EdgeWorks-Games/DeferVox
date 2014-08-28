using System;
using System.Drawing;
using System.Windows.Forms;

namespace DeferVox.Input
{
	public sealed class InputGameComponent : IGameComponent, IDisposable
	{
		private readonly GameEngine _engine;
		Point _currentPointer, _previousPointer;

		public InputGameComponent(GameEngine engine)
		{
			_engine = engine;
			//_engine.GameWindow.CursorVisible = false;
			ClipCursor();
		}

		public void Dispose()
		{
			Cursor.Clip = new Rectangle();
		}

		public event EventHandler<AimEventArgs> AimChange = (e, s) => { };

		private void ClipCursor()
		{
			/*var borderSize = (_engine.GameWindow.Bounds.Width - _engine.GameWindow.ClientSize.Width) / 2;
			Cursor.Clip = new Rectangle(
				_engine.GameWindow.Bounds.X + borderSize,
				(_engine.GameWindow.Bounds.Y + _engine.GameWindow.Bounds.Height) - (_engine.GameWindow.ClientSize.Height + borderSize),
				_engine.GameWindow.ClientSize.Width,
				_engine.GameWindow.ClientSize.Height);*/
		}

		public void Load()
		{
		}

		public void PreUpdate()
		{
			/*_currentPointer = Cursor.Position;

			var deltaPointer = new Point(
				_currentPointer.X - _previousPointer.X,
				_currentPointer.Y - _previousPointer.Y);

			Cursor.Position = new Point(
				_engine.GameWindow.Bounds.Left + (_engine.GameWindow.Bounds.Width / 2),
				_engine.GameWindow.Bounds.Top + (_engine.GameWindow.Bounds.Height / 2));

			_previousPointer = Cursor.Position;

			AimChange(this, new AimEventArgs(deltaPointer.X, deltaPointer.Y));*/
		}

		public void Update(TimeSpan delta)
		{
		}

		public void Render()
		{
		}
	}
}
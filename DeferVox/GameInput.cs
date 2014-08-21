using System;
using OpenTK;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class AimEventArgs : EventArgs
	{
		public int XDelta { get; private set; }
		public int YDelta { get; private set; }

		public AimEventArgs(int xDelta, int yDelta)
		{
			XDelta = xDelta;
			YDelta = yDelta;
		}
	}

	public sealed class GameInput
	{
		public GameInput(GameWindow gameWindow)
		{
			gameWindow.MouseMove += gameWindow_MouseMove;
		}

		private void gameWindow_MouseMove(object sender, MouseMoveEventArgs e)
		{
			AimChange(this, new AimEventArgs(e.XDelta, e.YDelta));
		}

		public event EventHandler<AimEventArgs> AimChange = (e, s) => { };
	}
}
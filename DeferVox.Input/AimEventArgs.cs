using System;

namespace DeferVox.Input
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
}
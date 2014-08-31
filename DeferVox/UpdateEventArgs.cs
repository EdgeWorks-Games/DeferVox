using System;

namespace DeferVox
{
	public class UpdateEventArgs : EventArgs
	{
		public UpdateEventArgs(TimeSpan delta, GameScene scene)
		{
			Delta = delta;
			Scene = scene;
		}

		public TimeSpan Delta { get; private set; }
		public GameScene Scene { get; private set; }
	}
}
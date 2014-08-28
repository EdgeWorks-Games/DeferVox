using System;

namespace DeferVox
{
	public class UpdateEventArgs
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
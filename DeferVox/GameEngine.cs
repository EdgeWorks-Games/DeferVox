using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class GameEngine : IDisposable
	{
		private GameScene _scene;

		public GameEngine()
		{
			Components = new Collection<IGameComponent>();
		}

		public string Name { get; set; }
		public Collection<IGameComponent> Components { get; private set; }

		public bool KeepRunning { get; set; }
		public TimeSpan TargetDelta { get; set; }

		public GameScene Scene
		{
			get { return _scene; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				if (_scene != null)
					_scene.Dispose();

				_scene = value;
			}
		}

		public void Dispose()
		{
			if (_scene != null)
				_scene.Dispose();
		}

		public event EventHandler<UpdateEventArgs> PreUpdate = (s, e) => { };
		public event EventHandler<UpdateEventArgs> Update = (s, e) => { };
		public event EventHandler<UpdateEventArgs> PostUpdate = (s, e) => { };

		public void UpdateWorld(TimeSpan delta)
		{
			if (_scene == null)
				throw new InvalidOperationException("Can't update before a scene has been set.");

			PreUpdate(this, new UpdateEventArgs(delta, _scene));

			var keyState = Keyboard.GetState();
			if ((keyState.IsKeyDown(Key.F4) && keyState.IsKeyDown(Key.AltLeft)) ||
				keyState.IsKeyDown(Key.Escape))
			{
				KeepRunning = false;
			}

			Update(this, new UpdateEventArgs(delta, _scene));

			PostUpdate(this, new UpdateEventArgs(delta, _scene));
		}

		public void Run()
		{
			if (_scene == null)
				throw new InvalidOperationException("Can't run before a scene has been set.");

			var stopwatch = new Stopwatch();
			var previousDelta = TargetDelta;

			KeepRunning = true;
			while (KeepRunning)
			{
				stopwatch.Restart();

				UpdateWorld(previousDelta);

				while (stopwatch.Elapsed < TargetDelta)
				{
					if (Thread.Yield())
						continue;

					// We couldn't yield, just sleep a bit
					Thread.Sleep(0);
				}

				previousDelta = stopwatch.Elapsed;
			}
		}
	}
}
using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class GameEngine : IDisposable
	{
		private GameScene _scene;

		public GameEngine(string friendlyName)
		{
			Name = friendlyName;
			Components = new Collection<IGameComponent>();
		}

		public bool KeepRunning { get; set; }
		public string Name { get; private set; }
		public Collection<IGameComponent> Components { get; private set; }

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
		public event EventHandler<UpdateEventArgs> AtUpdate = (s, e) => { };
		public event EventHandler<UpdateEventArgs> PostUpdate = (s, e) => { };

		public void Update(TimeSpan delta)
		{
			if (_scene == null)
				throw new InvalidOperationException("Can't update before a scene has been set.");

			PreUpdate(this, new UpdateEventArgs(delta, _scene));

#if DEBUG
			// Debug key for halting execution
			if (Keyboard.GetState().IsKeyDown(Key.Escape))
				KeepRunning = false;
#endif
			AtUpdate(this, new UpdateEventArgs(delta, _scene));

			PostUpdate(this, new UpdateEventArgs(delta, _scene));
		}

		public void Run()
		{
			if (_scene == null)
				throw new InvalidOperationException("Can't run before a scene has been set.");

			var targetDelta = TimeSpan.FromSeconds(0.016);
			KeepRunning = true;
			while (KeepRunning)
			{
				Update(targetDelta);

				// TODO: Write in actual frame limiting
				Thread.Yield();
			}
		}
	}
}
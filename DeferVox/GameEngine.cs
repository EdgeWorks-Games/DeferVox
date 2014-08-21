using System;
using System.Collections.ObjectModel;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace DeferVox
{
	public sealed class GameEngine : IDisposable
	{
		public GameEngine(string friendlyName)
		{
			Components = new Collection<IGameComponent>();

			// Set up the game window
			GameWindow = new GameWindow(
				1280, 720,
				new GraphicsMode(32, 16, 0, 0), // Deferred rendering so no samples
				friendlyName,
				GameWindowFlags.FixedWindow);

			GameWindow.Load += GameWindow_Load;
			GameWindow.RenderFrame += GameWindow_RenderFrame;
			GameWindow.UpdateFrame += GameWindow_UpdateFrame;
		}

		public GameWindow GameWindow { get; private set; }
		public Collection<IGameComponent> Components { get; private set; }

		public void Dispose()
		{
			GameWindow.Dispose();
		}

		private void GameWindow_Load(object sender, EventArgs e)
		{
			foreach (var component in Components)
				component.Load();
		}

		private void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			var delta = TimeSpan.FromSeconds(Math.Min(e.Time, 0.4));

#if DEBUG
			// Debug key for halting execution
			if (Keyboard.GetState().IsKeyDown(Key.Escape))
				GameWindow.Exit();
#endif

			// Perform work that has to be done before the world state can be updated
			// For example: input and network packet processing
			foreach (var component in Components)
				component.PreUpdate();

			// Update the world state
			foreach (var component in Components)
				component.Update(delta);
		}

		private void GameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			foreach (var component in Components)
				component.Render();

			GameWindow.SwapBuffers();
		}

		public void Run()
		{
			GameWindow.Run();
		}
	}
}
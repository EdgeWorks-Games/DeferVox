using System.Drawing;
using DeferVox;
using DeferVox.Input;
using DeferVox.Rendering;
using DeferVox.Toolbox;
using DeferVox.Toolbox.Voxels;
using DeferVox.Window;
using OpenTK;

namespace RedLine
{
	internal class Program
	{
		private static void Main()
		{
			var program = new Program();
			program.Run();
		}

		private void Run()
		{
			using (var engine = new GameEngine("Red Line"))
			using (var window = new WindowGameComponent(engine))
			using (var rendering = new RenderingGameComponent(engine, window))
			using (var input = new InputGameComponent(engine, window))
			{
				engine.Components.Add(window);
				engine.Components.Add(rendering);
				engine.Components.Add(input);

				var scene = new GameScene();

				scene.Root.AddChild(new NoClipCameraObject(engine, input, new Size(1280, 720))
				{
					Speed = 3f,
					FastSpeed = 6f
				});

				scene.Root.AddChild(new VoxelMapObject());

				engine.Scene = scene;

				engine.Run();
			}
		}
	}
}
using System.Drawing;
using DeferVox;
using DeferVox.BasicEntities;
using DeferVox.Rendering.Deferred;
using DeferVox.Voxels;
using OpenTK;

namespace RedLine
{
	internal static class Program
	{
		private static void Main()
		{
			using (var engine = new GameEngine(
				"Red Line",
				InitializeMainMenuScene,
				res => new DeferredSceneRenderer(res)))
			{
				engine.Run();
			}
		}

		private static void InitializeMainMenuScene(GameScene scene, GameEngine engine)
		{
			var playerCamera = new Camera
			{
				Resolution = new Size(1280, 720),
				VerticalFieldOfView = MathHelper.DegreesToRadians(70)
			};
			scene.Cameras.Add(playerCamera);

			scene.Entities.Add(new PlayerEntity(engine.Input, new Vector3(0, 1.1f, 0), playerCamera));
			scene.Entities.Add(new VoxelMapEntity());
		}
	}
}
using DeferVox;
using DeferVox.Entities;
using DeferVox.Rendering.Deferred;

namespace RedLine
{
	internal static class Program
	{
		private static void Main()
		{
			using (var engine = new GameEngine(
				"Red Line",
				CreateMainMenuScene,
				res => new DeferredRenderer(res)))
			{
				engine.Run();
			}
		}

		private static void CreateMainMenuScene(GameScene scene)
		{
			// Add the voxel map
			scene.Entities.Add(new VoxelMapEntity());
		}
	}
}
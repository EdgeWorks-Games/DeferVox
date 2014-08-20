using DeferVox;
using DeferVox.Voxels;
using DeferVox.Rendering.Deferred;

namespace RedLine
{
	internal static class Program
	{
		private static void Main()
		{
			using (var engine = new GameEngine(
				"Red Line",
				InitializeMainMenuScene,
				res => new DeferredRenderer(res)))
			{
				engine.Run();
			}
		}

		private static void InitializeMainMenuScene(GameScene scene)
		{
			// Add the voxel map
			scene.Entities.Add(new VoxelMapEntity());
		}
	}
}
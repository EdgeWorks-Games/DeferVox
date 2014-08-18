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

		private static GameScene CreateMainMenuScene()
		{
			GameScene workingScene = null, finishedScene;
			try
			{
				// Create our working scene
				workingScene = new GameScene();

				// Add the voxel map
				workingScene.Entities.Add(new VoxelMapEntity());

				// Mark the working scene as done
				finishedScene = workingScene;
				workingScene = null;
			}
			finally
			{
				// If an exception occured, clean up our working scene
				if(workingScene != null)
					workingScene.Dispose();
			}

			return finishedScene;
		}
	}
}
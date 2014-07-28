using DeferVox;

namespace RedLine
{
	internal static class Program
	{
		private static void Main()
		{
			using (var engine = new GameEngine(
				"Red Line",
				CreateMainMenuScene))
			{
				engine.Run();
			}
		}

		private static GameScene CreateMainMenuScene()
		{
			var scene = new GameScene();

			// Add a single test cube
			scene.Entities.Add(new TestEntity());

			return scene;
		}
	}
}
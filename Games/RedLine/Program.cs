﻿using System.Numerics;
using DeferVox;
using DeferVox.Entities;
using DeferVox.Graphics;

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
			var scene = new GameScene();

			// Add a single test entity
			scene.Entities.Add(new TestEntity
			{
				Position = new Vector3f(0, 2, 0)
			});

			// Add the voxel map
			scene.Entities.Add(new VoxelMapEntity());

			return scene;
		}
	}
}
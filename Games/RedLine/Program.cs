﻿using System.Drawing;
using DeferVox;
using DeferVox.BasicEntities;
using DeferVox.Input;
using DeferVox.Rendering.Deferred;
using DeferVox.Scenes;
using DeferVox.Voxels;
using OpenTK;

namespace RedLine
{
	internal class Program
	{
		private InputGameComponent _input;

		private static void Main()
		{
			var program = new Program();
			program.Run();
		}

		private void Run()
		{
			using (var engine = new GameEngine("Red Line"))
			using (var sceneManager = new ScenesGameComponent(
				InitializeMainMenuScene,
				new DeferredSceneRenderer(new Size(1280, 720))))
			using (_input = new InputGameComponent(engine))
			{
				engine.Components.Add(sceneManager);
				engine.Components.Add(_input);

				engine.Run();
			}
		}

		private void InitializeMainMenuScene(GameScene scene)
		{
			var playerCamera = new Camera
			{
				Resolution = new Size(1280, 720),
				VerticalFieldOfView = MathHelper.DegreesToRadians(70)
			};
			scene.Cameras.Add(playerCamera);

			scene.Entities.Add(new PlayerEntity(_input, new Vector3(0, 1.1f, 0), playerCamera));
			scene.Entities.Add(new VoxelMapEntity());
		}
	}
}
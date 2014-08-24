using System;
using DeferVox.Scenes;
using Moq;
using Xunit;

namespace DeferVox.Tests.Scenes
{
	public class GameSceneTests
	{
		[Fact]
		public void Dispose_Entities_Disposed()
		{
			var gameScene = new GameScene();
			var mock = new Mock<IEntity>();
			mock.Setup(e => e.Dispose()).Verifiable();

			gameScene.Entities.Add(mock.Object);
			gameScene.Dispose();

			mock.Verify(e => e.Dispose(), Times.Once);
		}

		[Fact]
		public void Update_Entities_Updated()
		{
			var gameScene = new GameScene();
			var delta = TimeSpan.FromSeconds(1.5);
			var mock = new Mock<IEntity>();
			mock.Setup(e => e.Update(delta)).Verifiable();

			gameScene.Entities.Add(mock.Object);
			gameScene.Update(delta);

			mock.Verify(e => e.Update(delta), Times.Once);

			gameScene.Dispose();
		}

		[Fact]
		public void Update_NoEntities_CanUpdate()
		{
			var gameScene = new GameScene();
			var delta = TimeSpan.FromSeconds(1.5);

			gameScene.Update(delta);

			gameScene.Dispose();
		}
	}
}

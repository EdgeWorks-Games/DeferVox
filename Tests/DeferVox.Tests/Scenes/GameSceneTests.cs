using System;
using DeferVox.Scenes;
using Moq;
using Xunit;

namespace DeferVox.Tests.Scenes
{
	class GameSceneTests : IDisposable
	{
		private readonly GameScene _gameScene = new GameScene();

		public void Dispose()
		{
			_gameScene.Dispose();
		}

		[Fact]
		public void Update_Entities_Updated()
		{
			var delta = TimeSpan.FromSeconds(1.5);

			var mock = new Mock<IEntity>();
			mock.Setup(e => e.Update(delta)).Verifiable();

			_gameScene.Entities.Add(mock.Object);
			_gameScene.Update(delta);

			mock.Verify(e => e.Update(delta), Times.Once);
		}

		[Fact]
		public void Update_NoEntities_CanUpdate()
		{
			var delta = TimeSpan.FromSeconds(1.5);

			_gameScene.Update(delta);
		}

		[Fact]
		public void Dispose_Entities_Disposed()
		{
			var mock = new Mock<IEntity>();
			mock.Setup(e => e.Dispose()).Verifiable();

			_gameScene.Entities.Add(mock.Object);
			_gameScene.Dispose();

			mock.Verify(e => e.Dispose(), Times.Once);
		}
	}
}

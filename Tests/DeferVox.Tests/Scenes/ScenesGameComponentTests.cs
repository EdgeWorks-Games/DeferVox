using System;
using DeferVox.Scenes;
using Moq;
using Xunit;

namespace DeferVox.Tests.Scenes
{
	public class ScenesGameComponentTests
	{
		private static void InitializeSceneWith(GameScene scene, IMock<IEntity> mock)
		{
			scene.Entities.Add(mock.Object);
		}

		[Fact]
		public void Dispose_Scene_Disposed()
		{
			var mock = new Mock<IEntity>();
			mock.Setup(e => e.Dispose()).Verifiable();
			var component = new ScenesGameComponent(s => InitializeSceneWith(s, mock), Mock.Of<ISceneRenderer>());

			component.Load();
			component.Dispose();

			mock.Verify(e => e.Dispose(), Times.Once);
		}

		[Fact]
		public void Load_Scene_Initialized()
		{
			var initialized = false;
			var component = new ScenesGameComponent(s => initialized = true, Mock.Of<ISceneRenderer>());

			// Before load it should not yet be initialized
			Assert.False(initialized);

			component.Load();

			Assert.True(initialized);
		}

		[Fact]
		public void Update_Scene_Updates()
		{
			var mock = new Mock<IEntity>();
			mock.Setup(e => e.Update(It.IsAny<TimeSpan>())).Verifiable();
			var component = new ScenesGameComponent(s => InitializeSceneWith(s, mock), Mock.Of<ISceneRenderer>());

			component.Load();
			component.Update(TimeSpan.FromSeconds(0.1));

			mock.Verify(e => e.Update(It.IsAny<TimeSpan>()), Times.Once);
		}

		[Fact]
		public void Update_NotLoaded_ThrowsException()
		{
			var component = new ScenesGameComponent(s => { }, Mock.Of<ISceneRenderer>());

			Assert.Throws(typeof (InvalidOperationException), () => component.Update(TimeSpan.FromSeconds(0.1)));
		}
	}
}
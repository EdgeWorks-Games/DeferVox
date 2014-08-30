using System;
using System.Collections.Generic;
using Xunit;

namespace DeferVox.Tests
{
// ReSharper disable AccessToDisposedClosure
	public class GameEngineTests
	{
		[Fact]
		public void UpdateWorld_NullScene_ThrowsException()
		{
			using (var engine = new GameEngine())
			{
				Assert.Throws<InvalidOperationException>(() =>
					engine.UpdateWorld(TimeSpan.FromSeconds(0.1)));
			}
		}

		[Fact]
		public void UpdateWorld_EmptyScene_CallsEvents()
		{
			using (var engine = new GameEngine())
			{
				var checkList = new List<string>();
				engine.PreUpdate += (s, e) => checkList.Add("PreUpdate");
				engine.Update += (s, e) => checkList.Add("Update");
				engine.PostUpdate += (s, e) => checkList.Add("PostUpdate");

				engine.Scene = new GameScene();
				engine.UpdateWorld(TimeSpan.FromSeconds(0.1));

				Assert.Equal(3, checkList.Count);
				Assert.Equal("PreUpdate", checkList[0]);
				Assert.Equal("Update", checkList[1]);
				Assert.Equal("PostUpdate", checkList[2]);
			}
		}
	}

// ReSharper restore AccessToDisposedClosure
}
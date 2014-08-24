using System.Drawing;
using DeferVox.Scenes;
using Xunit;

namespace DeferVox.Tests.Scenes
{
	public class CameraTests
	{
		[Fact]
		public void Ratio_Values_CorrectRatio()
		{
			var camera = new Camera
			{
				Resolution = new Size(10, 5)
			};
			Assert.Equal(2f, camera.Ratio);
		}
	}
}
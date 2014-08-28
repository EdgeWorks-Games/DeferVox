using System.Drawing;
using DeferVox.ObjectComponents;
using Xunit;

namespace DeferVox.Tests.ObjectComponents
{
	public class CameraTests
	{
		[Fact]
		public void Ratio_Values_CorrectRatio()
		{
			var camera = new CameraObjectComponent
			{
				Resolution = new Size(10, 5)
			};
			Assert.Equal(2f, camera.Ratio);
		}
	}
}
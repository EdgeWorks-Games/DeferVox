using System.Numerics;
using Xunit;

namespace DeferVox.Tests
{
	public class NumericsExtensionsTests
	{
		[Fact]
		public void ToVector3_NormalValues_Converted()
		{
			var normalValues = new Vector3f(6, -52, 23);
			var result = normalValues.ToVector3();
			Assert.Equal(6, result.X);
			Assert.Equal(-52, result.Y);
			Assert.Equal(23, result.Z);
		}
	}
}
using Xunit;

namespace DeferVox.Tests
{
	public class Vector3ITests
	{
		[Fact]
		public void ToVector3_NormalValues_Converted()
		{
			var values = new Vector3I(6, -52, 23);

			var result = values.ToVector3();

			Assert.Equal(values.X, result.X);
			Assert.Equal(values.Y, result.Y);
			Assert.Equal(values.Z, result.Z);
		}

		[Fact]
		public void Multiply_NormalValues_Multiplied()
		{
			var values = new Vector3I(6, -52, 23);

			var result = values*2;
			var resultStatic = Vector3I.Multiply(values, 2);

			Assert.Equal(values.X*2, result.X);
			Assert.Equal(values.X*2, resultStatic.X);
			Assert.Equal(values.Y*2, result.Y);
			Assert.Equal(values.Y*2, resultStatic.Y);
			Assert.Equal(values.Z*2, result.Z);
			Assert.Equal(values.Z*2, resultStatic.Z);
		}
	}
}
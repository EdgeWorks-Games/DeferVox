using System;
using Xunit;

namespace DeferVox.Tests
{
	public class Vector3ITests
	{
		[Fact]
		public void ToVector3_Values_Converted()
		{
			var values = new Vector3I(6, -52, 23);

			var result = values.ToVector3();

			Assert.Equal(values.X, result.X);
			Assert.Equal(values.Y, result.Y);
			Assert.Equal(values.Z, result.Z);
		}

		[Fact]
		public void Multiply_Values_Multiplied()
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

		[Fact]
		public void Equals_SameValues_ReturnsTrue()
		{
			var valueA = new Vector3I(6, -52, 23);
			var valueB = new Vector3I(6, -52, 23);

			Assert.True(valueA == valueB);
			Assert.False(valueA != valueB);
		}

		[Fact]
		public void Equals_DifferentValues_ReturnsFalse()
		{
			var valueA = new Vector3I(6, -52, 23);
			var valueB = new Vector3I(6, -52, -5);
			var valueC = new Vector3I(6, 7, -5);
			var valueD = new Vector3I(-63, 7, -5);

			Assert.False(valueA == valueB);
			Assert.False(valueA == valueC);
			Assert.False(valueA == valueD);
			Assert.True(valueA != valueB);
			Assert.True(valueA != valueC);
			Assert.True(valueA != valueD);
		}

		[Fact]
		public void Equals_BoxedSameValues_ReturnsTrue()
		{
			var valueA = new Vector3I(6, -52, 23);
			var valueB = (object)new Vector3I(6, -52, 23);

			Assert.True(valueA.Equals(valueB));
		}

		[Fact]
		public void Equals_DifferentTypes_ReturnsFalse()
		{
			var valueA = new Vector3I(6, -52, 23);
			var valueB = TimeSpan.FromSeconds(5.2);

// ReSharper disable SuspiciousTypeConversion.Global
			Assert.False(valueA.Equals(valueB));
// ReSharper restore SuspiciousTypeConversion.Global
		}

		[Fact]
		public void GetHashCode_SameValues_Match()
		{
			var valueA = new Vector3I(6, -52, 23);
			var valueB = new Vector3I(6, -52, 23);

			Assert.Equal(valueA.GetHashCode(), valueB.GetHashCode());
		}

		[Fact]
		public void GetHashCode_DifferentValues_DoNotMatch()
		{
			var valueA = new Vector3I(6, -52, 23);
			var valueB = new Vector3I(6, -53, 23);

			Assert.NotEqual(valueA.GetHashCode(), valueB.GetHashCode());
		}
	}
}
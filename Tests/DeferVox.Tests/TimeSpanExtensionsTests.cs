using System;
using OpenTK;
using Xunit;

namespace DeferVox.Tests
{
	public class TimeSpanExtensionsTests
	{
		[Fact]
		public void PerSecond_FloatHalfSecond_Halfed()
		{
			var time = TimeSpan.FromSeconds(0.5);
			Assert.Equal(1, time.PerSecond(2));
		}

		[Fact]
		public void PerSecond_Vector3HalfSecond_Halfed()
		{
			var time = TimeSpan.FromSeconds(0.5);
			var result = time.PerSecond(new Vector3(5, 2, 7));
			Assert.Equal(2.5f, result.X);
			Assert.Equal(1f, result.Y);
			Assert.Equal(3.5f, result.Z);
		}
	}
}

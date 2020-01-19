using FluentAssertions;
using Xunit;

namespace FatCat.XUnitRunner.Tests
{
	public class SanityCheck
	{
		[Fact]
		public void OnlyEverOnce() => TestFixture.InitializationCount.Should().Be(1);

		[Fact]
		public void WillAlwaysBe1() => TestFixture.InitializationCount.Should().Be(1);
	}
}
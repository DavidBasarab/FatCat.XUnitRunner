using FluentAssertions;
using Xunit;

namespace FatCat.XUnitRunner.Tests
{
	public class TestFixtureTests
	{
		[Fact]
		public void IWantThisToOnlyBeExecutedOnce() => TestFixture.InitializationCount.Should().Be(1);

		[Fact]
		public void WillOnlyInitializeTheTestOnce() => TestFixture.InitializationCount.Should().Be(1);
	}
}
namespace FatCat.XUnitRunner.Tests
{
	public class TestFixture : ITestFixture
	{
		public static int InitializationCount { get; set; }

		public void SetUp() { InitializationCount++; }

		public void TearDown() { InitializationCount = -76; }
	}
}
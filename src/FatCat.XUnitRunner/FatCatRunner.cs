using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FatCat.XUnitRunner
{
internal class FatCatRunner : XunitTestAssemblyRunner
{
	private readonly List<ITestFixture> testFixtures = new List<ITestFixture>();

	public FatCatRunner(ITestAssembly testAssembly, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
		: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions) { }

	protected override async Task AfterTestAssemblyStartingAsync()
	{
		// Let everything initialize
		await base.AfterTestAssemblyStartingAsync();

		Aggregator.Run(() =>
						{
							var assembly = ((IReflectionAssemblyInfo)TestAssembly.Assembly).Assembly;

							var testFixtureTypes = assembly.GetTypes().Where(i => i.GetInterfaces().Any(k => k.FullName == typeof(ITestFixture).FullName));

							foreach (var currentType in testFixtureTypes)
							{
								var testFixture = (ITestFixture)Activator.CreateInstanceFrom(assembly.Location, currentType.FullName).Unwrap();

								testFixture.SetUp();

								testFixtures.Add(testFixture);
							}
						});
	}

	protected override Task BeforeTestAssemblyFinishedAsync()
	{
		foreach (var testFixture in testFixtures) testFixture.TearDown();

		return base.BeforeTestAssemblyFinishedAsync();
	}

	protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus, ITestCollection testCollection, IEnumerable<IXunitTestCase> testCases, CancellationTokenSource cancellationTokenSource)
	{
		var runner = new XunitTestCollectionRunner(testCollection, testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource);

		return runner.RunAsync();
	}
}
}
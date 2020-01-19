using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FatCat.XUnitRunner
{
	internal class FatCatFramework : XunitTestFramework
	{
		public FatCatFramework(IMessageSink messageSink)
			: base(messageSink) { }

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName) => new FatCatExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
	}
}
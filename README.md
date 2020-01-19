# FatCat.XUnitRunner


## **A simple XUnit Runner that will have a global `ITestFixtures` that will run a one time `Setup` and `TearDown`**

https://www.nuget.org/packages/FatCat.XUnitRunner/

```
Install-Package FatCat.XUnitRunner
```

Add XUnit Packages

```
Install-Package xunit.runner.visualstudio
```

```
Install-Package xunit
```

In order to use FatCat.XUnitRunner you need to tell XUnit.  Please note this can be anywhere in your test project.

```C#
[assembly: TestFramework("FatCat.XUnitRunner.FatCatFramework", "FatCat.XUnitRunner")]
```

To use create a class, inherit from `ITestFixture` interface.  `SetUp` and `TearDown` will only be excuted once.

```C#
public class TestFixture : ITestFixture
{
    public static int InitializationCount { get; set; }

    public void SetUp() { InitializationCount++; }

    public void TearDown() { InitializationCount = -76; }
}
```


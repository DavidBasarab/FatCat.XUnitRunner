using System;

namespace FatCat.XUnitRunner
{
    public interface ITestFixture
    {
        void SetUp();

        void TearDown();
    }
}

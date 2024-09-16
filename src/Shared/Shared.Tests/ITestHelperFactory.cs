namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public interface ITestHelperFactory
    {
        Action<string>? Log { get; set; }

        TestHelper CreateTestHelper(bool migration, Action<IServiceCollection>? serviceCollection = null);
    }
}

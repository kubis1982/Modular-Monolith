namespace ModularMonolith.Modules.AccessManagement
{
    using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
    using ModularMonolith.Modules.AccessManagement.Persistance.WriteModel;
    using ModularMonolith.Shared;

    public class TestHelperFactory : TestHelperFactory<StartBootstraper, WriteDbContext, ReadDbContext>
    {
        public TestHelperFactory() : base(ServiceCollectionExtensions.ModuleCode)
        {
        }
    }
}

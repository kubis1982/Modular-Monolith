namespace ModularMonolith.Modules.ProjectName
{
    using ModularMonolith.Modules.ProjectName.Persistance.ReadModel;
    using ModularMonolith.Modules.ProjectName.Persistance.WriteModel;
    using ModularMonolith.Shared;

    public class TestHelperFactory : TestHelperFactory<StartBootstraper, WriteDbContext, ReadDbContext>
    {
        public TestHelperFactory() : base(ServiceCollectionExtensions.ModuleCode)
        {
        }
    }
}
